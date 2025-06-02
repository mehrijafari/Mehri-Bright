using System.ComponentModel.DataAnnotations;

namespace Mehri_Bright.Models
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; } = null!;
    }
}
