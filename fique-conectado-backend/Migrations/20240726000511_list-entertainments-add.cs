using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fique_conectado_backend.Migrations
{
    public partial class listentertainmentsadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "listEntertainments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntertainmentId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listEntertainments", x => x.id);
                    table.ForeignKey(
                        name: "FK_listEntertainments_entertainment_EntertainmentId",
                        column: x => x.EntertainmentId,
                        principalTable: "entertainment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_listEntertainments_list_ListId",
                        column: x => x.ListId,
                        principalTable: "list",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rating_EntertainmentId",
                table: "rating",
                column: "EntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_rating_UserId",
                table: "rating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_list_UserId",
                table: "list",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_listEntertainments_EntertainmentId",
                table: "listEntertainments",
                column: "EntertainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_listEntertainments_ListId",
                table: "listEntertainments",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_list_user_UserId",
                table: "list",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rating_entertainment_EntertainmentId",
                table: "rating",
                column: "EntertainmentId",
                principalTable: "entertainment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rating_user_UserId",
                table: "rating",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_list_user_UserId",
                table: "list");

            migrationBuilder.DropForeignKey(
                name: "FK_rating_entertainment_EntertainmentId",
                table: "rating");

            migrationBuilder.DropForeignKey(
                name: "FK_rating_user_UserId",
                table: "rating");

            migrationBuilder.DropTable(
                name: "listEntertainments");

            migrationBuilder.DropIndex(
                name: "IX_rating_EntertainmentId",
                table: "rating");

            migrationBuilder.DropIndex(
                name: "IX_rating_UserId",
                table: "rating");

            migrationBuilder.DropIndex(
                name: "IX_list_UserId",
                table: "list");
        }
    }
}
