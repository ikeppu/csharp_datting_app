
using System.Text;
using c_sharp_angular.Data;
using c_sharp_angular.Interfaces;
using c_sharp_angular.Middleware;
using c_sharp_angular.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
//Question If needd Itoken for another Service
builder.Services.AddScoped<ITokenService, TokenService>();

// Question: Add Db connect ?
// I thought it is correct answer
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
// config token inject token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200", "http://localhost:44423"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

