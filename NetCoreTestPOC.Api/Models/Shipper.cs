using System;
using System.Collections.Generic;

namespace NorthwindSqlite.Models;

public partial class Shipper
{
    public long Id { get; set; }

    public string? CompanyName { get; set; }

    public string? Phone { get; set; }
}
