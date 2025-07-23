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
    public class OrderDetailRepo : IOrderDetailRepo
    {
        public OrderDetail CreateOrderDetail(OrderDetail orderDetail)
        {
            return OrderDetailDAO.Instance.CreateOrderDetail(orderDetail);
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            return OrderDetailDAO.Instance.GetOrderDetailsByOrderId(orderId);
        }
    }
}
