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
                .Include(od => od.Order)
                .Where(od => od.Order.OrderId == orderId)
                .ToList();
        }
    }
}
