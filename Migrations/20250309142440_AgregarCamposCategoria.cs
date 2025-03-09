using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyecto_ecommerce_.NET_MVC_.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "categorias",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "categorias");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "categorias");
        }
    }
}
