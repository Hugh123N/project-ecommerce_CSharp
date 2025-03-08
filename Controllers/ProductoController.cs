using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto_ecommerce_.NET_MVC_.Models;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    public class ProductoController : BaseController
    {
        private readonly EcommerceCursoContext _context;

        public ProductoController(EcommerceCursoContext ecommerceCursoContext)
        {
            _context =ecommerceCursoContext;
        }

        [Authorize(Roles = "admin")]
        // GET: ProductoController
        public async Task<IActionResult> Index(string buscar)
        {
            var productos= from p in _context.Productos select p; 

            if (!String.IsNullOrEmpty(buscar))
            {
                productos = productos.Where(p => p.Nombre!.Contains(buscar));
            }

            return View(await productos.ToListAsync());
        }


        // GET: ProductoController/Details/5
        public async Task<IActionResult> Detail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
         
            return View(producto);
        }

		[Authorize(Roles = "admin")]
		// GET: ProductoController/Create
		public ActionResult Create()
        {   
            return View();
        }


		// POST: ProductoController/Create
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile img, Producto producto)
        {
            try
            {
                    if(img != null && img.Length > 0)
                    {
                        // Genera un nombre único para el archivo
                        var fileName = producto.Nombre + Path.GetExtension(img.FileName);

                        // Ruta donde se guardará la imagen
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        // Crea la carpeta "img" si no existe
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));

                        // Guarda la imagen en la carpeta
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(stream);
                        }

                        // Guarda la ruta relativa en el modelo
                        producto.Imagen = $"/images/{fileName}";
                    }
                //obtiene id de usuario desde el cookies
                string? idClaim = User.FindFirstValue("UsuarioCod");
                if (idClaim == null)
                {
                    TempData["Message"] = "Administrador no exise.";
                    TempData["MessageType"] = "warning";
                    return RedirectToAction("Login", "Login");
                }
                int? idUsu = int.Parse(idClaim);

                Usuario usuario = await _context.Usuarios.FindAsync(idUsu);
                if (usuario == null) {
                    return NotFound();
                }
                producto.Usuario = usuario;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                
                TempData["Message"] = "Producto creado exitosamente.";
                TempData["MessageType"] = "success"; // success, error, info, warning

                return RedirectToAction(nameof(Index));
                
            }
            catch
            {
                TempData["Message"] = "Ocurrió un error al crear el producto.";
                TempData["MessageType"] = "error";
                throw;
            }
        }

		// GET: ProductoController/Edit/5
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            return View(producto);
        }

		// POST: ProductoController/Edit/5
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto, IFormFile img)
        {
            if (producto.Id == null)
            {
                return NotFound();
            }
            // Obtenemos el producto original para manejar la imagen
            var productoOriginal = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (productoOriginal == null)
            {
                return NotFound();
            }

            // Si se sube una nueva imagen
            if (img != null && img.Length > 0)
            {
                // Generamos un nuevo nombre para la imagen
                var fileName = producto.Nombre + Path.GetExtension(img.FileName);

                // Ruta donde se guardará la nueva imagen
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                // Crea la carpeta "img" si no existe
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));

                // Guardamos la nueva imagen
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }

                // Eliminamos la imagen anterior del disco (si existe)
                if (!string.IsNullOrEmpty(productoOriginal.Imagen))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", productoOriginal.Imagen.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Actualizamos la ruta de la imagen en el producto
                producto.Imagen = $"/images/{fileName}";
            }
            else
            {
                // Si no se sube una nueva imagen, mantenemos la existente
                producto.Imagen = productoOriginal.Imagen;
            }
            // Actualizamos el producto en la base de datos
            try
            {
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                //mensaje de exito que se mostrara en la vista con Toastr.js
                TempData["Message"] = "Producto actualizado exitosamente.";
                TempData["MessageType"] = "success"; // success, error, info, warning
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateConcurrencyException)
            {
                //mensaje de error que se mostrara en la vista con Toastr.js
                TempData["Message"] = "Ocurrió un error al actualizar el producto.";
                TempData["MessageType"] = "error"; // success, error, info, warning
                throw;
            }
        }

		// POST: ProductoController/Delete/5
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, IFormCollection collection)
        {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    return NotFound();
                }

                bool estaEnUso = await _context.Detalles.AnyAsync(d => d.ProductoId == id && d.OrdenId != null);

            if (estaEnUso)
            {
                TempData["Message"] = "No se puede eliminar, el producto está en uso."; 
                TempData["MessageType"] = "info"; // success, error, info, warning
                return RedirectToAction(nameof(Index));
            }

            //elimina la imagen de la carpeta
            if (!string.IsNullOrEmpty(producto.Imagen))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", producto.Imagen.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

          
                //elimina el producto de la base de datos
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                //mensaje de exito que se mostrara en la vista con Toastr.js
                TempData["Message"] = "Producto eliminado exitosamente.";
                TempData["MessageType"] = "success"; // success, error, info, warning    

                return RedirectToAction(nameof(Index));
        }
    }
}
