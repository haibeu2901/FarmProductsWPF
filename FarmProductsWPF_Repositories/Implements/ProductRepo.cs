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
    public class ProductRepo : IProductRepo
    {
        public List<Product> GetAllProducts()
        {
            return ProductDAO.Instance.GetAllProducts();
        }

        public Product? GetProductById(int productId)
        {
            return ProductDAO.Instance.GetProductById(productId);
        }

        public List<Product> SearchProduct(string searchText)
        {
            return ProductDAO.Instance.SearchProduct(searchText);
        }
    }
}
