using FarmProductsWPF_BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Interfaces
{
    public interface IOrderRepo
    {
        List<Order> GetAllOrders();
        List<Order> GetOrdersByAccountId(int accountId);
    }
}
