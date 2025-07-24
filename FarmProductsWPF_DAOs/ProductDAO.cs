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
            return _context.Products
                .Include(p => p.Stock)
                .Include(p => p.Category)
                .ToList();
        }

        public List<Product> SearchProduct(string searchText)
        {
            return _context.Products
                .Include(p => p.Stock)
                .Include(p => p.Category)
                .AsEnumerable() // Switch to client-side evaluation
                .Where(p => p.ProductName.ToLower().Contains(searchText.ToLower()) ||
                            p.ProductId.ToString().Equals(searchText) ||
                            p.Category.CategoryName.ToLower().Contains(searchText.ToLower()) ||
                            p.Description.ToLower().Contains(searchText.ToLower()))
                .ToList();
        }

        public Product? GetProductById(int productId)
        {
            return _context.Products
                .Include(p => p.Stock)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == productId);
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Unit = product.Unit;
                existingProduct.SellingPrice = product.SellingPrice;
                existingProduct.Description = product.Description;
                _context.SaveChanges();
            }
            return existingProduct;
        }

        public bool DeleteProduct(int productId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // First, check for and delete any associated stock
                    var stock = _context.Stocks.Find(productId);
                    if (stock != null)
                    {
                        _context.Stocks.Remove(stock);
                    }
                    
                    // Then delete the product
                    var product = _context.Products.Find(productId);
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                        _context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    
                    return false;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
