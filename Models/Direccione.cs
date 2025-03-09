using System;
using System.Collections.Generic;

namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class Direccione
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Direccion { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();

    public virtual Usuario Usuario { get; set; } = null!;
}
