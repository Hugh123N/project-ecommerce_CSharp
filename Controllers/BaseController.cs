using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.CarritoCount = HomeController.detalles.Count;
            base.OnActionExecuted(context);
        }
    }
}
