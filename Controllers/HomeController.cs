using Microsoft.AspNetCore.Mvc;
using proyecto_ecommerce_.NET_MVC_.Models;
using System.Diagnostics;
//implementacion de dependencias
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using System.Collections.ObjectModel;
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> d1e599e9db96b6f542f178bffbff961047e95d57

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EcommerceCursoContext _context;

        static List<Detalle> detalles=new List<Detalle>();
        static double sumaTotal = 0.0;

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
            ViewBag.Total = sumaTotal;

            //recorremos la tabla y contamos cuantos objetos hay
            ViewBag.CarritoCount = detalles.Count;

            //HttpContext.Session.SetInt32("CarritoCount",detalles.Count);
            return View(detalles);
        }

		[Authorize(Roles = "user")]
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

		[Authorize(Roles = "user")]
		public async Task<IActionResult> OrdenResumen() {
            //obtenemos ID de usuario mediante sesion
            int? idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            Usuario usuario = await _context.Usuarios.FindAsync(idUsuario);
            if(usuario == null) {
                TempData["Message"] = "Primero debe Loguearse para realizar el pedido";
                TempData["MessageType"] = "warning";
                return RedirectToAction("Login","Login");
            }
            ViewBag.Usuario = usuario;

            ViewBag.Total = sumaTotal;
            return View(detalles);
        }

		[Authorize(Roles = "user")]
		public async Task<IActionResult> GenerarOrden()
        {
            try {
                //obtener el numero correlativo
                List<Ordene> ordenes = await _context.Ordenes.ToListAsync();
                // Determinar el número mayor en las órdenes existentes
                int numero = ordenes.Any() ? ordenes.Max(o => int.Parse(o.Numero)) + 1 : 1;
                // Formatear el número con ceros a la izquierda
                string numeroCorrelativo = numero.ToString("D10"); // "D10" asegura que tendrá 10 dígitos

                //obtenemos ID de usuario mediante sesion
                int? idUsuario = HttpContext.Session.GetInt32("UsuarioId");
                Usuario usuario = await _context.Usuarios.FindAsync(idUsuario);
                if (usuario == null)
                {
                    TempData["Message"] = "El Usuario no existe.";
                    TempData["MessageType"] = "warning";
                    return RedirectToAction("Carrito","Home");
                }
                Ordene orden = new Ordene {
                    Usuario = usuario,
                    FechaCreacion = DateTime.Now,
                    Total = sumaTotal,
                    Numero = numeroCorrelativo
                };
                //guardamos orden
                _context.Add(orden);
                 await _context.SaveChangesAsync();
                
                foreach(var iten in detalles)
                {
                    var det = new Detalle
                    {
                        Nombre = iten.Nombre,
                        Cantidad = iten.Cantidad,
                        Precio = iten.Precio,
                        Total = iten.Total,
                        Orden = orden,
                        Producto = iten.Producto
                    };
                    _context.Add(det); // Añadimos cada detalle

                    // Actualizar el stock del producto
                    var producto = await _context.Productos.FindAsync(iten.Producto.Id); 
                    if (producto != null)
                    {
                        producto.Cantidad -= (int) iten.Cantidad; // Reducir el stock
                        if (producto.Cantidad < 0)
                        {
                            // Manejar caso de stock insuficiente
                            TempData["Message"] = $"Stock insuficiente para el producto {producto.Nombre}.";
                            TempData["MessageType"] = "error";
                            return RedirectToAction("Index", "Home");
                        }
                        _context.Productos.Update(producto); // Actualizar el producto
                    }
                }
                // Guardamos los cambios en la base de datos
                await _context.SaveChangesAsync();

                detalles = new List<Detalle>();
               
                TempData["Message"] = "Orden generado exitosamente.";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index","Home");
             }
            catch
            {
                TempData["Message"] = "No fue posible generar la orden.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Carrito", "Home");
            }
        }

        public async Task<IActionResult> ListaOrden() {
            List<Ordene> listaOrdenes = new List<Ordene>();
            //obtenemos ID de usuario mediante sesion
            int? idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            if (idUsuario == null)
            {
                TempData["Message"] = "Debe iniciar sesión para ver sus órdenes.";
                TempData["MessageType"] = "warning";
                return RedirectToAction("Index","Home");
            }
            // Buscamos al usuario en la base de datos
            Usuario usuario = await _context.Usuarios
                .Include(u => u.Ordenes) // Incluye las órdenes asociadas al usuario
                .FirstOrDefaultAsync(u => u.Id == idUsuario);

            if (usuario == null)
            {
                TempData["Message"] = "Usuario no encontrado.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "Home");
            }

            // Verificamos si el usuario es administrador
            if (usuario.Tipo != null && usuario.Tipo.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                // Si es administrador, obtenemos todas las órdenes
                listaOrdenes = await _context.Ordenes
                    .Include(o => o.Usuario) // Incluye los detalles del usuario para mostrar en la vista
                    .ToListAsync();
            }
            else
            {
                // Si no es administrador, solo mostramos las órdenes del usuario
                listaOrdenes = usuario.Ordenes.ToList();
            }

            return View(listaOrdenes);

        }

        public async Task<IActionResult> DetalleOrden(int id) {
            List<Detalle> lista = new List<Detalle>();
            try {
                if (id == null)
                {
                    return NotFound();
                }
                //obtiene detalles oasociados a Orden
                Ordene orden = await _context.Ordenes.Include(u => u.Detalles).FirstOrDefaultAsync(d => d.Id == id);
                Usuario usuario = await _context.Usuarios.FindAsync(orden.UsuarioId);
                if (orden == null && usuario == null)
                {
                    return NotFound();
                }
                lista = orden.Detalles.ToList();
                ViewBag.Usuario = usuario;
                ViewBag.Total = orden.Total;
                return View(lista);
            }
            catch
            {
                TempData["Message"] = "Ocurrio un error al obtener detalle, Intentelo de nuevo mas tarde.";
                TempData["MessageType"] = "warning";
                throw;
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




