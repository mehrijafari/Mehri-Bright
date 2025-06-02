namespace Mehri_Bright.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public List<OrderProductDto> ProductsForOrder { get; set; } = new List<OrderProductDto>();
    }
}
