using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRepositories();
builder.Services.AddOptions(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddConnectors();
builder.Services.RegisterHandlers();
builder.Services.AddServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();