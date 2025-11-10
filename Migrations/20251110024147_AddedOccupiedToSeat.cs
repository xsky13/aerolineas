using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aerolineas.Migrations
{
    /// <inheritdoc />
    public partial class AddedOccupiedToSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ocupado",
                table: "Asientos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ocupado",
                table: "Asientos");
        }
    }
}
