using FarmProductsWPF_BOs;
using FarmProductsWPF_DAOs;
using FarmProductsWPF_DTOs;
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
        private Product MapToEntity(ProductDTO productDTO)
        {
            return new Product
            {
                ProductId = productDTO.ProductId,
                ProductName = productDTO.ProductName,
                Unit = productDTO.Unit,
                SellingPrice = productDTO.SellingPrice,
                Description = productDTO.Description,
                UpdatedDate = productDTO.UpdatedDate,
            };
        }

        private ProductDTO MapToDTO(Product product)
        {
            if (product == null) return null;
            
            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Unit = product.Unit,
                SellingPrice = product.SellingPrice,
                Description = product.Description,
                UpdatedDate = product.UpdatedDate,
                CategoryName = product.Category?.CategoryName
            };
        }

        public List<ProductDTO> GetAllProducts()
        {
            return ProductDAO.Instance.GetAllProducts()
                .Select(product => MapToDTO(product))
                .Where(dto => dto != null)
                .ToList();
        }
    }
}
