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
    public class CategoryRepo : ICategoryRepo
    {
        public ProductCategory AddCategory(ProductCategory category)
        {
            return CategoryDAO.Instance.AddCategory(category);
        }

        public bool DeleteCategory(int categoryId)
        {
            return CategoryDAO.Instance.DeleteCategory(categoryId);
        }

        public List<ProductCategory> GetAllCategories()
        {
            return CategoryDAO.Instance.GetAllCategories();
        }

        public ProductCategory? GetCategoryById(int categoryId)
        {
            return CategoryDAO.Instance.GetCategoryById(categoryId);
        }

        public List<ProductCategory> SearchCategory(string searchText)
        {
            return CategoryDAO.Instance.SearchCategory(searchText);
        }

        public ProductCategory UpdateCategory(ProductCategory category)
        {
            return CategoryDAO.Instance.UpdateCategory(category);
        }
    }
}
