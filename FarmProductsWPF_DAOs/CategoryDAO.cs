using FarmProductsWPF_BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_DAOs
{
    public class CategoryDAO
    {
        private readonly FarmProductsDbContext _context;
        private static CategoryDAO? _instance;

        public CategoryDAO()
        {
            _context = new FarmProductsDbContext();
        }

        public static CategoryDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CategoryDAO();
                }
                return _instance;
            }
        }

        public List<ProductCategory> GetAllCategories()
        {
            return _context.ProductCategories
                .Include(c => c.Products)
                .ToList();
        }

        public List<ProductCategory> SearchCategory(string searchText)
        {
            return _context.ProductCategories
                .Include(c => c.Products)
                .AsEnumerable()
                .Where(c => c.CategoryName.ToLower().Contains(searchText.ToLower()) ||
                            c.Description.ToLower().Contains(searchText.ToLower()) ||
                            c.CategoryId.ToString().Equals(searchText))
                .ToList();
        }

        public ProductCategory? GetCategoryById(int categoryId)
        {
            return _context.ProductCategories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public ProductCategory AddCategory(ProductCategory category)
        {
            _context.ProductCategories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public ProductCategory UpdateCategory(ProductCategory category)
        {
            var existingCategory = GetCategoryById(category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.Description = category.Description;
                _context.SaveChanges();
                return existingCategory;
            }
            return null;
        }

        public bool DeleteCategory(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category != null)
            {
                if (category.Products.Any())
                {
                    return false;
                }
                _context.ProductCategories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
