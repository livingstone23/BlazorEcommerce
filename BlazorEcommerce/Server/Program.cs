global using BlazorEcommerce.Shared.Models;
global using Microsoft.EntityFrameworkCore;
global using BlazorEcommerce.Server.Data;
global using BlazorEcommerce.Server.Services.ProductServices;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//(1.1)Permite configurar Swagger una vez instalado el paquete Swashbuckle.AspNetCore
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro de Servicios
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();

//(1.2)Permite configurar Swagger una vez instalado el paquete Swashbuckle.AspNetCore
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//(1.3)Permite configurar Swagger una vez instalado el paquete Swashbuckle.AspNetCore
app.UseSwagger();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
