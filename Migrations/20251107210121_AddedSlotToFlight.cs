using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aerolineas.Migrations
{
    /// <inheritdoc />
    public partial class AddedSlotToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightCode",
                table: "Vuelos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Runway = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GateId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vuelos_SlotId",
                table: "Vuelos",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vuelos_Slot_SlotId",
                table: "Vuelos",
                column: "SlotId",
                principalTable: "Slot",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vuelos_Slot_SlotId",
                table: "Vuelos");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropIndex(
                name: "IX_Vuelos_SlotId",
                table: "Vuelos");

            migrationBuilder.DropColumn(
                name: "FlightCode",
                table: "Vuelos");
        }
    }
}
