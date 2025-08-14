﻿using System;
using System.Collections.Generic;

namespace FarmProductsWPF_BOs;

public partial class ImportedStock
{
    public int ImportId { get; set; }

    public int ProductId { get; set; }

    public int StockBeforeUpdate { get; set; }

    public int UpdatedStockQuantity { get; set; }

    public int StockAfterUpdate { get; set; }

    public string? Notes { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Stock Product { get; set; } = null!;

    public virtual Account UpdatedByNavigation { get; set; } = null!;
}
