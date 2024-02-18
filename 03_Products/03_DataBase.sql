use master 
go
if db_id('03_Products') is not null
	drop database [03_Products]
go

create database [03_Products]
go
use [03_Products]
go

-- тип товара
create table TypeOfProduct(
	[Id] int primary key identity(1,1),
	[Type] nvarchar(30) not null unique
)
go
-- поставщик
create table Supplier(
	[Id] int primary key identity(1,1),
	[Name] nvarchar(30) not null unique
)
go

create table Product(
	[Id] int primary key identity(1,1),
	[Name] nvarchar(30) not null,
	[Type_ID] int foreign key references TypeOfProduct(Id),
	[Supplier_ID] int foreign key references Supplier(Id),
	[Count] int not null check ([Count] >= 0),
	[Cost_Price] decimal(10,2) not null check([Cost_Price] >= 0),
	[Date_Delivery] date not null,
)
go

SELECT TOP 1 
    P.Id, 
    P.Name AS ProductName, 
    TP.[Type] AS ProductType, 
    S.[Name] AS SupplierName, 
    P.[Count], 
    P.Cost_Price, 
    P.Date_Delivery
FROM 
    Product AS P
JOIN 
    TypeOfProduct AS TP ON P.Type_ID = TP.Id
JOIN 
    Supplier AS S ON P.Supplier_ID = S.Id
ORDER BY 
    P.[Count] desc;

-- Инициализация таблицы TypeOfProduct
INSERT INTO TypeOfProduct ([Type])
VALUES ('Electronics'), ('Clothing'), ('Books'), ('Home Appliances'), ('Toys');

-- Инициализация таблицы Supplier
INSERT INTO Supplier ([Name])
VALUES ('TechWorld'), ('FashionHub'), ('BookBarn'), ('HomeComfort'), ('ToyLand');

-- Инициализация таблицы Product
INSERT INTO Product ([Name], [Type_ID], [Supplier_ID], [Count], [Cost_Price], [Date_Delivery])
VALUES
('Smartphone', 1, 1, 10, 500.00, '2024-02-01'),
('Laptop', 1, 1, 20, 900.00, '2024-02-02'),
('T-Shirt', 2, 2, 15, 20.00, '2024-02-03'),
('Jeans', 2, 2, 30, 40.00, '2024-02-04'),
('Fiction Book', 3, 3, 25, 15.00, '2024-02-05'),
('Refrigerator', 4, 4, 18, 800.00, '2024-02-06'),
('LED TV', 4, 4, 22, 1200.00, '2024-02-07'),
('Sneakers', 2, 2, 17, 50.00, '2024-02-08'),
('Dress', 2, 2, 28, 70.00, '2024-02-09'),
('Non-fiction Book', 3, 3, 24, 18.00, '2024-02-10'),
('Microwave Oven', 4, 4, 20, 150.00, '2024-02-11'),
('Action Figure', 5, 5, 12, 10.00, '2024-02-12'),
('Headphones', 1, 1, 30, 40.00, '2024-02-13'),
('Skirt', 2, 2, 25, 30.00, '2024-02-14'),
('Cookware Set', 4, 4, 18, 200.00, '2024-02-15'),
('Tablet', 1, 1, 15, 300.00, '2024-02-16'),
('Adventure Book', 3, 3, 20, 14.00, '2024-02-17'),
('Air Purifier', 4, 4, 10, 120.00, '2024-02-18'),
('Toy Car', 5, 5, 35, 25.00, '2024-02-19'),
('Doll', 5, 5, 27, 20.00, '2024-02-20');



select Type from TypeOfProduct