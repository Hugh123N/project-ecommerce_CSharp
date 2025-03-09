using System;
using System.Collections.Generic;

namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
