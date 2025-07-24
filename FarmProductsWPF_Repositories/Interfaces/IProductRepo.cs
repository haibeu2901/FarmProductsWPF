using FarmProductsWPF_BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Interfaces
{
    public interface IProductRepo
    {
        List<Product> GetAllProducts();
        List<Product> SearchProduct(string searchText);
        Product? GetProductById(int productId);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        bool DeleteProduct(int productId);
    }
}
