﻿using System;
using System.Collections.Generic;

namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class Ordene
{
    public int Id { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaRecivida { get; set; }

    public string? Numero { get; set; }

    public double Total { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    public virtual Usuario? Usuario { get; set; }
}