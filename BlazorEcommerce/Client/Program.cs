global using BlazorEcommerce.Shared.Models;
global using BlazorEcommerce.Shared;
global using System.Net.Http.Json;
global using BlazorEcommerce.Client.Services.ProductService;
global using BlazorEcommerce.Client.Services.CategoryService;
global using BlazorEcommerce.Client.Services.AuthService;
global using BlazorEcommerce.Client.Services.CartService;
global using BlazorEcommerce.Client.Services.OrderService;
global using Microsoft.AspNetCore.Components.Authorization;


using BlazorEcommerce.Client;
using BlazorEcommerce.Client.Services.OrderService;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


//Vid70-1.-Uso de local storage
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


//Registro de servicios
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Vid108.-2 Expose the authentication state
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


await builder.Build().RunAsync();

