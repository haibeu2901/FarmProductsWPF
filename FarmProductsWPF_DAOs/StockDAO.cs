using FarmProductsWPF_BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_DAOs
{
    public class StockDAO
    {
        private readonly FarmProductsDbContext _context;
        private static StockDAO? _instance;

        public StockDAO()
        {
            _context = new FarmProductsDbContext();
        }

        public static StockDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StockDAO();
                }
                return _instance;
            }
        }

        public List<Stock> GetAllStocks()
        {   
            return _context.Stocks
                .Include(s => s.Product)
                .ThenInclude(p => p.Category)
                .ToList();
        }

        public Stock? GetStockByProductId(int productId)
        {
            return _context.Stocks
                .Include(s => s.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefault(s => s.ProductId == productId);
        }

        public List<Stock> SearchStock(string searchText)
        {
            return _context.Stocks
                .Include(s => s.Product)
                .ThenInclude(p => p.Category)
                .AsEnumerable() // Switch to client-side evaluation
                .Where(s => s.Product.ProductName.ToLower().Contains(searchText.ToLower()) ||
                            s.Product.ProductId.ToString().Equals(searchText) ||
                            s.Product.Category.CategoryName.ToLower().Contains(searchText.ToLower()) ||
                            s.Product.Description.ToLower().Contains(searchText.ToLower()))
                .ToList();
        }

        public Stock UpdateStockWhenOrderCreated(int productId, int quantity)
        {
            var stock = GetStockByProductId(productId);
            if (stock != null)
            {
                stock.Quantity -= quantity;
                _context.SaveChanges();
            }
            return stock;
        }

        public Stock UpdateStockLevel(Stock stock)
        {
            var existingStock = GetStockByProductId(stock.ProductId);
            if (existingStock != null)
            {
                existingStock.Quantity = stock.Quantity;
                existingStock.LastUpdated = DateTime.Now;
                existingStock.UpdatedBy = stock.UpdatedBy;
                existingStock.Notes = stock.Notes;
                _context.SaveChanges();
            }
            return existingStock;
        }
    }
}
