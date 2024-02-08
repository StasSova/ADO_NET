use master 
go
if db_id('Tourist_Agency') is not null
	drop database [Tourist_Agency]
go

create database [Tourist_Agency]
go
use [Tourist_Agency]
go

-- тип транспорта, которым можно добраться куда нибудь
create table TypeOfVehicles(
	[Id] int primary key identity(1,1),
	[Type] nvarchar(30) not null unique
)
go
-- тип отдыха
create table TypeOfHoliday(
	[Id] int primary key identity(1,1),
	[Type] nvarchar(30) not null unique
)
go

-- страна
create table Country(
	[Id] int primary key identity(1,1),
	[Name] nvarchar(30) not null unique,
)
go

-- город
create table City(
	[Id] int primary key identity(1,1),
	[Name] nvarchar(30) not null,
	[Country_ID] int foreign key references Country(Id),
)
go

-- способ путешенствия от одной страны, к другой
create table WayOfTravel(
	[Id] int primary key identity(1,1),
	[From_City_ID] int foreign key references City(ID),
	[To_City_ID] int foreign key references City(ID),
	[Type_Of_Vehicles] int foreign key references TypeOfVehicles(ID),
	[Time_Of_Travel] float not null check ([Time_Of_Travel] > 0),
	[Price_Of_Travel] decimal(10,2) null check ([Price_Of_Travel] >= 0)
)
go

-- курорт
create table Resorts(
	[Id] int primary key identity(1,1),
	[Name] nvarchar(30) not null,
	[City_ID] int foreign key references City(Id),
)
go

-- отели
create table Hotels(
	[Id] int primary key identity(1,1),
	[Name] nvarchar(30) not null,
	[Resort_ID] int foreign key references Resorts(Id),
)
go

-- способ путешенствия от одной страны, к другой
create table Resort_TypeOfHoliday(
	[Id] int primary key identity(1,1),
	[Resorts_ID] int foreign key references Resorts(ID),
	[TypeOfHoliday_ID] int foreign key references TypeOfHoliday(ID),
)
go

-- Люди
create table People(
	[Id] int primary key identity(1,1),
	[First_Name] nvarchar(30) not null,
	[Last_Name] nvarchar(30) not null,
	[Age] int not null check([Age] >= 0)
)
go

-- сотрудники
create table Employees(
	[Id] int primary key identity(1,1),
	[People_ID] int foreign key references People(Id),
	[Salary] decimal(10,2) check ([Salary] >= 0),
)
go

-- путешественник
create table Traveler(
	[Id] int primary key identity(1,1),
	[People_ID] int foreign key references People(Id)
)
go

-- Таблица путешествий
create table Travels (
    [Id] int primary key identity(1,1),
    [Traveler_ID] int foreign key references People(Id),
    [WayOfTravel_ID] int foreign key references WayOfTravel(Id),
    [Departure_Date] date not null,
    [Return_Date] date not null,
    [Total_Cost] decimal(10,2) null check ([Total_Cost] >= 0)
);
go

create table TouristAgency (
    [Id] int primary key identity(1,1),
    [Director_ID] int foreign key references Employees(ID),
	[Name] nvarchar(30) not null,
);
go

create table Employees_TouristAgency (
    [Id] int primary key identity(1,1),
    [Employee_ID] int foreign key references Employees(ID),
    [TouristAgency_ID] int foreign key references TouristAgency(ID),
);
go

create table History_Of_Travels(
    [Id] int primary key identity(1,1),
    [Manager_ID] int foreign key references Employees(ID),
    [TouristAgency_ID] int foreign key references TouristAgency(ID),
    [Travels_ID] int foreign key references TouristAgency(ID),
);
go



-------------------------------------------------
--					INITIALIZATION
-------------------------------------------------

-- Тип транспорта
INSERT INTO TypeOfVehicles ([Type]) VALUES
    ('Plane'),
    ('Train'),
    ('Car'),
    ('Bus'),
    ('Ship');
go
-- Тип отдыха
INSERT INTO TypeOfHoliday ([Type]) VALUES
    ('Beach'),
    ('Skiing'),
    ('Adventure'),
    ('Cultural'),
    ('Cruise');
go
-- Страна
INSERT INTO Country ([Name]) VALUES
    ('USA'),
    ('France'),
    ('Italy'),
    ('Japan'),
    ('Australia');
go
-- Город
INSERT INTO City ([Name], [Country_ID]) VALUES
    ('New York', 1),
    ('Paris', 2),
    ('Rome', 3),
    ('Tokyo', 4),
    ('Sydney', 5);
go
-- Способ путешествия
INSERT INTO WayOfTravel ([From_City_ID], [To_City_ID], [Type_Of_Vehicles], [Time_Of_Travel], [Price_Of_Travel]) VALUES
    (1, 2, 1, 5.0, 500.00),
    (2, 3, 2, 8.0, 300.00),
    (3, 4, 3, 3.0, 200.00),
    (4, 5, 4, 12.0, 150.00),
    (5, 1, 5, 24.0, 1000.00);
