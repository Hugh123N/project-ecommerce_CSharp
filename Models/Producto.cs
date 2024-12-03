using System;
using System.Collections.Generic;

namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class Producto
{
    public int Id { get; set; }

    public int Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; }

    public string? Nombre { get; set; }

    public double Precio { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    public virtual Usuario? Usuario { get; set; }
}
