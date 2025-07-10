using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TechStore.Api;
using TechStore.Api.Configurations;
using TechStore.Api.DTO;
using TechStore.Api.DTOValidators;
using TechStore.DAL.Data;
using TechStore.DAL.Repositories;
using TechStore.DAL.UnitOfWork;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Repositories;
using TechStore.Domain.Interfaces.Services;
using TechStore.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Type = SecuritySchemeType.Http,
        Name = "Authorization",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

Registrator.RegisterServices(builder.Services, builder.Configuration);

builder.Services.AddScoped<ITokensService, TokensService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShopAddressService, ShopAddressService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IShopAddressRepository, ShopAddressRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IValidator<CategoryDTO>, CategoryDTOValidator>();
builder.Services.AddScoped<IValidator<ShopAddressDTO>, ShopAddressDTOValidator>();
builder.Services.AddScoped<IValidator<ProductDTO>, ProductDTOValidator>();
builder.Services.AddScoped<IValidator<UserDTO>, UserDTOValidator>();
builder.Services.AddScoped<IValidator<CartDTO>, CartDTOValidator>();
builder.Services.AddScoped<IValidator<CartItemDTO>, CartItemDTOValidator>();
builder.Services.AddScoped<IValidator<OrderDTO>, OrderDTOValidator>();
builder.Services.AddScoped<IValidator<CartItemToCreateDTO>, CartItemToCreateDTOValidator>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var currentPath = builder.Environment.ContentRootPath;
builder.Services.AddScoped<IFileUploadService>(_ => new FileUploadService(currentPath));

var jwtConfig = builder.Configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
    {
        config.Audience = jwtConfig.Audience;
        config.RequireHttpsMetadata = false;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
        };
    });

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TechStoreDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "static", "images")),
    RequestPath = "/images"
});

app.MapControllers();

app.Run();
