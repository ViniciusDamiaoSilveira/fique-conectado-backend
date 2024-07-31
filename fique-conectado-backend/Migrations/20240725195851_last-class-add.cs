using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fique_conectado_backend.Migrations
{
    public partial class lastclassadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeList",
                table: "rating",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "rating",
                newName: "EntertainmentId");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "rating",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "NumRating",
                table: "rating",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "entertainment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entertainment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "list",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TypeList = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_list", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entertainment");

            migrationBuilder.DropTable(
                name: "list");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "NumRating",
                table: "rating");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "rating",
                newName: "TypeList");

            migrationBuilder.RenameColumn(
                name: "EntertainmentId",
                table: "rating",
                newName: "Name");
        }
    }
}
