using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using proyecto_ecommerce_.NET_MVC_.Controllers;
using System.ComponentModel.DataAnnotations;



namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

    public string? Nombre { get; set; }

    public string? Password { get; set; }

    public string? Telefono { get; set; }

    public string? Tipo { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
