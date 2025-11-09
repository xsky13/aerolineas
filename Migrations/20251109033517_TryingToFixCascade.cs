using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aerolineas.Migrations
{
    /// <inheritdoc />
    public partial class TryingToFixCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vuelos_Slots_SlotId",
                table: "Vuelos");

            migrationBuilder.AddForeignKey(
                name: "FK_Vuelos_Slots_SlotId",
                table: "Vuelos",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vuelos_Slots_SlotId",
                table: "Vuelos");

            migrationBuilder.AddForeignKey(
                name: "FK_Vuelos_Slots_SlotId",
                table: "Vuelos",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id");
        }
    }
}
