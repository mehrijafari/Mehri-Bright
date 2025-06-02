namespace frontend.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public List<OrderProduct> ProductsForOrder { get; set; } = new List<OrderProduct>();
}
