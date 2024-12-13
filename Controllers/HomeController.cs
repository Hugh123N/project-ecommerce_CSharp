using Microsoft.AspNetCore.Mvc;
using proyecto_ecommerce_.NET_MVC_.Models;
using System.Diagnostics;
//implementacion de dependencias
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceCursoContext _context;

        static List<Detalle> detalles=new List<Detalle>();
        static double sumaTotal = 0.0;
        Ordene orden = new Ordene();

        public HomeController(ILogger<HomeController> logger, EcommerceCursoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> products = await _context.Productos.ToListAsync();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("../Login/Login");
        }

        public IActionResult Carrito()
        {
            ViewBag.CarritoCount = detalles.Count;
            ViewBag.Total = sumaTotal;
            return View(detalles);
        }

        [HttpPost]
        public async Task<IActionResult> Carrito(int id, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            Detalle detalle=new Detalle
            {
                Cantidad = cantidad,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Total = cantidad * producto.Precio,
                Producto = producto
            };

            // Verifica si el producto ya está en la lista
            var detalleExistente = detalles.FirstOrDefault(d => d.Producto.Id == producto.Id);
            if(detalleExistente != null)
            {   // Si ya existe, actualiza la cantidad y el total
                detalleExistente.Cantidad += detalle.Cantidad;
                detalleExistente.Total = detalleExistente.Cantidad*detalleExistente.Precio;
            }else //sino esta crea un nuevo detalle
                detalles.Add(detalle);

            //recorremos la tabla y sumamos todos los totales
            sumaTotal = detalles.Sum(s=>s.Total);
            orden.Total = sumaTotal;
            ViewBag.Total = sumaTotal;

            //recorremos la tabla y contamos cuantos objetos hay
            ViewBag.CarritoCount = detalles.Count;

            //HttpContext.Session.SetInt32("CarritoCount",detalles.Count);
            return View(detalles);
        }

        public IActionResult DeleteCart(int id)
        {
            List<Detalle> detallesNueva = new List<Detalle>();
            foreach(var item in detalles)
            {
                if (item.Producto.Id != id) {
                    detallesNueva.Add(item);
                }
                detalles = detallesNueva;
                sumaTotal=detalles.Sum(s=>s.Total);

            }
            //mensaje de exito que se mostrara en la vista con Toastr.js
            TempData["Message"] = "Producto eliminado del carrito.";
            TempData["MessageType"] = "warning"; // success, error, info, warning

            return RedirectToAction("Carrito","Home");
        }

        public IActionResult OrdenResumen() {
            Usuario usuario=new Usuario();
            ViewBag.Total = sumaTotal;
            return View(detalles);
        }

        public IActionResult OrdenGenerar()
        {

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




