using FarmProductsWPF_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_Repositories.Interfaces
{
    public interface IProductRepo
    {
        List<ProductDTO> GetAllProducts();
    }
}
