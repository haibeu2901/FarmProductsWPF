using FarmProductsWPF_BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Interfaces
{
    public interface ICategoryRepo
    {
        bool DeleteCategory(int categoryId);
        ProductCategory UpdateCategory(ProductCategory category);
        ProductCategory AddCategory(ProductCategory category);
        ProductCategory? GetCategoryById(int categoryId);
        List<ProductCategory> SearchCategory(string searchText);
        List<ProductCategory> GetAllCategories();
    }
}
