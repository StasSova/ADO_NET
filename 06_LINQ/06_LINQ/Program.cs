namespace LINQ;
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public override string ToString()
    {
        return $"{Name}, {Age}, {City}";
    }
}

class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int DepId { get; set; }
}
class Department
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}

class Good()
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    public override string ToString()
    {
        return $"id:{Id} Title:{Title} Price:{Price} Category:{Category}";
    }
}
internal class Program
{
    static void Main()
    {
        // TASK 1
        //List<Person> person = new List<Person>()
        //{
        //    new Person() {Name = "Andrey", Age = 24, City = "Kyiv"},
        //    new Person(){ Name = "Liza", Age = 18, City = "Odesa" },
        //    new Person(){ Name = "Oleg", Age = 15, City = "London" },
        //    new Person(){ Name = "Sergey", Age = 55, City = "Kyiv" },
        //    new Person(){ Name = "Sergey", Age = 32, City = "Lviv" }
        //};
        //foreach (Person p in person) { Console.WriteLine(p); }
        //Console.WriteLine("------------------------");



        //Console.WriteLine("Выбрать людей, старших 25 лет");
        //foreach (var i in person.Where(x => x.Age > 25)) { Console.WriteLine(i); }
        //Console.WriteLine("\n");
        //var res0 = from p in person
        //           where p.Age > 25
        //           select p;
        //foreach (var i in res0) { Console.WriteLine(i); }
        //Console.WriteLine("------------------------");



        //Console.WriteLine("Выбрать людей, проживающих не в Лондоне");
        //foreach (var i in person.Where(x => x.City != "London")) { Console.WriteLine(i); }
        //Console.WriteLine("\n");
        //var res1 = from p in person
        //           where p.City != "London"
        //           select p;
        //foreach (var i in res1) { Console.WriteLine(i); }
        //Console.WriteLine("------------------------");



        //Console.WriteLine("Выбрать имена людей, проживающих в Киеве.");
        //foreach (var i in person.Where(x => x.City == "Kyiv").Select(x => x.Name)) { Console.WriteLine(i); }
        //Console.WriteLine("\n");
        //var res2 = from p in person
        //           where p.City == "Kyiv"
        //           select p.Name;
        //foreach (var i in res2) { Console.WriteLine(i); }
        //Console.WriteLine("------------------------");



        //Console.WriteLine("Выбрать людей, старших 35 лет, с именем Sergey.");
        //foreach (var i in person.Where(x => x.Name == "Sergey" && x.Age > 35)) { Console.WriteLine(i); }
        //Console.WriteLine("\n");
        //var res3 = from p in person
        //           where p.Age > 35 && p.Name =="Sergey"
        //           select p;
        //foreach (var i in res3) { Console.WriteLine(i); }
        //Console.WriteLine("------------------------");



        //Console.WriteLine("Выбрать людей, проживающих в Одессе.");
        //foreach (var i in person.Where(x => x.City == "Odesa")) { Console.WriteLine(i); }
        //Console.WriteLine("\n");
        //var res4 = from p in person
        //           where p.City == "Odesa"
        //           select p;
        //foreach (var i in res4) { Console.WriteLine(i); }








        // TASK 2
        //List<Department> departments = new List<Department>()
        //{
        //    new Department(){ Id = 1, Country = "Ukraine", City = "Lviv" },
        //    new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
        //    new Department(){ Id = 3, Country = "France", City = "Paris" },
        //    new Department(){ Id = 4, Country = "Ukraine", City = "Odesa" }
        //};
        //List<Employee> employees = new List<Employee>()
        //{
        //    new Employee() { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
        //    new Employee() { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
        //    new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
        //    new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
        //    new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
        //    new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
        //    new Employee() { Id = 7, FirstName = "Nikita", LastName = " Krotov ", Age = 27, DepId = 4 }
        //};

        //Console.WriteLine("Выбрать имена и фамилии сотрудников, работающих в Украине, но не в Одессе");
        //var res5 = employees.Join(departments, empl => empl.DepId, dep => dep.Id, (empl, dep) => new
        //{
        //    empl.FirstName,
        //    empl.LastName,
        //    dep.City,
        //    dep.Country
        //}).Where(x => x.City != "Odesa" && x.Country == "Ukraine");
        //foreach (var r in res5) { Console.WriteLine(r.FirstName + " " + r.LastName); }
        //Console.WriteLine("\n");
        //var res6 = from e in employees
        //           join d in departments on e.DepId equals d.Id
        //           where d.Country == "Ukraine" && d.City != "Odesa"
        //           select new { Name = e.FirstName, LastName = e.LastName };
        //foreach (var r in res6) { Console.WriteLine(r.Name + " " + r.LastName); }
        //Console.WriteLine("------------------------");


        //Console.WriteLine("Вывести список стран без повторений.");
        //foreach (var r in departments.Select(x => x.Country).Distinct()) { Console.WriteLine(r); }
        //Console.WriteLine("\n");
        //foreach (var r in (from d in departments group d by d.Country into g select g.Key)) { Console.WriteLine(r); }
        //Console.WriteLine("------------------------");

        //Console.WriteLine("Выбрать 3-x первых сотрудников, возраст которых превышает 25 лет.");
        //foreach(var r in employees.Where(x=>x.Age>25).Take(3)) { Console.WriteLine(r.FirstName + " " + r.LastName); }
        //Console.WriteLine("\n");
        //foreach (var r in (from e in employees where e.Age > 25 select e).Take(3)) { Console.WriteLine(r.FirstName + " " + r.LastName); }
        //Console.WriteLine("------------------------");

        //Console.WriteLine("Выбрать имена, фамилии и возраст студентов из Киева, возраст которых превышает 21 год");
        //foreach (var e in employees.Join(departments, e => e.DepId, d => d.Id, (e, d) => new {e.FirstName, e.LastName, e.Age, d.City }).Where(e => e.City == "Kyiv" && e.Age > 21)){ Console.WriteLine($"{e.FirstName} {e.LastName} {e.Age} {e.City}"); }
        //Console.WriteLine("\n");
        //foreach (var e in (from e in employees
        //                   join d in departments on e.DepId equals d.Id
        //                   where d.City == "Kyiv" && e.Age > 21
        //                   select new { e.FirstName, e.LastName, e.Age, d.City}))
        //{ Console.WriteLine($"{e.FirstName} {e.LastName} {e.Age} {e.City}"); }
        //Console.WriteLine("------------------------");





        // TASK 3
        List<Department> departments = new List<Department>()
        {
            new Department(){ Id = 1, Country = "Ukraine", City = "Odesa"},
            new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
            new Department(){ Id = 3, Country = "France", City = "Paris" },
            new Department(){ Id = 4, Country = " Ukraine ", City = "Lviv"}
        };
        List<Employee> employees = new List<Employee>()
        {
            new Employee(){ Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
            new Employee() { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
            new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
            new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
            new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
            new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
            new Employee() { Id = 7, FirstName = "Nikita", LastName = "Krotov", Age = 27, DepId = 4 }
        };

        //Console.WriteLine("Упорядочить имена и фамилии сотрудников по алфавиту, которые проживают в Украине. Выполнить запрос немедленно");
        //foreach (var i in employees
        //    .Join(departments, e => e.DepId, d => d.Id, (e, d) => new{ e.FirstName, e.LastName, d.Country})
        //    .Where(x => x.Country == "Ukraine") // фильтрация 
        //    .OrderBy(x => x.FirstName) // сортируем по имени
        //    .ThenBy(x=>x.LastName) // сортируем по фамилии
        //    .ToList()) // выполняем запрос немедленно
        //{ Console.WriteLine(i); }
        //Console.WriteLine("\n");
        //foreach (var i in (from e in employees
        //                  join d in departments on e.DepId equals d.Id
        //                  where d.Country == "Ukraine"
        //                  orderby e.FirstName, e.LastName
        //                  select e))
        //{ Console.WriteLine($"{i.FirstName} {i.LastName}"); }
        //Console.WriteLine("--------------------------------");


        //Console.WriteLine("Отсортировать сотрудников по возрастам по убыванию. Вывести Id, FirstName, LastName, Age. Выполнить запрос немедленно");
        //foreach (var e in employees
        //        .OrderByDescending(e=> e.Age)
        //        .Select(e => new { e.Id, e.FirstName, e.LastName, e.Age })
        //        .ToList())
        //{ Console.WriteLine(e); }
        //Console.WriteLine("\n");
        //foreach (var e in (from e in employees
        //                   orderby e.Age descending
        //                   select new { e.Id, e.FirstName, e.LastName, e.Age }
        //                   ).ToList())
        //{ Console.WriteLine(e); }
        //Console.WriteLine("--------------------------------");

        //Console.WriteLine("Сгруппировать студентов по возрасту. Вывести возраст и сколько раз он встречается в списке.");
        //foreach (var g in employees
        //                    .GroupBy(e=> e.Age)
        //                    .Select(g=> new { Age = g.Key, Count = g.Count()}))
        //{  Console.WriteLine(g); }
        //Console.WriteLine("\n");
        //foreach (var g in from e in employees
        //                  group e by e.Age into g
        //                  select new {Age = g.Key, Count = g.Count()}) 
        //{ Console.WriteLine(g); }
        //Console.WriteLine("-------------------------------------");



        // TASK 4
        List<Good> good = new List<Good>()
        {
            new Good() { Id = 1, Title = "Nokia 1100", Price = 450.99, Category = "Mobile" },
            new Good() { Id = 2, Title = "Iphone 4", Price = 5000, Category = "Mobile" },
            new Good() { Id = 3, Title = "Refregirator 5000", Price = 2555, Category = "Kitchen" },
            new Good() { Id = 4, Title = "Mixer", Price = 150, Category = "Kitchen" },
            new Good() { Id = 5, Title = "Magnitola", Price = 1499, Category = "Car" },
            new Good() { Id = 6, Title = "Samsung Galaxy", Price = 3100, Category = "Mobile" },
            new Good() { Id = 7, Title = "Auto Cleaner", Price = 2300, Category = "Car" },
            new Good() { Id = 8, Title = "Owen", Price = 700, Category = "Kitchen" },
            new Good() { Id = 9, Title = "Siemens Turbo", Price = 3199, Category = "Mobile" },
            new Good() { Id = 10, Title = "Lighter", Price = 150, Category = "Car" }
        };

        //Console.WriteLine("Выбрать товары категории Mobile, цена которых превышает 1000 грн.");
        //foreach (var r in good.Where(x => x.Price > 1000 && x.Category == "Mobile")) 
        //{ Console.WriteLine(r); }
        //Console.WriteLine("\n");
        //foreach (var r in from g in good where g.Price > 1000 && g.Category == "Mobile" select g)
        //{ Console.WriteLine(r); }
        //Console.WriteLine("-----------------------------------");

        //Console.WriteLine("Вывести название и цену тех товаров, которые не относятся к категории Kitchen, цена которых превышает 1000 грн.");
        //foreach (var r in good.Where(x => x.Price > 1000 && x.Category != "Kitchen").Select(x => new { Name = x.Title, Price = x.Price}))
        //{ Console.WriteLine(r); }
        //Console.WriteLine("\n");
        //foreach (var r in from g in good where g.Price > 1000 && g.Category != "Kitchen" select new { Name = g.Title, Price = g.Price })
        //{ Console.WriteLine(r); }
        //Console.WriteLine("-----------------------------------");


        //Console.WriteLine("Вычислить среднее значение всех цен товаров.");
        //Console.WriteLine("Avarage: " +good.Select(x => x.Price).Average());
        //Console.WriteLine("\n");
        //Console.WriteLine("Avarage: " + (from g in good select g.Price).Average());
        //Console.WriteLine("-----------------------------------");

        //Console.WriteLine("Вывести список категорий без повторений.");
        //foreach (var i in good.Select(x=> x.Category).Distinct()) Console.WriteLine(i);
        //Console.WriteLine("\n");
        //foreach (var i in (from g in good select g.Category).Distinct()) Console.WriteLine(i);
        //Console.WriteLine("-----------------------------------");

        //Console.WriteLine("Вывести названия и категории товаров в алфавитном порядке, упорядоченных по названию");
        //foreach (var i in good.Select(x => new {x.Title, x.Category}).OrderBy(x=>x.Title))
        //{ Console.WriteLine($"T: {i.Title} Cat: {i.Category}"); }
        //Console.WriteLine("\n");
        //foreach (var i in from g in good orderby g.Title select new {g.Title, g.Category })
        //{ Console.WriteLine($"T: {i.Title} Cat: {i.Category}"); }
        //Console.WriteLine("-----------------------------------");

        //Console.WriteLine("Посчитать суммарное количество товаров категорий Сar и Mobile.");
        //Console.WriteLine("Count: " + good.Count(g=>g.Category=="Car"||g.Category=="Mobile"));
        //Console.WriteLine("-----------------------------------");

        Console.WriteLine("Вывести список категорий и количество товаров каждой категории");
        var categoryCounts = from g in good
                             group g by g.Category into categoryGroup
                             select new
                             {
                                 Category = categoryGroup.Key,
                                 Count = categoryGroup.Count()
                             };

        foreach (var category in categoryCounts)
        {
            Console.WriteLine($"Category: {category.Category}, Count: {category.Count}");
        }
        Console.WriteLine("-----------------------------------");



    }
}