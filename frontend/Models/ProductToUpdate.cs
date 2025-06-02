using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class ProductToUpdate
    {
        [Required(ErrorMessage = "A product name must be provided.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "A product description must be provided.")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "A price must be provided.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "A product ID must be provided")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Inventory status must be provided.")]
        public bool InStock { get; set; }
    }
}
