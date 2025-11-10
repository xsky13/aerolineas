using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aerolineas.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeatToticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AsientoId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AsientoId",
                table: "Tickets",
                column: "AsientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Asientos_AsientoId",
                table: "Tickets",
                column: "AsientoId",
                principalTable: "Asientos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Asientos_AsientoId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AsientoId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AsientoId",
                table: "Tickets");
        }
    }
}
