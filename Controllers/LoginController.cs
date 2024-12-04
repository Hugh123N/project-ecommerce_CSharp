using Microsoft.AspNetCore.Mvc;
using proyecto_ecommerce_.NET_MVC_.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    public class LoginController : Controller
    {
        private readonly EcommerceCursoContext _context;
        public LoginController(EcommerceCursoContext context) 
        { 
            _context = context; 
        }

        public IActionResult Index() {
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


    }
}