go
-- Курорт
INSERT INTO Resorts ([Name], [City_ID]) VALUES
    ('Beach Paradise', 1),
    ('Alpine Retreat', 2),
    ('Historic Rome Getaway', 3),
    ('Tokyo Adventure Base', 4),
    ('Sydney Coastal Escape', 5);
go
-- Отель
INSERT INTO Hotels ([Name], [Resort_ID]) VALUES
    ('Luxury Beach Resort', 1),
    ('Mountain Chalet', 2),
    ('Historic Rome Hotel', 3),
    ('Tokyo Business Hotel', 4),
    ('Sydney Seaside Hotel', 5);
go
-- Связь Курорт-Тип отдыха
INSERT INTO Resort_TypeOfHoliday ([Resorts_ID], [TypeOfHoliday_ID]) VALUES
    (1, 1),
    (2, 2),
    (3, 4),
    (4, 3),
    (5, 1);
go
-- Люди
INSERT INTO People ([First_Name], [Last_Name], [Age]) VALUES
    ('John', 'Doe', 25),
    ('Alice', 'Smith', 32),
    ('Bob', 'Johnson', 28),
    ('Elena', 'Garcia', 35),
    ('Michael', 'Wang', 30);
go
-- Сотрудники
INSERT INTO Employees ([People_ID], [Salary]) VALUES
    (1, 50000.00),
    (2, 60000.00),
    (3, 55000.00),
    (4, 70000.00),
    (5, 65000.00);
go
-- Путешественник
INSERT INTO Traveler ([People_ID]) VALUES
    (1),
    (2),
    (3),
    (4),
    (5);
go
-- Таблица путешествий
INSERT INTO Travels ([Traveler_ID], [WayOfTravel_ID], [Departure_Date], [Return_Date], [Total_Cost]) VALUES
    (1, 1, '2023-05-01', '2023-05-10', 1200.00),
    (2, 2, '2023-06-15', '2023-06-25', 900.00),
    (3, 3, '2023-07-20', '2023-07-25', 600.00),
    (4, 4, '2023-08-10', '2023-08-20', 450.00),
    (5, 5, '2023-09-01', '2023-09-15', 2000.00);
go
-- Туристическое агентство
INSERT INTO TouristAgency ([Director_ID], [Name]) VALUES
    (1, 'Banana Agancy');
go
-- Сотрудники туристического агентства
INSERT INTO Employees_TouristAgency ([Employee_ID], [TouristAgency_ID]) VALUES
    (2, 1),
    (3, 1),
    (4, 1);
go
-- История путешествий
INSERT INTO History_Of_Travels ([Manager_ID], [TouristAgency_ID], [Travels_ID]) VALUES
    (2, 1, 1),
    (3, 1, 1),
    (1, 1, 1);
go


-------------------------------------------------
--					REQUEST
-------------------------------------------------


-- список всех стран, где есть курорты
SELECT DISTINCT c.[Name] AS Country
FROM Country c
	INNER JOIN City ci ON c.[Id] = ci.[Country_ID]
	INNER JOIN Resorts r ON ci.[Id] = r.[City_ID];

-- получить информацию об отдыхающем, его маршруту и дате отправления
SELECT 
    p.[First_Name] + ' ' + p.[Last_Name] AS Traveler,
    w.[From_City_ID] AS Departure_City,
    w.[To_City_ID] AS Destination_City,
	w.Type_Of_Vehicles As Type_Of_Vehicles,
    t.[Departure_Date],
    t.[Return_Date]
FROM Travels t
	INNER JOIN People p ON t.[Traveler_ID] = p.[Id]
	INNER JOIN WayOfTravel w ON t.[WayOfTravel_ID] = w.[Id];

-- средний возраст всех отдыхающих в странах
SELECT 
    c.[Name] AS Country,
    AVG(p.[Age]) AS Average_Age
FROM People p
	INNER JOIN Travels t ON p.[Id] = t.[Traveler_ID]
	INNER JOIN WayOfTravel w ON t.[WayOfTravel_ID] = w.[Id]
	INNER JOIN City fc ON w.[From_City_ID] = fc.[Id]
	INNER JOIN Country c ON fc.[Country_ID] = c.[Id]
GROUP BY c.[Name];

-- суммарная стоимость всех путешествий тур агенства
SELECT 
    ta.[Name] AS Tourist_Agency,
    SUM(t.[Total_Cost]) AS Total_Cost
FROM Travels t
	INNER JOIN Employees e ON t.[Traveler_ID] = e.[People_ID]
	INNER JOIN Employees_TouristAgency eta ON e.[Id] = eta.[Employee_ID]
	INNER JOIN TouristAgency ta ON eta.[TouristAgency_ID] = ta.[Id]
GROUP BY ta.[Name];

-- список всех сотрудников
SELECT 
    p.[First_Name] + ' ' + p.[Last_Name] AS Employee,
    e.[Salary]
FROM Employees e
	INNER JOIN People p ON e.[People_ID] = p.[Id]
	INNER JOIN Employees_TouristAgency et ON e.[Id] = et.[Employee_ID]
	INNER JOIN TouristAgency ta ON et.[TouristAgency_ID] = ta.[Id]
WHERE ta.[Name] = 'Banana Agancy';