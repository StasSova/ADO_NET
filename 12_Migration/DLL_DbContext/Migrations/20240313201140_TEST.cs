using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class TEST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_FeaturesOfGameM_Game_Games_GamesId",
                table: "M_FeaturesOfGameM_Game");

            migrationBuilder.DropForeignKey(
                name: "FK_M_FeaturesOfGameM_Game_M_FeaturesOfGame_FeaturesOfGameId",
                table: "M_FeaturesOfGameM_Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_M_FeaturesOfGameM_Game",
                table: "M_FeaturesOfGameM_Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_M_FeaturesOfGame",
                table: "M_FeaturesOfGame");

            migrationBuilder.RenameTable(
                name: "M_FeaturesOfGameM_Game",
                newName: "GameFeatures");

            migrationBuilder.RenameTable(
                name: "M_FeaturesOfGame",
                newName: "FeaturesOfGames");

            migrationBuilder.RenameIndex(
                name: "IX_M_FeaturesOfGameM_Game_GamesId",
                table: "GameFeatures",
                newName: "IX_GameFeatures_GamesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FeaturesOfGames",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameFeatures",
                table: "GameFeatures",
                columns: new[] { "FeaturesOfGameId", "GamesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeaturesOfGames",
                table: "FeaturesOfGames",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesOfGames_Name",
                table: "FeaturesOfGames",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameFeatures_FeaturesOfGames_FeaturesOfGameId",
                table: "GameFeatures",
                column: "FeaturesOfGameId",
                principalTable: "FeaturesOfGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameFeatures_Games_GamesId",
                table: "GameFeatures",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameFeatures_FeaturesOfGames_FeaturesOfGameId",
                table: "GameFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_GameFeatures_Games_GamesId",
                table: "GameFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameFeatures",
                table: "GameFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeaturesOfGames",
                table: "FeaturesOfGames");

            migrationBuilder.DropIndex(
                name: "IX_FeaturesOfGames_Name",
                table: "FeaturesOfGames");

            migrationBuilder.RenameTable(
                name: "GameFeatures",
                newName: "M_FeaturesOfGameM_Game");

            migrationBuilder.RenameTable(
                name: "FeaturesOfGames",
                newName: "M_FeaturesOfGame");

            migrationBuilder.RenameIndex(
                name: "IX_GameFeatures_GamesId",
                table: "M_FeaturesOfGameM_Game",
                newName: "IX_M_FeaturesOfGameM_Game_GamesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "M_FeaturesOfGame",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_M_FeaturesOfGameM_Game",
                table: "M_FeaturesOfGameM_Game",
                columns: new[] { "FeaturesOfGameId", "GamesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_M_FeaturesOfGame",
                table: "M_FeaturesOfGame",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_M_FeaturesOfGameM_Game_Games_GamesId",
                table: "M_FeaturesOfGameM_Game",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_M_FeaturesOfGameM_Game_M_FeaturesOfGame_FeaturesOfGameId",
                table: "M_FeaturesOfGameM_Game",
                column: "FeaturesOfGameId",
                principalTable: "M_FeaturesOfGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
