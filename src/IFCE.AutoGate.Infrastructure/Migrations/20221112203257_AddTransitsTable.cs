using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFCE.AutoGate.Infrastructure.Migrations
{
    public partial class AddTransitsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransitTypeId = table.Column<int>(type: "integer", nullable: false),
                    TransitDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transits_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transits_TransitTypes_TransitTypeId",
                        column: x => x.TransitTypeId,
                        principalTable: "TransitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transits_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transits_DriverId",
                table: "Transits",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transits_TransitTypeId",
                table: "Transits",
                column: "TransitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transits_VehicleId",
                table: "Transits",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transits");

            migrationBuilder.DropTable(
                name: "TransitTypes");
        }
    }
}
