using FarmProductsWPF_BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_DAOs
{
    public class ProductDAO
    {
        private readonly FarmProductsDbContext _context;
        private static ProductDAO? _instance;

        public ProductDAO()
        {
            _context = new FarmProductsDbContext();
        }

        public static ProductDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProductDAO();
                }
                return _instance;
            }
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }
    }
}
