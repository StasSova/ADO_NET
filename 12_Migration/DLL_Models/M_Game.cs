using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Models
{
    public class M_Game: DbEntity
    {
        public M_Game() { }
        public string Name { get; set; }
        public DateTime Release {  get; set; }
        public virtual ICollection<M_GameStyle> Styles { get; set; }
        public virtual ICollection<M_GameStudio> Studios { get; set; }


        // added new fields
        public int CopiesSold {  get; set; }
        public virtual ICollection<M_FeaturesOfGame> FeaturesOfGame { get; set; }
    }
}

//// Начальная инициализация особенностей игры
//migrationBuilder.InsertData(
//    table: "M_FeaturesOfGame",
//    columns: new[] { "Name" },
//    values: new object[,]
//    {
//                    { "Online Multiplayer" },
//                    { "Cooperative" },
//                    { "Single Player Campaign" },
//                    { "Open World" },
//                    { "Role Playing" },
//        // Добавьте другие особенности здесь
//    });

//// Связи между играми и особенностями
//migrationBuilder.InsertData(
//    table: "M_FeaturesOfGameM_Game",
//    columns: new[] { "FeaturesOfGameId", "GamesId" },
//    values: new object[,]
//    {
//                    // GTA V
//                    { 1, 1 }, // Online Multiplayer
//                    { 2, 1 }, // Cooperative
//                    { 3, 1 }, // Single Player Campaign
//                    { 4, 1 }, // Open World

//                    // The Witcher 3
//                    { 1, 2 }, // Online Multiplayer
//                    { 3, 2 }, // Single Player Campaign
//                    { 4, 2 }, // Open World
//                    { 5, 2 }, // Role Playing

//                    // The Last of Us Part II
//                    { 2, 3 }, // Cooperative
//                    { 3, 3 }, // Single Player Campaign

//                    // Assassin's Creed Valhalla
//                    { 2, 4 }, // Cooperative
//                    { 3, 4 }, // Single Player Campaign
//                    { 4, 4 }, // Open World
//                    { 5, 4 }, // Role Playing

//                    // Skyrim
//                    { 3, 5 }, // Single Player Campaign
//                    { 4, 5 }, // Open World
//                    { 5, 5 }, // Role Playing
//                              // Добавьте другие связи здесь
//    });