using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aerolineas.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeatToReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AsientoId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_AsientoId",
                table: "Reservas",
                column: "AsientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Asientos_AsientoId",
                table: "Reservas",
                column: "AsientoId",
                principalTable: "Asientos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Asientos_AsientoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_AsientoId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "AsientoId",
                table: "Reservas");
        }
    }
}
