using Mehri_Bright.Entities;

namespace Mehri_Bright.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool InStock { get; set; }

    }
}
