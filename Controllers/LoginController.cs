using Microsoft.AspNetCore.Mvc;
using proyecto_ecommerce_.NET_MVC_.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using proyecto_ecommerce_.NET_MVC_.Data;
using proyecto_ecommerce_.NET_MVC_.ViewModels;
using System;
using System.Text.RegularExpressions;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    public class LoginController : Controller
    {
        private readonly EcommerceCursoContext _context;
        public LoginController(EcommerceCursoContext appDBContext) 
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
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Debe completar ambos campos.";
                return View("Index");
            }
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Usuario o contraseña incorrectos.";
                return View("Index");
            } // Logica para guardar la sesión del usuario puede ser agregada aquí.

            TempData["SuccessMessage"] = "Inicio de sesión exitoso.";
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Registro(UsuarioVM modelo)
        {
            if (!Regex.IsMatch(modelo.direccion, @"^[a-zA-Z0-9\s,.-]+$"))
            {
                ViewData["ErrorDirec"] = "La dirección es inválida.";
                return View();
            }


            if (modelo.password != modelo.repetirPassword)
            {
                ViewData["MCumplir"] = "Las contraseñas deben ser iguales";
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
                Password = modelo.password,
                Tipo = "usuario"
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

			ViewData["NuevoUsuario"] = "Bienvenido, ya tienes cuenta (no olvides la contraseña)";

			return View();
        }


    }
}
