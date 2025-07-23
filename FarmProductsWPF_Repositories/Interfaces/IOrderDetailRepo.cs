using FarmProductsWPF_BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Interfaces
{
    public interface IOrderDetailRepo
    {
        List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        OrderDetail CreateOrderDetail(OrderDetail orderDetail);
    }
}
