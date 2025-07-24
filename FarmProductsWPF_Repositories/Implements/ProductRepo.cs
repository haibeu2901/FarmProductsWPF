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
        public Product AddProduct(Product product)
        {
            return ProductDAO.Instance.AddProduct(product);
        }

        public bool DeleteProduct(int productId)
        {
            return ProductDAO.Instance.DeleteProduct(productId);
        }

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

        public Product UpdateProduct(Product product)
        {
            return ProductDAO.Instance.UpdateProduct(product);
        }
    }
}
