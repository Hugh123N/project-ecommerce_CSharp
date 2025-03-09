using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto_ecommerce_.NET_MVC_.Models;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
