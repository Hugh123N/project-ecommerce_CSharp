using Microsoft.AspNetCore.Mvc;
using proyecto_ecommerce_.NET_MVC_.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using proyecto_ecommerce_.NET_MVC_.ViewModels;
using System;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Azure.Core;
using proyecto_ecommerce_.NET_MVC_.service;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    public class LoginController : BaseController
    {
        private readonly EcommerceNetContext _context;
        public LoginController(EcommerceNetContext appDBContext) 
        { 
            _context = appDBContext;
        }

        [HttpGet]
		public IActionResult Registro()
		{
			return View();
		}
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
        public async Task<IActionResult> Login(UsuarioVM modelo)
        {
            Usuario? usuario_encontrado = await _context.Usuarios.Where(u => u.Username == modelo.username).FirstOrDefaultAsync();

            if (usuario_encontrado == null && !BCrypt.Net.BCrypt.Verify(modelo.password, usuario_encontrado?.Password))
            {
                TempData["Message"] = "No existe usuario o contraseña";
                TempData["MessageType"] = "warning"; // success, error, info, warning
                return View();
            }

            //config cookies -> es para crear las cookies y gestionar nombre, username y tipo(rol)
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, usuario_encontrado.Nombre),
                 new Claim("UserName", usuario_encontrado.Username),
                 new Claim(ClaimTypes.Email, usuario_encontrado.Email),
                 new Claim("UsuarioCod", usuario_encontrado.Id.ToString())
             };
            claims.Add(new Claim(ClaimTypes.Role, usuario_encontrado.Tipo));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


            // Guardar información del usuario en Session
            HttpContext.Session.SetInt32("UsuarioId", usuario_encontrado.Id);

            TempData["SuccessMessage"] = "Inicio de sesión exitoso.";
            TempData["MessageType"] = "success"; // success, error, info, warning

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Registro(UsuarioVM modelo)
        {
            if (modelo.password != modelo.repetirPassword)
            {
                ViewData["MCumplir"] = "Las contraseñas deben ser iguales";
                return View();
            }

            if (string.IsNullOrEmpty(modelo.direccion) || !Regex.IsMatch(modelo.direccion, @"^[a-zA-Z0-9\s,.\-#]+$"))
            {
                ViewData["MCumplir"] = "La dirección no es válida o está vacía";
                return View();
            }

            if (string.IsNullOrEmpty(modelo.email) || !Regex.IsMatch(modelo.email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) 
            { 
                ViewData["MCumplir"] = "El formato del correo es inválido"; 
                return View(); 
            }
            if (string.IsNullOrEmpty(modelo.telefono) || !Regex.IsMatch(modelo.telefono, @"^[0-9]{7,13}$")) 
            { 
                ViewData["MCumplir"] = "El teléfono es inválido. (9 digitos en Perú)"; 
                return View(); 
            }
            if (string.IsNullOrEmpty(modelo.nombre) || !Regex.IsMatch(modelo.nombre, @"^[a-zA-Z\sáéíóúÁÉÍÓÚñÑ]+$"))
            {
                ViewData["MCumplir"] = "El nombre es inválido.";
                return View();
            }
            if (string.IsNullOrEmpty(modelo.username) || !Regex.IsMatch(modelo.username, @"^[a-zA-Z0-9]{9,255}$")) 
            { 
                ViewData["MCumplir"] = "El nombre de usuario debe ser minimo 9 digitos."; 
                return View(); 
            }
            if (string.IsNullOrEmpty(modelo.password) || !Regex.IsMatch(modelo.password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$")) 
            { 
                ViewData["MCumplir"] = "La contraseña es inválida. Recuerda que minimo 8 digitos y UNA MAYUSCULa, una minuscula y un número"; 
                return View(); 
            }

            /*usar validaciones backend primero*/

            Usuario? username_repetido = await _context.Usuarios.Where(u => u.Username == modelo.username).FirstOrDefaultAsync();
            if (username_repetido != null)
            {
                ViewData["MCumplir"] = "Ese username ya lo usó otro";
                return View(); 
            }

            Usuario? correo_repetido = await _context.Usuarios.Where(u => u.Email == modelo.email).FirstOrDefaultAsync();
			if (correo_repetido != null)
			{
				ViewData["MCumplir"] = "El correo ya lo usa otro usuario";
				return View();
			}

            Usuario usuario = new Usuario()
            {
                Direccion = modelo.direccion,
                Email = modelo.email,
                Nombre = modelo.nombre,
                Telefono = modelo.telefono,
                Username = modelo.username,
                Password = BCrypt.Net.BCrypt.HashPassword(modelo.password), //encripta password
                Tipo = "user"
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            TempData["NuevoUsuario"] = "Bienvenido, ya tienes cuenta (no olvides la contraseña). Ingresa para comprar";
            TempData["MessageType"] = "info"; // success, error, info, warning

            return RedirectToAction("Index", "Home");
        }

        //cerrar session
        public async Task<IActionResult> CerrarSession()
        {
            //cerramos cookies ->   Out para salir
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("UsuarioId");
            HttpContext.Session.Clear(); // Borra toda la información en Session

            TempData["Message"] = "Session cerrada exitosamente.";
            TempData["MessageType"] = "success"; // success, error, info, warning

            return RedirectToAction("Index", "Home");
        }
        
    }
}
