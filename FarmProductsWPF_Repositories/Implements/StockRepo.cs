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
    public class StockRepo : IStockRepo
    {
        public List<Stock> GetAllStocks()
        {
            return StockDAO.Instance.GetAllStocks();
        }

        public Stock? GetStockByProductId(int productId)
        {
            return StockDAO.Instance.GetStockByProductId(productId);
        }

        public List<Stock> SearchStock(string searchText)
        {
            return StockDAO.Instance.SearchStock(searchText);
        }

        public Stock UpdateStockLevel(Stock stock)
        {
            return StockDAO.Instance.UpdateStockLevel(stock);
        }
    }
}
