using System.ComponentModel.DataAnnotations;

namespace Mehri_Bright.Models;

public class CreateOrderDto
{
    [Required]
    public int CustomerId { get; set; }

    public List<CreateOrderProductDto> OrderProducts { get; set; } = new List<CreateOrderProductDto>();
}
