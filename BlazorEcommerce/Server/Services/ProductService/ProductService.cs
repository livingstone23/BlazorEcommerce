using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Server.Services.ProductServices
{
    public class ProductService: IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                          .Include(p=> p.Variants)
                          .ToListAsync()

            };

            return response;

        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            //Product product = null;

            //if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            //{
            //    product = await _context.Products
            //        .Include(p => p.Variants.Where(v => !v.Deleted))
            //        .ThenInclude(v => v.ProductType)
            //        .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted);
            //}
            //else
            //{
            //    product = await _context.Products
            //        .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
            //        .ThenInclude(v => v.ProductType)
            //        .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted && p.Visible);
            //}

            //if (product == null)
            //{
            //    response.Success = false;
            //    response.Message = "Sorry, but this product does not exist.";
            //}
            //else
            //{
            //    response.Data = product;
            //}

            var product = await _context.Products
                .Include(p => p.Variants)
                .ThenInclude(v => v.ProductType)
                .FirstOrDefaultAsync(p => p.Id == productId);


            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this product does not exist.";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                    .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower())) 
                                //&& p.Visible && !p.Deleted)
                    .Include(p => p.Variants)
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.Where(p=>p.Title.ToLower().Contains(searchText.ToLower())
                                                        ||
                                                        p.Description.ToLower().Contains(searchText.ToLower()))
                                                            .Include(p=> p.Variants)
                                                            .ToListAsync()
                
            };

            return response;
        }



        /// <summary>
        /// Permite realizar busquedas de propuestas 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {

            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };

        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                            p.Description.ToLower().Contains(searchText.ToLower())) //&&
                            //p.Visible && !p.Deleted)
                .Include(p => p.Variants)
                .ToListAsync();
        }
    }
}
