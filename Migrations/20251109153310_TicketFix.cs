using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aerolineas.Migrations
{
    /// <inheritdoc />
    public partial class TicketFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Vuelos_VueloId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_VueloId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "VueloId",
                table: "Ticket");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VueloId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_VueloId",
                table: "Ticket",
                column: "VueloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Vuelos_VueloId",
                table: "Ticket",
                column: "VueloId",
                principalTable: "Vuelos",
                principalColumn: "Id");
        }
    }
}
