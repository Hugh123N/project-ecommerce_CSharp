using Microsoft.EntityFrameworkCore;
using proyecto_ecommerce_.NET_MVC_.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion a base de datos desde secretos de usuario
/*var secret = builder.Configuration["ConexionString"];
Console.Write(secret);*/
builder.Services.AddDbContext<EcommerceCursoContext>(options => options.UseSqlServer(builder.Configuration["ConexionString"]));

// Agrega servicios para la sesión ********************* CONFIGURACION DE SESSION
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MiAplicacion.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Agrega servicios de controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Habilitar sesiones en la aplicación ******** INICILIAZANDO EL USO DE SESSION
app.UseSession();

// Otros middlewares
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

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
