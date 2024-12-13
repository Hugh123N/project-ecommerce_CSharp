using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using proyecto_ecommerce_.NET_MVC_.Controllers;
using System.ComponentModel.DataAnnotations;

namespace proyecto_ecommerce_.NET_MVC_.ViewModels
{
    public class UsuarioVM
    {
        public string direccion {  get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string repetirPassword { get; set; }

    }
}
