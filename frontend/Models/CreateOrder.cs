using System.ComponentModel.DataAnnotations;

namespace frontend.Models;

public class CreateOrder
{
    [Required]
    public int CustomerId { get; set; }

    public List<CreateOrderProduct> OrderProducts { get; set; } = new List<CreateOrderProduct>();
}
