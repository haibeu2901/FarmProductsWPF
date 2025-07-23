using FarmProductsWPF_BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_DAOs
{
    public class OrderDetailDAO
    {
        private readonly FarmProductsDbContext _context;
        private static OrderDetailDAO? _instance;

        public OrderDetailDAO()
        {
            _context = new FarmProductsDbContext();
        }

        public static OrderDetailDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderDetailDAO();
                }
                return _instance;
            }
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Product.Category)
                .Include(od => od.Order)
                .Where(od => od.Order.OrderId == orderId)
                .ToList();
        }

        public OrderDetail CreateOrderDetail(int orderId, OrderDetail orderDetail)
        {
            orderDetail.OrderId = orderId;
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
            return orderDetail;
        }

        public List<OrderDetail> CreateOrderDetails(int orderId, List<OrderDetail> orderDetails)
        {
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderId = orderId;
                _context.OrderDetails.Add(orderDetail);

                // Update the product stock
                var stock = _context.Stocks.Find(orderDetail.ProductId);
                int productId = stock?.ProductId ?? 0;
                if (stock != null)
                {
                    StockDAO.Instance.UpdateStockWhenOrderCreated(productId, orderDetail.Quantity);
                }
                else
                {
                    throw new Exception($"Product with ID {orderDetail.ProductId} not found.");
                }
            }
            _context.SaveChanges();
            return orderDetails;
        }
    }
}
