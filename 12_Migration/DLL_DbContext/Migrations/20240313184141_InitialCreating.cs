using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Release = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStudios",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    StudiosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStudios", x => new { x.GamesId, x.StudiosId });
                    table.ForeignKey(
                        name: "FK_GameStudios_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStudios_Studios_StudiosId",
                        column: x => x.StudiosId,
                        principalTable: "Studios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameStyles",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    StylesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStyles", x => new { x.GamesId, x.StylesId });
                    table.ForeignKey(
                        name: "FK_GameStyles_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStyles_Styles_StylesId",
                        column: x => x.StylesId,
                        principalTable: "Styles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStudios_StudiosId",
                table: "GameStudios",
                column: "StudiosId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStyles_StylesId",
                table: "GameStyles",
                column: "StylesId");

            migrationBuilder.CreateIndex(
                name: "IX_Styles_Name",
                table: "Styles",
                column: "Name",
                unique: true);


            // Начальная инициализация данных
            migrationBuilder.InsertData(
                table: "Styles",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Action" },
                    { "Adventure" },
                    { "RPG" },
                    { "Strategy" },
                    { "Simulation" },
                    // Добавьте другие жанры здесь
                });

            migrationBuilder.InsertData(
                table: "Studios",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Rockstar" },
                    { "Naughty Dog" },
                    { "CDPR" },
                    { "Ubisoft" },
                    { "Bethesda" },
                    // Другие студии здесь
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "Release" },
                values: new object[,]
                {
                    { "GTA V", new DateTime(2013, 9, 17) },
                    { "The Witcher 3", new DateTime(2015, 5, 19) },
                    { "The Last of Us Part II", new DateTime(2020, 6, 19) },
                    { "Assassin's Creed Valhalla", new DateTime(2020, 11, 10) },
                    { "Skyrim", new DateTime(2011, 11, 11) },
                    // Добавьте другие игры здесь
                });

            // Связи между играми и студиями
            migrationBuilder.InsertData(
                table: "GameStudios",
                columns: new[] { "GamesId", "StudiosId" },
                values: new object[,]
                {
                    { 1, 1 }, // GTA V - Rockstar
                    { 2, 3 }, // The Witcher 3 - CDPR
                    { 3, 2 }, // The Last of Us Part II - Naughty Dog
                    { 4, 4 }, // Assassin's Creed Valhalla - Ubisoft
                    { 5, 5 }, // Skyrim - Bethesda
                  // Добавьте другие связи здесь
                });

            // Связи между играми и жанрами
            migrationBuilder.InsertData(
                table: "GameStyles",
                columns: new[] { "GamesId", "StylesId" },
                values: new object[,]
                {
                    { 1, 1 }, // GTA V - Action
                    { 2, 3 }, // The Witcher 3 - RPG
                    { 3, 1 }, // The Last of Us Part II - Action
                    { 4, 4 }, // Assassin's Creed Valhalla - Strategy
                    { 5, 2 }, // Skyrim - Adventure
                    // Добавьте другие связи здесь
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStudios");

            migrationBuilder.DropTable(
                name: "GameStyles");

            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Styles");
        }
    }
}
