using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Valtimperium;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<ValtimperContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    options.LoginPath = "/Log/Login");

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();


var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};


app.UseCookiePolicy(cookiePolicyOptions);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Main}/{id?}");

app.Run();


//Scaffold-DbContext "Server=.\SQLEXPRESS;Database=Valtimperium;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer
//@{
//Layout = "_Layout";
//}
