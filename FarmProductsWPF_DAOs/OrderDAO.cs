using FarmProductsWPF_BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_DAOs
{
    public class OrderDAO
    {
        private readonly FarmProductsDbContext _context;
        private static OrderDAO? _instance;

        public OrderDAO()
        {
            _context = new FarmProductsDbContext();
        }

        public static OrderDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderDAO();
                }
                return _instance;
            }
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Staff)
                .OrderByDescending(o => o.OrderId)
                .ThenByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> GetOrdersByAccountId(int accountId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Staff)
                .Where(o => o.CustomerId == accountId)
                .OrderByDescending(o => o.OrderId)
                .ThenByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> SearchOrdersByAccountId(int accountId, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetOrdersByAccountId(accountId);
            }

            var query = _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Include(o => o.Staff)
                .Where(o => o.CustomerId == accountId);

            // Try parsing search term as OrderId
            if (int.TryParse(searchTerm, out int orderId))
            {
                query = query.Where(o => o.OrderId == orderId);
            }
            // Try parsing search term as Date
            else if (DateTime.TryParse(searchTerm, out DateTime orderDate))
            {
                query = query.Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == orderDate.Date);
            }
            // Search by staff name or product name
            else
            {
                query = query.Where(o =>
                    (o.Staff != null && o.Staff.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    o.OrderDetails.Any(od => od.Product.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                );
            }

            return query
                .OrderByDescending(o => o.OrderId)
                .ThenByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order? GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.Staff)
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
    }
}
