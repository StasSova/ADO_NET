using _07_EntityFramework_CodeFirst;
using Microsoft.EntityFrameworkCore;

class Program
{

    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Отобразить всю информацию о странах");
            Console.WriteLine("2. Отобразить название стран");
            Console.WriteLine("3. Отобразить название столиц");
            Console.WriteLine("4. Отобразить название всех европейских стран");
            Console.WriteLine("5. Отобразить название стран с площадью, которая больше конкретного числа");
            Console.WriteLine("6. Отобразить все страны, у которых в названии есть буквы a, е");
            Console.WriteLine("7. Отобразить все страны, у которых название начинается с буквы a");
            Console.WriteLine("8. Отобразить название стран, у которых площадь находится в указанном диапазоне");
            Console.WriteLine("9. Отобразить название стран, у которых количество жителей больше указанного числа");
            Console.WriteLine("0. Выход");
            int result = int.Parse(Console.ReadLine()!);
            switch (result)
            {
                case 1:
                    ShowAllCountries();
                    break;
                case 2:
                    ShowAllNameCountries();
                    break;
                case 3:
                    ShowAllCapitalCountries();
                    break;
                case 4:
                    ShowAllNameEuropCountries();
                    break;
                case 5:
                    ShowAllCountriesByArea();
                    break;
                case 6:
                    ShowAllCountriesWithAE();
                    break;
                case 7:
                    ShowAllCountriesStartWithA();
                    break;
                case 8:
                    ShowAllNameCountriesBetweenArea();
                    break;
                case 9:
                    ShowAllCountriesByInhabitants();
                    break;
                case 0:
                    return;
            };
        }
    }
    //• отобразить всю информацию о странах;
    static void ShowAllCountries()
    {
        Console.Clear();
        using (var db = new CountryDbContext())
        {
            var query = (from c in db.Countries select c).ToArray();
            Console.WindowWidth = 150;
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|   ID   |              Name              |          Capital          |    Population    |     Area    |  Part of the World |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"| {c.Id,-6} | {c.Name,-30} | {c.Capital,-25} | {c.NumberOfInhabitants,-15} | {c.CountryArea,-11} | {c.PartOfTheWorld?.Name,-20} |");
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
        }
        Console.ReadKey();
    }
    //• отобразить название стран;
    static void ShowAllNameCountries()
    {
        Console.Clear();
        using (var db = new CountryDbContext())
        {
            var query = (from c in db.Countries select c.Name).ToArray();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|              Name                    |");
            Console.WriteLine("----------------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"| {c,-36} |");
            }
            Console.WriteLine("----------------------------------------");
        }
        Console.ReadKey();
    }
    //• отобразить название столиц;
    static void ShowAllCapitalCountries()
    {
        Console.Clear();
        using (var db = new CountryDbContext())
        {
            var query = (from c in db.Countries select c.Capital).ToArray();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|              Capital                 |");
            Console.WriteLine("----------------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"| {c,-36} |");
            }
            Console.WriteLine("----------------------------------------");
        }
        Console.ReadKey();
    }
    //• отобразить название всех европейских стран;
    static void ShowAllNameEuropCountries()
    {
        Console.Clear();
        using (var db = new CountryDbContext())
        {
            var europeanCountries = db.Countries
                            .Where(c => c.PartOfTheWorld.Name == "Europe")
                            .Select(c => c.Name)
                            .ToList();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|              Name                    |");
            Console.WriteLine("----------------------------------------");
            foreach (var c in europeanCountries)
            {
                Console.WriteLine($"| {c,-36} |");
            }
            Console.WriteLine("----------------------------------------");
        }
        Console.ReadKey();
    }
    //• отобразить название стран с площадью, которая больше конкретного числа.
    static void ShowAllCountriesByArea()
    {
        Console.Clear();
        Console.Write("Display countries with area greater than: ");
        int area = int.Parse(Console.ReadLine()!);
        using (var db = new CountryDbContext())
        {
            var query = (from c in db.Countries where c.CountryArea >= area select c).ToArray();
            Console.WindowWidth = 150;
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|   ID   |              Name              |          Capital          |    Population    |     Area    |  Part of the World |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"| {c.Id,-6} | {c.Name,-30} | {c.Capital,-25} | {c.NumberOfInhabitants,-15} | {c.CountryArea,-11} | {c.PartOfTheWorld?.Name,-20} |");
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
        }
        Console.ReadKey();
    }

    //• отобразить все страны, у которых в названии есть буквы a, е;
    static void ShowAllCountriesWithAE()
    {
        Console.Clear();
        using (var db = new CountryDbContext())
        {
            var query = db.Countries
                        .Where(c => EF.Functions.Like(c.Name, "%a%") || EF.Functions.Like(c.Name, "%e%"))
                        .ToArray();

            Console.WindowWidth = 150;
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|   ID   |              Name              |          Capital          |    Population    |     Area    |  Part of the World |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"| {c.Id,-6} | {c.Name,-30} | {c.Capital,-25} | {c.NumberOfInhabitants,-15} | {c.CountryArea,-11} | {c.PartOfTheWorld?.Name,-20} |");
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
        }
        Console.ReadKey();
    }
    //• отобразить все страны, у которых название начинается с буквы a;
    static void ShowAllCountriesStartWithA()
    {
        Console.Clear();
        using (var db = new CountryDbContext())
        {
            var query = db.Countries
                        .Where(c => EF.Functions.Like(c.Name, "A%"))
                        .ToArray();

            Console.WindowWidth = 150;
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|   ID   |              Name              |          Capital          |    Population    |     Area    |  Part of the World |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"| {c.Id,-6} | {c.Name,-30} | {c.Capital,-25} | {c.NumberOfInhabitants,-15} | {c.CountryArea,-11} | {c.PartOfTheWorld?.Name,-20} |");
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
        }
        Console.ReadKey();
    }
    //• отобразить название стран, у которых площадь находится в указанном диапазоне;
    static void ShowAllNameCountriesBetweenArea()
    {
        Console.Clear();
        Console.Write("Display countries with area greater than: ");
        int min = int.Parse(Console.ReadLine()!);
        Console.Write("\nAnd less than: ");
        int max = int.Parse(Console.ReadLine()!);

        (min,max) = min > max ? (max,min) : (min, max);
        using (var db = new CountryDbContext())
        {
            var query = (from c in db.Countries where c.CountryArea >= min && c.CountryArea <= max select c.Name).ToArray();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|              Name                    |");
            Console.WriteLine("----------------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"| {c,-36} |");
            }
            Console.WriteLine("----------------------------------------");
        }
        Console.ReadKey();
    }
    //• отобразить название стран, у которых количество жителей больше указанного числа.
    static void ShowAllCountriesByInhabitants()
    {
        Console.Clear();
        Console.Write("Display countries with inhabitants greater than: ");
        int area = int.Parse(Console.ReadLine()!);
        using (var db = new CountryDbContext())
        {
            var query = (from c in db.Countries where c.NumberOfInhabitants >= area select c).ToArray();
            Console.WindowWidth = 150;
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|   ID   |              Name              |          Capital          |    Population    |     Area    |  Part of the World |");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
            foreach (var c in query)
            {
                Console.WriteLine($"| {c.Id,-6} | {c.Name,-30} | {c.Capital,-25} | {c.NumberOfInhabitants,-15} | {c.CountryArea,-11} | {c.PartOfTheWorld?.Name,-20} |");
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
        }
        Console.ReadKey();
    }
}