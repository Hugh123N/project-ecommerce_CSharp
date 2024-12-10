using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_ecommerce_.NET_MVC_.Models;

namespace proyecto_ecommerce_.NET_MVC_.Data
{
    public class proyecto_ecommerce_ : DbContext
    {
        public proyecto_ecommerce_(DbContextOptions<proyecto_ecommerce_> options) : base(options)
        { 
        }

        public DbSet<Usuario> Usuario { get; set; } = default!;

    }
}
