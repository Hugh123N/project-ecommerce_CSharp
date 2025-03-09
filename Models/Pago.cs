using System;
using System.Collections.Generic;

namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int OrdenId { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime? FechaPago { get; set; }

    public virtual Ordene Orden { get; set; } = null!;
}
