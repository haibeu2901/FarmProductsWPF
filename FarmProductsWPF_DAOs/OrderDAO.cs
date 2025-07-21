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
    }
}
