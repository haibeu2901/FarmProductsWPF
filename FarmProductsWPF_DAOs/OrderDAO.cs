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
                .Include(o => o.Customer == null ? null : o.Customer)
                .Where(o => o.CustomerId == accountId || o.StaffId == accountId)
                .OrderByDescending(o => o.OrderId)
                .ThenByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> SearchOrdersByAccountId(int accountId, string searchTerm)
        {
            bool isDate = DateTime.TryParse(searchTerm, out DateTime parsedDate);
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Staff)
                .Where(o => o.CustomerId == accountId)
                .Where(o =>
                    o.OrderId.ToString() == searchTerm ||
                    (o.Staff != null && o.Staff.FullName.ToLower().Contains(searchTerm.ToLower())) ||
                    o.OrderDetails.Any(od => od.Product.ProductName.ToLower().Contains(searchTerm.ToLower())) ||
                    (isDate && o.OrderDate.HasValue && o.OrderDate.Value.Date == parsedDate.Date)
                )
                .OrderByDescending(o => o.OrderId)
                .ThenByDescending(o => o.OrderDate)
                .AsEnumerable()
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

        public List<Order> GetOrdersHistory()
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Staff)
                .Include(o => o.Customer == null ? null : o.Customer)
                .Where(o => o.OrderDate.HasValue)
                .OrderByDescending(o => o.OrderId)
                .ThenByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> SearchOrdersHistory(string searchTerm)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Staff)
                .Include(o => o.Customer == null ? null : o.Customer)
                .Where(o => o.OrderDate.HasValue)
                .Where(o => o.OrderId.ToString() == searchTerm ||
                            (o.Staff != null && o.Staff.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                            (o.Customer != null && (o.Customer.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                            o.Customer.PhoneNumber.ToLower().Contains(searchTerm.ToLower())) ||
                            o.OrderDetails.Any(od => od.Product.ProductName.ToLower().Contains(searchTerm.ToLower())))))
                .OrderByDescending(o => o.OrderId)
                .ThenByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order UpdateOrder(Order order)
        {
            var existingOrder = GetOrderById(order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.CustomerId = order.CustomerId;
                existingOrder.StaffId = order.StaffId;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.TotalAmount = order.TotalAmount;
                _context.SaveChanges();
            }
            return existingOrder;
        }

        public List<Order> FilterOrdersByDate(DateOnly minDate, DateOnly maxDate)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Staff)
                .Include(o => o.Customer == null ? null : o.Customer)
                .Where(o => o.OrderDate.HasValue &&
                            DateOnly.FromDateTime(o.OrderDate.Value) >= minDate &&
                            DateOnly.FromDateTime(o.OrderDate.Value) <= maxDate)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> FilterCustomerOrdersByDate(int customerId, DateOnly minDate, DateOnly maxDate)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Staff)
                .Include(o => o.Customer == null ? null : o.Customer)
                .Where(o => o.OrderDate.HasValue &&
                            DateOnly.FromDateTime(o.OrderDate.Value) >= minDate &&
                            DateOnly.FromDateTime(o.OrderDate.Value) <= maxDate &&
                            o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
    }
}
