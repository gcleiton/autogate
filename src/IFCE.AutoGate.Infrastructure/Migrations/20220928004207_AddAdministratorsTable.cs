using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFCE.AutoGate.Infrastructure.Migrations
{
    public partial class AddAdministratorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RecoveryPasswordCode = table.Column<Guid>(type: "uuid", maxLength: 256, nullable: true),
                    RecoveryPasswordExpiresAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_Email",
                table: "Administrators",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");
        }
    }
}
