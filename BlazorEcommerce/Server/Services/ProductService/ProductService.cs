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

    }
}
