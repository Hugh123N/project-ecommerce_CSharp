﻿using Microsoft.AspNetCore.Mvc;
using proyecto_ecommerce_.NET_MVC_.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Login(UsuarioVM modelo)
        {
            Usuario? usuario_encontrado = await _context.Usuarios.Where(u => u.Username == modelo.username && u.Password == modelo.password).FirstOrDefaultAsync();

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No existe usuario o contraseña";
                return View();
            }


            TempData["SuccessMessage"] = "Inicio de sesión exitoso.";
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

            if (string.IsNullOrEmpty(modelo.direccion) || !Regex.IsMatch(modelo.direccion, @"^[a-zA-Z0-9\s,.-]+$"))
            {
                ViewData["ErrorMessage"] = "La dirección no es válida o está vacía";
                return View();
            }
            /*usar validaciones backend primero*/



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
