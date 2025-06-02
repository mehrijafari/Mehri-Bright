using System;
using System.Collections.Generic;

namespace Mehri_Bright.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
