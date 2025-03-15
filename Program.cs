using Microsoft.EntityFrameworkCore;
using proyecto_ecommerce_.NET_MVC_.Models;
//1- 
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Input;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion a base de datos desde secretos de usuario
builder.Services.AddDbContext<EcommerceNetContext>(options => options.UseSqlServer(builder.Configuration["ConexionString"]));

// Agrega servicios para la sesi�n y cookies ********************* CONFIGURACION DE SESSION y Cookies Authentication
//2- CONFIG PARA COOKIES  
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    { 
        option.LoginPath = "/Home/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        option.AccessDeniedPath = "/Login/Login";
    }); 
//CONFIG PARA SESSION
builder.Services.AddDistributedMemoryCache(); //requerido para Session
builder.Services.AddSession(options =>
{  
    //options.Cookie.Name = ".MiAplicacion.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
/********************************** Fin Session *********************************************+*/
// Agrega servicios de controladores y vistas
builder.Services.AddControllersWithViews();

/*****  agregamos config JWT  *****/
var key = builder.Configuration["Jwt:key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.RequireHttpsMetadata = false;
        option.SaveToken = true;
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });

builder.Services.AddHttpClient();

var app = builder.Build();

// Habilitar sesiones en la aplicaci�n ******** INICILIAZANDO EL USO DE SESSION
app.UseSession(); //habilita Session

// Otros middlewares
app.UseStaticFiles();
app.UseRouting();
//3- COOKIES
app.UseAuthentication();

app.UseAuthorization();
/************************************ fin en habilitar *******************************************/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//inicializa autenticaion jwt
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
