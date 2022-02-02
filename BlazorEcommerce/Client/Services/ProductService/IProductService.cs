namespace BlazorEcommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        //Permite decirle al ciclo que ha cambiado
        event Action ProductsChanged;
        List<Product> Products { get; set; }

        Task GetProducts(string? categoryUrl = null);

        Task<ServiceResponse<Product>> GetProduct(int productId);



    }



}
