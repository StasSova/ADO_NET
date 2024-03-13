using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class Added_NewFeaturesOfGame_SoldCopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CopiesSold",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "M_FeaturesOfGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_FeaturesOfGame", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "M_FeaturesOfGameM_Game",
                columns: table => new
                {
                    FeaturesOfGameId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_FeaturesOfGameM_Game", x => new { x.FeaturesOfGameId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_M_FeaturesOfGameM_Game_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_FeaturesOfGameM_Game_M_FeaturesOfGame_FeaturesOfGameId",
                        column: x => x.FeaturesOfGameId,
                        principalTable: "M_FeaturesOfGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_FeaturesOfGameM_Game_GamesId",
                table: "M_FeaturesOfGameM_Game",
                column: "GamesId");



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_FeaturesOfGameM_Game");

            migrationBuilder.DropTable(
                name: "M_FeaturesOfGame");

            migrationBuilder.DropColumn(
                name: "CopiesSold",
                table: "Games");
        }
    }
}
