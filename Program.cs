using Microsoft.EntityFrameworkCore;
using proyecto_ecommerce_.NET_MVC_.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion a base de datos desde secretos de usuario
//var secret = builder.Configuration["ConexionString"];
builder.Services.AddDbContext<EcommerceCursoContext>(options => options.UseSqlServer(builder.Configuration["ConexionString"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
