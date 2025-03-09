using System;
using System.Collections.Generic;

namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public double Precio { get; set; }

    public int Cantidad { get; set; }

    public string? Imagen { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaActualizacion { get; set; }

    public int? CategoriaId { get; set; }

    public int UsuarioId { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual Usuario Usuario { get; set; } = null!;
}
