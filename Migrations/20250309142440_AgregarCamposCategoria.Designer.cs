﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using proyecto_ecommerce_.NET_MVC_.Models;

#nullable disable

namespace proyecto_ecommerce_.NET_MVC_.Migrations
{
    [DbContext(typeof(EcommerceNetContext))]
    [Migration("20250309142440_AgregarCamposCategoria")]
    partial class AgregarCamposCategoria
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Carrito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id")
                        .HasName("PK__carrito__3213E83F8194984F");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("carrito", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre");

                    b.HasKey("Id")
                        .HasName("PK__categori__3213E83F6A3F39BF");

                    b.ToTable("categorias", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Detalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<int>("OrdenId")
                        .HasColumnType("int")
                        .HasColumnName("orden_id");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("precio");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("total");

                    b.HasKey("Id")
                        .HasName("PK__detalles__3213E83FF65F4D6E");

                    b.HasIndex("OrdenId");

                    b.HasIndex("ProductoId");

                    b.ToTable("detalles", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Direccione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ciudad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("ciudad");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("direccion");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("estado");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id")
                        .HasName("PK__direccio__3213E83FA0380991");

                    b.HasIndex("UsuarioId");

                    b.ToTable("direcciones", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Envio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DireccionId")
                        .HasColumnType("int")
                        .HasColumnName("direccion_id");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValue("pendiente")
                        .HasColumnName("estado");

                    b.Property<DateTime?>("FechaEnvio")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_envio");

                    b.Property<int>("OrdenId")
                        .HasColumnType("int")
                        .HasColumnName("orden_id");

                    b.HasKey("Id")
                        .HasName("PK__envios__3213E83F9585DC06");

                    b.HasIndex("DireccionId");

                    b.HasIndex("OrdenId");

                    b.ToTable("envios", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Favorito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id")
                        .HasName("PK__favorito__3213E83F6FAF6CCF");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("favoritos", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Ordene", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaCreacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_creacion")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("FechaRecivida")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_recivida");

                    b.Property<string>("Numero")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("numero");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("total");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id")
                        .HasName("PK__ordenes__3213E83F05DC53DD");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ordenes", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Pago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValue("pendiente")
                        .HasColumnName("estado");

                    b.Property<DateTime?>("FechaPago")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_pago");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("metodo_pago");

                    b.Property<int>("OrdenId")
                        .HasColumnType("int")
                        .HasColumnName("orden_id");

                    b.HasKey("Id")
                        .HasName("PK__pagos__3213E83F80B56071");

                    b.HasIndex("OrdenId");

                    b.ToTable("pagos", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int")
                        .HasColumnName("categoria_id");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("descripcion");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasDefaultValue("activo")
                        .HasColumnName("estado");

                    b.Property<DateTime>("FechaActualizacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha_actualizacion")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Imagen")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("imagen");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("precio");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id")
                        .HasName("PK__producto__3213E83FE8F8EE79");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("productos", null, t =>
                        {
                            t.HasTrigger("trg_CambiarEstadoProducto");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("direccion");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Telefono")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("telefono");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tipo");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("PK__usuarios__3213E83F64B1091A");

                    b.HasIndex(new[] { "Email" }, "UQ__usuarios__AB6E61645B15D4AE")
                        .IsUnique();

                    b.ToTable("usuarios", (string)null);
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Carrito", b =>
                {
                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Producto", "Producto")
                        .WithMany("Carritos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__carrito__product__5812160E");

                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Usuario", "Usuario")
                        .WithMany("Carritos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__carrito__usuario__571DF1D5");

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Detalle", b =>
                {
                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Ordene", "Orden")
                        .WithMany("Detalles")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__detalles__orden___5EBF139D");

                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Producto", "Producto")
                        .WithMany("Detalles")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__detalles__produc__5FB337D6");

                    b.Navigation("Orden");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Direccione", b =>
                {
                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Usuario", "Usuario")
                        .WithMany("Direcciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__direccion__usuar__4CA06362");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Envio", b =>
                {
                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Direccione", "Direccion")
                        .WithMany("Envios")
                        .HasForeignKey("DireccionId")
                        .IsRequired()
                        .HasConstraintName("FK__envios__direccio__6D0D32F4");

                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Ordene", "Orden")
                        .WithMany("Envios")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__envios__orden_id__6C190EBB");

                    b.Navigation("Direccion");

                    b.Navigation("Orden");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Favorito", b =>
                {
                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Producto", "Producto")
                        .WithMany("Favoritos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__favoritos__produ__70DDC3D8");

                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Usuario", "Usuario")
                        .WithMany("Favoritos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__favoritos__usuar__6FE99F9F");

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Ordene", b =>
                {
                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Usuario", "Usuario")
                        .WithMany("Ordenes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__ordenes__usuario__5BE2A6F2");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Pago", b =>
                {
                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Ordene", "Orden")
                        .WithMany("Pagos")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__pagos__orden_id__6383C8BA");

                    b.Navigation("Orden");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Producto", b =>
                {
                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__productos__categ__5441852A");

                    b.HasOne("proyecto_ecommerce_.NET_MVC_.Models.Usuario", "Usuario")
                        .WithMany("Productos")
                        .HasForeignKey("UsuarioId")
                        .IsRequired()
                        .HasConstraintName("FK__productos__usuar__534D60F1");

                    b.Navigation("Categoria");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Direccione", b =>
                {
                    b.Navigation("Envios");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Ordene", b =>
                {
                    b.Navigation("Detalles");

                    b.Navigation("Envios");

                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Producto", b =>
                {
                    b.Navigation("Carritos");

                    b.Navigation("Detalles");

                    b.Navigation("Favoritos");
                });

            modelBuilder.Entity("proyecto_ecommerce_.NET_MVC_.Models.Usuario", b =>
                {
                    b.Navigation("Carritos");

                    b.Navigation("Direcciones");

                    b.Navigation("Favoritos");

                    b.Navigation("Ordenes");

                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
