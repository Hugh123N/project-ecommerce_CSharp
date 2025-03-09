using System;
using System.Collections.Generic;

namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class Envio
{
    public int Id { get; set; }

    public int OrdenId { get; set; }

    public int DireccionId { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaEnvio { get; set; }

    public virtual Direccione Direccion { get; set; } = null!;

    public virtual Ordene Orden { get; set; } = null!;
}
