using System;
using System.Collections.Generic;

namespace FarmProductsWPF_BOs;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal SellingPrice { get; set; }

    public string? Description { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ProductCategory? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Stock? Stock { get; set; }
}
