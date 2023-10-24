using System;
using System.Collections.Generic;

namespace WebAPIKurs;

public partial class Delivery
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Type { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;
}
