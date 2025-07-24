using FarmProductsWPF_BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Interfaces
{
    public interface IStockRepo
    {
        List<Stock> GetAllStocks();
        Stock? GetStockByProductId(int productId);
        List<Stock> SearchStock(string searchText);
        Stock UpdateStockLevel(Stock stock);
    }
}
