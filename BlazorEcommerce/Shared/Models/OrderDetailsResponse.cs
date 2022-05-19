namespace BlazorEcommerce.Shared.Models;

public class OrderDetailsResponse
{
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderDetailsProductResponse> Products { get; set; }
}