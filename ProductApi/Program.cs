using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Product.App.Contract.IServices;
using Product.App.Contract.Mappers;
using Product.App.Services;
using Product.Infrastructure.Context;
using Product.Infrastructure.Repositories.Product;
using Product.Model.Models;
using Shop.EndPoint.Api.Middlewares;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(x => x.AddPolicy("CorsPolicy",
    b => b.AllowAnyHeader()
          .AllowAnyMethod().WithOrigins("www.google.com", "http://localhost:2000")));

builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IProductRepository<Products>, ProductRepository<Products>>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
var connectionString = builder.Configuration.GetConnectionString("ProductConnectionString");
builder.Services.AddDbContext<ProductContext>(option => option.UseSqlServer(connectionString));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(cfg =>
   {
       cfg.TokenValidationParameters = new TokenValidationParameters()
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateIssuerSigningKey = true,
           RequireExpirationTime = false,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenService.Key))
       };
   });


builder.Services.AddAuthorization(x => x.AddPolicy("AdminPolicy", p => p.RequireClaim(ClaimTypes.Role)));

builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.Run();
