using Microsoft.EntityFrameworkCore;
using StoreSolution.Data.Data.EF;
using StoreSolution.Data.Services;
using Microsoft.Extensions.Configuration;
using StoreSolution.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//vuhv
builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreOnlineDatabase")));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBrandService,BrandService>();
builder.Services.AddTransient<IBrandService, BrandService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddTransient<ICartService,CartService>();

//builder.Services.AddScoped<ICartService,CartService>();
//builder.Services.AddTransient<ICartService, CartService>();

builder.Services.AddScoped<IStorageService, FileStorageService>();
//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
