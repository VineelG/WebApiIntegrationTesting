using System;
using System.Collections.Generic;

namespace NorthwindSqlite.Models;

public partial class OrderDetail
{
    public string Id { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public long Quantity { get; set; }

    public double Discount { get; set; }

    public long? ProductId { get; set; }

    public long? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
