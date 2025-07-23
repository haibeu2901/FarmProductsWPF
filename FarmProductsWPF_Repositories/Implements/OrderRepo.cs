using FarmProductsWPF_BOs;
using FarmProductsWPF_DAOs;
using FarmProductsWPF_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Implements
{
    public class OrderRepo : IOrderRepo
    {
        public Order CreateOrder(Order order)
        {
            return OrderDAO.Instance.CreateOrder(order);
        }

        public List<Order> GetAllOrders()
        {
            return OrderDAO.Instance.GetAllOrders();
        }

        public Order? GetOrderById(int orderId)
        {
            return OrderDAO.Instance.GetOrderById(orderId);
        }

        public List<Order> GetOrdersByAccountId(int accountId)
        {
            return OrderDAO.Instance.GetOrdersByAccountId(accountId);
        }

        public List<Order> GetOrdersHistory()
        {
            return OrderDAO.Instance.GetOrdersHistory();
        }

        public List<Order> SearchOrdersByAccountId(int accountId, string searchTerm)
        {
            return OrderDAO.Instance.SearchOrdersByAccountId(accountId, searchTerm);
        }

        public List<Order> SearchOrdersHistory(string searchTerm)
        {
            return OrderDAO.Instance.SearchOrdersHistory(searchTerm);
        }
    }
}
