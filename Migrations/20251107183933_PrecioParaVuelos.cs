using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aerolineas.Migrations
{
    /// <inheritdoc />
    public partial class PrecioParaVuelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Precio",
                table: "Vuelos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Vuelos");
        }
    }
}
