using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace proyecto_ecommerce_.NET_MVC_.Models;

public partial class EcommerceCursoContext : DbContext
{
    public EcommerceCursoContext()
    {
    }

    public EcommerceCursoContext(DbContextOptions<EcommerceCursoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Detalle> Detalles { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-LNE8K5Q\\SQLEXPRESS; Database=ecommerceCurso; Integrated Security=True; TrustServerCertificate=True");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Detalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalles__3213E83FF32D767A");

            entity.ToTable("detalles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.OrdenId).HasColumnName("orden_id");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Orden).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.OrdenId)
                .HasConstraintName("FKdurdo71oa161lmmal7oeaor74");

            entity.HasOne(d => d.Producto).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FKio4oyl8qt5jclekxp7bwws2iy");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ordenes__3213E83FD55F4B7E");

            entity.ToTable("ordenes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(6)
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaRecivida)
                .HasPrecision(6)
                .HasColumnName("fecha_recivida");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FKsqu43gsd6mtx7b1siww96324");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83FCEB044B9");

            entity.ToTable("productos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Productos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FKo8g0kqq9awvgh4elqai7tdhu");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83FBC78D64E");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
