use master 
go
if db_id('Vegetables_Fruit') is not null
	drop database [Vegetables_Fruit]
go

create database [Vegetables_Fruit]
go
use [Vegetables_Fruit]
go

create table TypeOfProduct(
	[ID] int primary key identity(1,1),
	[Type] nvarchar(30) not null unique
);
go

create table Product(
	[ID] int primary key identity(1,1),
	[Name] nvarchar(30) not null,
	[Type_ID] int foreign key references TypeOfProduct(ID),
	[Color] varchar(30) not null,
	[Calories] int not null check([Calories]>=0)
);
go


-- Inserting records into the TypeOfProduct table (if necessary)
INSERT INTO TypeOfProduct ([Type])
VALUES ('Vegetable'), ('Fruit');

-- Inserting 10 records into the Product table
INSERT INTO Product ([Name], [Type_ID], [Color], [Calories])
VALUES
    ('Tomato', 1, 'Red', 18),
    ('Cucumber', 1, 'Green', 15),
    ('Apple', 2, 'Green', 52),
    ('Banana', 2, 'Yellow', 89),
    ('Carrot', 1, 'Orange', 41),
    ('Orange', 2, 'Orange', 47),
    ('Bell Pepper', 1, 'Red', 30),
    ('Pear', 2, 'Green', 57),
    ('Potato', 1, 'Yellow', 77),
    ('Lemon', 2, 'Yellow', 29);

