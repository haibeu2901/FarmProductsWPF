using System;
using System.Collections.Generic;

namespace FarmProductsWPF_BOs;

public partial class Stock
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime? LastUpdated { get; set; }

    public int? UpdatedBy { get; set; }

    public string? Notes { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Account? UpdatedByNavigation { get; set; }
}
