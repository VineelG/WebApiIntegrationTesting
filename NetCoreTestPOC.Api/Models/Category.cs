using System;
using System.Collections.Generic;

namespace NorthwindSqlite.Models;

public partial class Category
{
    public long Id { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
