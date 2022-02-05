
using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Server.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductAsync();

        Task<ServiceResponse<Product>> GetProductAsync(int productId);

        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);

        Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);

        //Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page);

        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);








    }
}
