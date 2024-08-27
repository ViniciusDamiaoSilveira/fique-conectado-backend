using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fique_conectado_backend.Migrations
{
    public partial class AtualizacaoCadastro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entertainment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApiId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Release = table.Column<string>(type: "TEXT", nullable: false),
                    PathPoster = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entertainment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_list_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntertainmentId = table.Column<string>(type: "TEXT", nullable: false),
                    NumRating = table.Column<float>(type: "REAL", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EntertainmentId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rating_entertainment_EntertainmentId1",
                        column: x => x.EntertainmentId1,
                        principalTable: "entertainment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_rating_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "listEntertainments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntertainmentId = table.Column<string>(type: "TEXT", nullable: false),
                    EntertainmentId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listEntertainments", x => x.id);
                    table.ForeignKey(
                        name: "FK_listEntertainments_entertainment_EntertainmentId1",
                        column: x => x.EntertainmentId1,
                        principalTable: "entertainment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_listEntertainments_list_ListId",
                        column: x => x.ListId,
                        principalTable: "list",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_list_UserId",
                table: "list",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_listEntertainments_EntertainmentId1",
                table: "listEntertainments",
                column: "EntertainmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_listEntertainments_ListId",
                table: "listEntertainments",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_rating_EntertainmentId1",
                table: "rating",
                column: "EntertainmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_rating_UserId",
                table: "rating",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "listEntertainments");

            migrationBuilder.DropTable(
                name: "rating");

            migrationBuilder.DropTable(
                name: "list");

            migrationBuilder.DropTable(
                name: "entertainment");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
