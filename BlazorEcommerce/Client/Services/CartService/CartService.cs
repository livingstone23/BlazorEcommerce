using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private  readonly  ILocalStorageService _localStorage;
        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {

            //if (await _authService.IsUserAuthenticated())
            //{
            //    await _http.PostAsJsonAsync("api/cart/add", cartItem);
            //}
            //else
            //{
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                {
                    cart = new List<CartItem>();
                }

                cart.Add(cartItem);

                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
                //var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId &&
                //                              x.ProductTypeId == cartItem.ProductTypeId);
                //if (sameItem == null)
                //{
                //    cart.Add(cartItem);
                //}
                //else
                //{
                //    sameItem.Quantity += cartItem.Quantity;
                //}

                //await _localStorage.SetItemAsync("cart", cart);
                //}

                //await GetCartItemsCount();

        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            return cart;
        }

    }
}
