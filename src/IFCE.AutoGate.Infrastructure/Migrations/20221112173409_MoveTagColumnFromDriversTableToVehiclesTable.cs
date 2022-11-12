using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFCE.AutoGate.Infrastructure.Migrations
{
    public partial class MoveTagColumnFromDriversTableToVehiclesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_Email_Tag",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Vehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate_Tag",
                table: "Vehicles",
                columns: new[] { "Plate", "Tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Email",
                table: "Drivers",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Plate_Tag",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_Email",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Drivers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Email_Tag",
                table: "Drivers",
                columns: new[] { "Email", "Tag" },
                unique: true);
        }
    }
}
