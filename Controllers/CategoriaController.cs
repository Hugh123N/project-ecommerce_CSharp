using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto_ecommerce_.NET_MVC_.Models;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    [Authorize(AuthenticationSchemes =CookieAuthenticationDefaults.AuthenticationScheme ,Roles = "admin")]
    public class CategoriaController : BaseController
    {
        private readonly EcommerceNetContext _context;

        public CategoriaController(EcommerceNetContext ecommerceNetContext)
        {
            _context = ecommerceNetContext;
        }

        // GET: CategoriaController
        public async Task<IActionResult> Index(string buscar)
        {
            var categorias = from p in _context.Categorias select p;

            if (!String.IsNullOrEmpty(buscar))
            {
                categorias = categorias.Where(p => p.Nombre!.Contains(buscar));
            }

            return View(await categorias.ToListAsync());
        }

        // GET: CategoriaController/Details/5
        public async Task<IActionResult> Estado(int id)
        {
            try {
                var cat = await _context.Categorias.FindAsync(id);
                if (cat == null)
                    return NotFound();
                if(cat.Estado == "Activo") { 
                    cat.Estado = "Desactivado";
                }else
                    cat.Estado = "Activo";

                //guardamos en la BD
                _context.Update(cat);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            } catch {
                //mensaje de error que se mostrara en la vista con Toastr.js
                TempData["Message"] = "Ocurrió un error al actualizar el Estado.";
                TempData["MessageType"] = "error"; // success, error, info, warning
                throw;
            }
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile img, Categoria categoria)
        {
            try
            {
                if (img != null && img.Length > 0)
                {
                    // Genera un nombre único para el archivo
                    var fileName = categoria.Nombre + Path.GetExtension(img.FileName);

                    // Ruta donde se guardará la imagen
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    // Crea la carpeta "images" si no existe
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));

                    // Guarda la imagen en la carpeta
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    // Guarda la ruta relativa en el modelo
                    categoria.Imagen = $"/images/{fileName}";
                }
                categoria.Estado = "Activo";

                _context.Add(categoria);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Producto creado exitosamente.";
                TempData["MessageType"] = "success"; // success, error, info, warning

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Ocurrió un error al crear la categoria.";
                TempData["MessageType"] = "error";
                throw;
            }
        }

        // GET: CategoriaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categoria cat, IFormFile img)
        {
            try
            {
                if (cat.Id == null)
                    return NotFound();
                // Obtenemos el categoria original para manejar la imagen
                var catOriginal = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c=> c.Id==cat.Id);
                if (catOriginal == null)
                    return NotFound();

                if(img != null && img.Length > 0)
                {
                    // Generamos un nuevo nombre para la imagen
                    var fileName = cat.Nombre + Path.GetExtension(img.FileName);
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
                    if (!string.IsNullOrEmpty(catOriginal.Imagen))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", catOriginal.Imagen.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Actualizamos la ruta de la imagen en el producto
                    cat.Imagen = $"/images/{fileName}";
                }else{
                    // Si no se sube una nueva imagen, mantenemos la existente
                    cat.Imagen = catOriginal.Imagen;
                }

                cat.Estado = catOriginal.Estado;
                // Actualizamos la categoria en la base de datos
                _context.Categorias.Update(cat);
                await _context.SaveChangesAsync();

                //mensaje de exito que se mostrara en la vista con Toastr.js
                TempData["Message"] = "Categoria actualizado exitosamente.";
                TempData["MessageType"] = "success"; // success, error, info, warning

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //mensaje de error que se mostrara en la vista con Toastr.js
                TempData["Message"] = "Ocurrió un error al actualizar la categoria.";
                TempData["MessageType"] = "error"; // success, error, info, warning
                throw;
            }
        }

    }
}
