using System;
using System.Collections.Generic;

namespace NorthwindSqlite.Models;

public partial class Region
{
    public long Id { get; set; }

    public string? RegionDescription { get; set; }
}
