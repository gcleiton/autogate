using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFCE.AutoGate.Infrastructure.Migrations
{
    public partial class AddTrackingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Administrators",
                type: "TIMESTAMP",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Administrators",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Administrators",
                type: "TIMESTAMP",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Administrators",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Administrators");
        }
    }
}
