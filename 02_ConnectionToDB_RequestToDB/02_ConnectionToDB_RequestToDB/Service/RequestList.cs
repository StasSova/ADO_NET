using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_ConnectionToDB_RequestToDB.Service
{
    public static class RequestList
    {
        public static string GetAllProducts = "select * from Product";
        public static string GetUniqueColors = "SELECT DISTINCT Color FROM Product;";
        public static string GetMaxCalories = "SELECT * FROM Product WHERE Calories = (SELECT MAX(Calories) FROM Product);";
        public static string GetMinCalories = "SELECT * FROM Product WHERE Calories = (SELECT MIN(Calories) FROM Product);";
        public static string GetAvgCalories = "SELECT TOP 1 * FROM Product ORDER BY ABS(Calories - (SELECT AVG(Calories) FROM Product));";

        public static string GetNumberOfVegateble = "SELECT COUNT(*) AS VegetableCount FROM Product WHERE Type_ID = (SELECT ID FROM TypeOfProduct WHERE Type = 'Vegetable');";
        public static string GetNumberOfFruit = "SELECT COUNT(*) AS FruitCount FROM Product WHERE Type_ID = (SELECT ID FROM TypeOfProduct WHERE Type = 'Fruit');";

        public static string GetAllDownCall = "SELECT * FROM Product WHERE Calories < [CALLORIES_DOWN];";
        public static string GetAllUpCall = "SELECT * FROM Product WHERE Calories > [CALLORIES_UP];";
        public static string GetAllBettwenCall = "SELECT * FROM Product WHERE Calories BETWEEN [CALLORIES_DOWN] AND [CALLORIES_UP];";

    }
}
