using FarmProductsWPF_BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_DAOs
{
    public class ImportedStockDAO
    {
        private readonly FarmProductsDbContext _context;
        private static ImportedStockDAO? _instance;

        public ImportedStockDAO()
        {
            _context = new FarmProductsDbContext();
        }

        public static ImportedStockDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ImportedStockDAO();
                }
                return _instance;
            }
        }

        public List<ImportedStock> GetStockHistoryByProductId(int productId)
        {
            return _context.ImportedStocks
                .Include(i => i.Product)
                .Where(i => i.ProductId == productId)
                .OrderByDescending(i => i.UpdatedAt)
                .ToList();
        }

        public ImportedStock AddStock(ImportedStock imStock)
        {
            _context.ImportedStocks.Add(imStock);
            _context.SaveChanges();
            return imStock;
        }
    }
}
