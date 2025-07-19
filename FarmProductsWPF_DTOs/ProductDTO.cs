using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProductsWPF_DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string Unit { get; set; } = null!;

        public decimal SellingPrice { get; set; }

        public string? Description { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? CategoryName { get; set; }
    }
}
