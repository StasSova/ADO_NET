use master 
go
if db_id('04_StationeryCompany') is not null
	drop database [04_StationeryCompany]
go

create database [04_StationeryCompany]
go
use [04_StationeryCompany]
go


create table TypeOfItem(
	[Id] int primary key identity(1,1),
	[Type] nvarchar(100) not null unique,
	[Count] int check([Count]>=0) default(0),
)
go
CREATE TABLE TypeOfItemArchive (
    Id INT PRIMARY KEY,
    Type NVARCHAR(100) NOT NULL,
    DeletedDate DATETIME
);
go
create table Manager(
	[Id] int primary key identity(1,1),
	[FName] nvarchar(100) not null,
	[LName] nvarchar(100) not null
)
go
CREATE TABLE ManagerArchive (
    Id INT PRIMARY KEY,
    FName NVARCHAR(25) NOT NULL,
    LName NVARCHAR(25) NOT NULL,
    DeletedDate DATETIME
);
go
create table Company(
	[Id] int primary key identity(1,1),
	[Name] nvarchar(100) not null
)
go
CREATE TABLE CompanyArchive (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    DeletedDate DATETIME
);
go

create table Item(
	[Id] int primary key identity(1,1),
	[Name] nvarchar(100) not null check ([Name]<>''),
	[Type_ID] int foreign key references TypeOfItem(Id) ON DELETE CASCADE,
	[Count] int check ([Count] >= 0),
	[CostPrice] decimal (10,2) check ([CostPrice] >= 0),
	[Price] decimal (10,2) check ([Price] >= 0),
)
go

CREATE TABLE ItemArchive (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Type_ID INT NOT NULL ,
    Count INT NOT NULL,
    CostPrice DECIMAL(10, 2) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    DeletedDate DATETIME
);
go
create table HistoryOfSells(
	[Id] int primary key identity(1,1),
	[ItemId] int foreign key references Item(Id) ON DELETE SET NULL,
	[ManagerID] int foreign key references Manager(Id) ON DELETE SET NULL,
	[BuyersCompany] int foreign key references Company(Id) ON DELETE SET NULL,
	[Count] int not null check ([Count] > 0),
	[Date] date not null check ([Date] <= GetDate()) default (GetDate())
)
go
---------------------------------------------------------------------
--					ОТОБРАЖЕНИЕ
---------------------------------------------------------------------
CREATE PROCEDURE ShowAllHystoryOfSells
AS
BEGIN
    SELECT *
    FROM HistoryOfSells h
END;
go
-- Показать канцтовары, которые продал конкретный менеджер по продажам:
--drop procedure ShowItemsSoldByManagerId
CREATE PROCEDURE ShowItemsSoldByManagerId
    @ManagerId INT
AS
BEGIN
    SELECT h.*
    FROM Item i
    INNER JOIN HistoryOfSells h ON i.Id = h.ItemId
    WHERE h.ManagerID = @ManagerId;
END;
go
-- Показать канцтовары, которые купила конкретная фирма покупатель;
--drop procedure ShowItemsBoughtByCompanyId
CREATE PROCEDURE ShowItemsBoughtByCompanyId
    @CompanyId INT
AS
BEGIN
    SELECT h.*
    FROM Item i
    INNER JOIN HistoryOfSells h ON i.Id = h.ItemId
    WHERE h.BuyersCompany = @CompanyId;
END;
go
-- Показать название канцтоваров, которые не продавались заданное количество дней.
--drop procedure GetItemsNotSoldForDays
CREATE PROCEDURE GetItemsNotSoldForDays
    @NumberOfDays INT
AS
BEGIN
    SELECT DISTINCT
        H.*
    FROM
        Item I
    LEFT JOIN
        HistoryOfSells H ON I.Id = H.ItemID
    WHERE
        H.Date < DATEADD(DAY, -@NumberOfDays, GETDATE()) OR H.Date IS NULL;
END;
go
---------------------------------------------------------------------
--					ТИПЫ
---------------------------------------------------------------------
-- Отображение всех типов канцтоваров:
CREATE PROCEDURE ShowAllItemTypesWithID
AS
BEGIN
    SELECT * FROM TypeOfItem;
END;
go



---------------------------------------------------------------------
--					ТОВАРЫ
---------------------------------------------------------------------
-- Показать канцтовары
CREATE PROCEDURE ShowAllItemsInformationWithID
AS
BEGIN
    SELECT * FROM Item;
END;
go
-- Показать канцтовары с максимальным количеством единиц:
CREATE PROCEDURE ShowItemsWithMaxQuantityWithID
AS
BEGIN
    SELECT * 
    FROM Item 
    WHERE Count = (SELECT MAX(Count) FROM Item);
END;
go
-- Показать канцтовары с минимальным количеством единиц:
CREATE PROCEDURE ShowItemsWithMinQuantityWithID
AS
BEGIN
    SELECT * 
    FROM Item 
    WHERE Count = (SELECT MIN(Count) FROM Item);
END;
go
-- Показать канцтовары с минимальной себестоимостью единицы 
CREATE PROCEDURE ShowItemsWithMinCostPriceWithID
AS
BEGIN
    SELECT * 
    FROM Item 
    WHERE CostPrice = (SELECT MIN(CostPrice) FROM Item);
END;
go
-- Показать канцтовары с максимальной себестоимостью единицы:
CREATE PROCEDURE ShowItemsWithMaxCostPriceWithID
AS
BEGIN
    SELECT * 
    FROM Item 
    WHERE CostPrice = (SELECT MAX(CostPrice) FROM Item);
END;
go


-- Показать название самых популярных канцтоваров. Популярность высчитываем по количеству проданных единиц;
CREATE PROCEDURE GetMostPopularItems
    @TopItemsCount INT
AS
BEGIN
    SELECT TOP (@TopItemsCount)
        I.*,
        TotalSales.TotalSales AS TotalSales
    FROM
        Item I
    INNER JOIN
        (
            SELECT
                H.ItemID,
                SUM(H.Count) AS TotalSales
            FROM
                HistoryOfSells H
            GROUP BY
                H.ItemID
        ) AS TotalSales ON I.Id = TotalSales.ItemID
    ORDER BY
        TotalSales.TotalSales DESC;
END;
go


---------------------------------------------------------------------
--					КОМПАНИЯ
---------------------------------------------------------------------
-- Показать информацию о фирме покупателе
CREATE PROCEDURE GetCompanyInfo
AS
BEGIN
    SELECT * FROM Company;
END;
GO
-- Показать информацию о фирме покупателе, которая купила на самую большую сумму
CREATE PROCEDURE GetTopCompaniesByPurchaseAmount
    @TopCompaniesCount INT
AS
BEGIN
    SELECT TOP (@TopCompaniesCount)
        C.*,
        SUM(I.Price * H.Count) AS TotalPurchaseAmount
    FROM
        Company C
    INNER JOIN
        HistoryOfSells H ON C.Id = H.BuyersCompany
    INNER JOIN
        Item I ON H.ItemId = I.Id 
    GROUP BY
        C.Id, C.Name
    ORDER BY
        TotalPurchaseAmount DESC;
END;
go

---------------------------------------------------------------------
--					МЕНЕДЖЕРЫ
---------------------------------------------------------------------
-- Отображение всех менеджеров по продажам
CREATE PROCEDURE ShowAllManagersWithID
AS
BEGIN
    SELECT * FROM Manager;
END;
go
--Показать информацию о менеджере с наибольшим количеством продаж по количеству единиц;
CREATE PROCEDURE GetTopManagersBySales
    @TopManagersCount INT
AS
BEGIN
    SELECT TOP (@TopManagersCount)
        M.*,
        SUM(H.Count) AS TotalSales
    FROM
        Manager M
    INNER JOIN
        HistoryOfSells H ON M.Id = H.ManagerID
    GROUP BY
        M.Id, M.FName, M.LName
    ORDER BY
        TotalSales DESC;
END;
go
-- Показать информацию о менеджере по продажам с наибольшей общей суммой прибыли;
CREATE PROCEDURE GetTopManagersByProfit
    @TopManagersCount INT
AS
BEGIN
    SELECT TOP (@TopManagersCount)
        M.*,
        SUM(I.Price * H.Count) AS TotalProfit
    FROM
        Manager M
    INNER JOIN
        HistoryOfSells H ON M.Id = H.ManagerID
    INNER JOIN
        Item I ON H.ItemId = I.Id 
    GROUP BY
        M.Id, M.FName, M.LName
    ORDER BY
        TotalProfit DESC;
END;
go
-- Показать информацию о менеджере по продажам с наибольшей общей суммой прибыли за указанный промежуток времени;
CREATE PROCEDURE GetManagerWithHighestProfitInRange
    @StartDate DATE,
    @EndDate DATE,
    @TopManagersCount INT
AS
BEGIN
    SELECT TOP (@TopManagersCount)
        M.*,
        SUM(I.Price * H.Count) AS TotalProfit
    FROM
        Manager M
    INNER JOIN
        HistoryOfSells H ON M.Id = H.ManagerID
    INNER JOIN
        Item I ON H.ItemId = I.Id 
    WHERE
        H.Date BETWEEN @StartDate AND @EndDate
    GROUP BY
        M.Id, M.FName, M.LName
    ORDER BY
        TotalProfit DESC;
END;
GO
---------------------------------------------------------------------
--					ПРОДАЖИ
---------------------------------------------------------------------

-- Показать информацию о самой недавней продаже;
CREATE PROCEDURE GetRecentSaleInformation
    @TopCount INT
AS
BEGIN
    SELECT TOP (@TopCount) *
    FROM HistoryOfSells
    ORDER BY Date DESC;
END;
GO
---------------------------------------------------------------------
--					ПРОЧЕЕ
---------------------------------------------------------------------
-- Показать среднее количество товаров по каждому типу канцтоваров.
CREATE PROCEDURE ShowAverageItemCountPerType
AS
BEGIN
    SELECT 
        t.[Type], 
        AVG(i.[Count]) AS AverageItemCount
    FROM TypeOfItem t
    INNER JOIN Item i ON t.[Id] = i.[Type_ID]
    GROUP BY t.[Type];
END;
go

-- Показать информацию о типе канцтоваров с наибольшим количеством продаж по единицам
--drop procedure GetItemTypeWithMostSales
CREATE PROCEDURE GetItemTypeWithMostSales
    @NumberOfItemsToShow INT
AS
BEGIN
    -- Временная таблица для хранения результатов
    CREATE TABLE #TempMostSoldItems (
        TypeId INT,
        TypeName NVARCHAR(100),
        TotalSales INT
    );

    -- Заполняем временную таблицу данными о типах товаров с наибольшим количеством продаж
    INSERT INTO #TempMostSoldItems (TypeId, TypeName, TotalSales)
    SELECT TOP (@NumberOfItemsToShow)
        T.Id AS TypeId,
        T.[Type] AS TypeName,
        SUM(H.Count) AS TotalSales
    FROM
        TypeOfItem T
    INNER JOIN
        Item I ON T.Id = I.Type_ID
    INNER JOIN
        HistoryOfSells H ON I.Id = H.ItemId
    GROUP BY
        T.Id, T.[Type]
    ORDER BY
        SUM(H.Count) DESC;

    -- Возвращаем всю информацию о типе товара с наибольшим количеством продаж
    SELECT
        T.*,
        M.TotalSales
    FROM
        TypeOfItem T
    INNER JOIN
        #TempMostSoldItems M ON T.Id = M.TypeId;

    -- Удаляем временную таблицу
    DROP TABLE #TempMostSoldItems;
END;
-- Показать информацию о типе самых прибыльных канцтоваров
--drop PROCEDURE GetMostProfitableItemType
CREATE PROCEDURE GetMostProfitableItemType
    @NumberOfItemsToShow INT
AS
BEGIN
    SELECT TOP (@NumberOfItemsToShow)
        T.Id,
        T.[Type],
        T.[Count],
        SUM((I.Price - I.CostPrice) * H.Count) AS TotalProfit
    FROM
        TypeOfItem T
    INNER JOIN
        Item I ON T.Id = I.Type_ID
    INNER JOIN
        HistoryOfSells H ON I.Id = H.ItemID
    GROUP BY
        T.Id, T.[Type], T.[Count]
    ORDER BY
        TotalProfit DESC;
END;
GO


---------------------------------------------------------------------
--					ТРИГГЕР
---------------------------------------------------------------------
-- тригер, который срабатывает при вставке в табилцу
CREATE TRIGGER UpdateTypeItemCount
ON Item
AFTER INSERT
AS
BEGIN
    -- Обновляем количество товаров в соответствующем типе в таблице TypeOfItem
    UPDATE t
    SET t.[Count] = t.[Count] + i.[Count]
    FROM TypeOfItem t
    INNER JOIN inserted i ON t.[Id] = i.[Type_ID];
END;
go

-- Триггер для обновления при удалении
CREATE TRIGGER UpdateTypeItemCount_Delete
ON Item
AFTER DELETE
AS
BEGIN
    DECLARE @DeletedTypeId INT;
    DECLARE @DeletedCount INT;

    -- Получаем Id удаленного типа товара и количество удаленных предметов
    SELECT @DeletedTypeId = d.[Type_ID], @DeletedCount = d.[Count]
    FROM deleted d;

    -- Обновляем количество товаров в соответствующем типе в таблице TypeOfItem
    UPDATE TypeOfItem
    SET [Count] = [Count] - @DeletedCount
    WHERE [Id] = @DeletedTypeId;
END;
GO

-- Триггер для обновления при обновлении
CREATE TRIGGER UpdateTypeItemCount_Update
ON Item
AFTER UPDATE
AS
BEGIN
    DECLARE @UpdatedTypeId INT;
    DECLARE @OldCount INT;
    DECLARE @NewCount INT;

    -- Получаем Id обновленного типа товара и старое и новое количество предметов
    SELECT @UpdatedTypeId = i.[Type_ID], @OldCount = d.[Count], @NewCount = i.[Count]
    FROM inserted i
    JOIN deleted d ON i.[Id] = d.[Id];

    -- Обновляем количество товаров в соответствующем типе в таблице TypeOfItem
    UPDATE TypeOfItem
    SET [Count] = [Count] + (@NewCount - @OldCount)
    WHERE [Id] = @UpdatedTypeId;
END;
go

-- Триггер Удаление фирм покупателей
CREATE TRIGGER Company_DeleteTrigger
ON Company
AFTER DELETE
AS
BEGIN
    INSERT INTO CompanyArchive (Id, Name, DeletedDate)
    SELECT Id, Name, GETDATE()
    FROM deleted;
END;
go

-- Триггер Удаление типов канцтоваров
CREATE TRIGGER TypeOfItem_DeleteTrigger
ON TypeOfItem
AFTER DELETE
AS
BEGIN
    INSERT INTO TypeOfItemArchive (Id, Type, DeletedDate)
    SELECT Id, Type, GETDATE()
    FROM deleted;
END;
go

-- Триггер Удаление менеджеров по продажам
CREATE TRIGGER Manager_DeleteTrigger
ON Manager
AFTER DELETE
AS
BEGIN
    INSERT INTO ManagerArchive (Id, FName, LName, DeletedDate)
    SELECT Id, FName, LName, GETDATE()
    FROM deleted;
END;
go

-- Триггер удаление канцтоваров:
CREATE TRIGGER Item_DeleteTrigger
ON Item
AFTER DELETE
AS
BEGIN
    INSERT INTO ItemArchive (Id, Name, Type_ID, Count, CostPrice, Price, DeletedDate)
    SELECT Id, Name, Type_ID, Count, CostPrice, Price, GETDATE()
    FROM deleted;
END;
go

---------------------------------------------------------------------
--					ВСТАВКА
---------------------------------------------------------------------

-- Вставка новых канцтоваров;
--drop procedure InsertNewItem
CREATE PROCEDURE InsertNewItem
    @ItemName NVARCHAR(30),
    @TypeId INT,
    @Count INT,
    @CostPrice DECIMAL(10, 2),
    @Price DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Item ([Name], [Type_ID], [Count], [CostPrice], [Price])
		VALUES (@ItemName, @TypeId, @Count, @CostPrice, @Price);
END;
go

-- Вставка новых типов канцтоваров;
CREATE PROCEDURE InsertNewItemType
    @TypeName NVARCHAR(30)
AS
BEGIN
    INSERT INTO TypeOfItem ([Type])
    VALUES (@TypeName);
END;
go
-- Вставка новых менеджеров по продажам
CREATE PROCEDURE InsertNewManager
    @FirstName NVARCHAR(25),
    @LastName NVARCHAR(25)
AS
BEGIN
    INSERT INTO Manager ([FName], [LName])
    VALUES (@FirstName, @LastName);
END;
go
-- Вставка новых фирм покупателей:
CREATE PROCEDURE InsertNewBuyerCompany
    @CompanyName NVARCHAR(30)
AS
BEGIN
    INSERT INTO Company ([Name])
    VALUES (@CompanyName);
END;
go

---------------------------------------------------------------------
--					ОБНОВЛЕНИЕ
---------------------------------------------------------------------

-- Обновление информации о существующих канцтоварах
CREATE PROCEDURE UpdateItemInformation
    @ItemId INT,
    @ItemName NVARCHAR(30),
    @TypeId INT,
    @Count INT,
    @CostPrice DECIMAL(10, 2),
    @Price DECIMAL(10, 2)
AS
BEGIN
    -- Обновить информацию о канцтоваре
    UPDATE Item
    SET [Name] = @ItemName,
        [Type_ID] = @TypeId,
        [Count] = @Count,
        [CostPrice] = @CostPrice,
        [Price] = @Price
    WHERE Id = @ItemId;
END;
go
-- Обновление информации о существующих фирмах покупателях;
CREATE PROCEDURE UpdateCompanyInformation
    @CompanyName NVARCHAR(30),
    @NewCompanyName NVARCHAR(30)
AS
BEGIN
    DECLARE @CompanyId INT;

    SELECT @CompanyId = Id FROM Company WHERE [Name] = @CompanyName;

    -- Проверить наличие фирмы покупателя
    IF @CompanyId IS NULL
    BEGIN
        -- Если фирма покупателя не найдена, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Фирма покупателя ' + @CompanyName + ' не найдена.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Обновить информацию о фирме покупателе
    UPDATE Company
    SET [Name] = @NewCompanyName
    WHERE Id = @CompanyId;
END;
go
-- Обновление информации о существующих менеджерах по продажам;
CREATE PROCEDURE UpdateManagerInformation
    @ManagerFirstName NVARCHAR(25),
    @ManagerLastName NVARCHAR(25),
    @NewFirstName NVARCHAR(25),
    @NewLastName NVARCHAR(25)
AS
BEGIN
    DECLARE @ManagerId INT;

    -- Найти Id менеджера по его имени и фамилии
    SELECT @ManagerId = Id FROM Manager WHERE [FName] = @ManagerFirstName AND [LName] = @ManagerLastName;

    -- Проверить наличие менеджера
    IF @ManagerId IS NULL
    BEGIN
        -- Если менеджер не найден, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Менеджер ' + @ManagerFirstName + ' ' + @ManagerLastName + ' не найден.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Обновить информацию о менеджере
    UPDATE Manager
    SET [FName] = @NewFirstName,
        [LName] = @NewLastName
    WHERE Id = @ManagerId;
END;
go
-- Обновление информации о существующих типах канцтоваров.
CREATE PROCEDURE UpdateItemTypeInformation
    @TypeName NVARCHAR(30),
    @NewTypeName NVARCHAR(30)
AS
BEGIN
    DECLARE @TypeId INT;

    -- Найти Id типа канцтовара по его названию
    SELECT @TypeId = Id FROM TypeOfItem WHERE [Type] = @TypeName;

    -- Проверить наличие типа канцтовара
    IF @TypeId IS NULL
    BEGIN
        -- Если тип канцтовара не найден, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Тип канцтовара ' + @TypeName + ' не найден.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Обновить информацию о типе канцтовара
    UPDATE TypeOfItem
    SET [Type] = @NewTypeName
    WHERE Id = @TypeId;
END;
go

---------------------------------------------------------------------
--					УДАЛЕНИЕ
---------------------------------------------------------------------

-- Удаление канцтоваров:
CREATE PROCEDURE DeleteItem
    @ItemName NVARCHAR(30)
AS
BEGIN
    DECLARE @ItemId INT;

    -- Найти Id канцтовара по его названию
    SELECT @ItemId = Id FROM Item WHERE [Name] = @ItemName;

    -- Проверить наличие канцтовара
    IF @ItemId IS NULL
    BEGIN
        -- Если канцтовар не найден, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Канцтовар ' + @ItemName + ' не найден.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Удалить канцтовар
    DELETE FROM Item
    WHERE Id = @ItemId;
END;
go

CREATE PROCEDURE DeleteItemById
    @ItemId INT
AS
BEGIN
    -- Проверить наличие канцтовара по заданному ID
    IF NOT EXISTS (SELECT 1 FROM Item WHERE Id = @ItemId)
    BEGIN
        -- Если канцтовар не найден, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Канцтовар с ID ' + CONVERT(NVARCHAR(10), @ItemId) + ' не найден.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Удалить канцтовар
    DELETE FROM Item
    WHERE Id = @ItemId;
END;
GO

-- Удаление менеджеров по продажам:
CREATE PROCEDURE DeleteManager
    @ManagerFirstName NVARCHAR(25),
    @ManagerLastName NVARCHAR(25)
AS
BEGIN
    DECLARE @ManagerId INT;

    -- Найти Id менеджера по его имени и фамилии
    SELECT @ManagerId = Id FROM Manager WHERE [FName] = @ManagerFirstName AND [LName] = @ManagerLastName;

    -- Проверить наличие менеджера
    IF @ManagerId IS NULL
    BEGIN
        -- Если менеджер не найден, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Менеджер ' + @ManagerFirstName + ' ' + @ManagerLastName + ' не найден.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Удалить менеджера
    DELETE FROM Manager
    WHERE Id = @ManagerId;
END;
go

CREATE PROCEDURE DeleteManagerById
    @ManagerId INT
AS
BEGIN
    -- Проверить наличие менеджера по заданному ID
    IF NOT EXISTS (SELECT 1 FROM Manager WHERE Id = @ManagerId)
    BEGIN
        -- Если менеджер не найден, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Менеджер с ID ' + CONVERT(NVARCHAR(10), @ManagerId) + ' не найден.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Удалить менеджера
    DELETE FROM Manager
    WHERE Id = @ManagerId;
END;
GO

-- Удаление типов канцтоваров:
CREATE PROCEDURE DeleteItemType
    @TypeName NVARCHAR(30)
AS
BEGIN
    DECLARE @TypeId INT;

    -- Найти Id типа канцтовара по его названию
    SELECT @TypeId = Id FROM TypeOfItem WHERE [Type] = @TypeName;

    -- Проверить наличие типа канцтовара
    IF @TypeId IS NULL
    BEGIN
        -- Если тип канцтовара не найден, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Тип канцтовара ' + @TypeName + ' не найден.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Удалить тип канцтовара
    DELETE FROM TypeOfItem
    WHERE Id = @TypeId;
END;
go

CREATE PROCEDURE DeleteItemTypeById
    @TypeId INT
AS
BEGIN
    -- Проверить наличие типа канцтовара по заданному идентификатору
    IF NOT EXISTS (SELECT 1 FROM TypeOfItem WHERE Id = @TypeId)
    BEGIN
        -- Если тип канцтовара не найден, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Тип канцтовара с ID ' + CONVERT(NVARCHAR(10), @TypeId) + ' не найден.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Удалить тип канцтовара
    DELETE FROM TypeOfItem
    WHERE Id = @TypeId;
END;
GO

-- Удаление фирм покупателей:
CREATE PROCEDURE DeleteCompany
    @CompanyName NVARCHAR(30)
AS
BEGIN
    DECLARE @CompanyId INT;

    -- Найти Id фирмы покупателя по её названию
    SELECT @CompanyId = Id FROM Company WHERE [Name] = @CompanyName;

    -- Проверить наличие фирмы покупателя
    IF @CompanyId IS NULL
    BEGIN
        -- Если фирма покупателя не найдена, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Фирма покупателя ' + @CompanyName + ' не найдена.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Удалить фирму покупателя
    DELETE FROM Company
    WHERE Id = @CompanyId;
END;
go
CREATE PROCEDURE DeleteCompanyById
    @CompanyId INT
AS
BEGIN
    -- Проверить наличие фирмы покупателя по заданному идентификатору
    IF NOT EXISTS (SELECT 1 FROM Company WHERE Id = @CompanyId)
    BEGIN
        -- Если фирма покупателя не найдена, выбросить пользовательское исключение
        DECLARE @ErrorMessage NVARCHAR(100) = 'Фирма покупателя с ID ' + CONVERT(NVARCHAR(10), @CompanyId) + ' не найдена.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- Удалить фирму покупателя
    DELETE FROM Company
    WHERE Id = @CompanyId;
END;
GO




---------------------------------------------------------------------
--					ИНИЦИАЛИЗАЦИЯ
---------------------------------------------------------------------

-- Вставка типов канцтоваров
INSERT INTO TypeOfItem ([Type]) 
VALUES 
    ('Карандаши'),
    ('Ручки'),
    ('Тетради'),
    ('Степлеры'),
    ('Клей'),
    ('Блокноты'),
    ('Маркеры'),
    ('Ластик'),
    ('Ножницы'),
    ('Корректоры'),
    ('Канцелярские кнопки'),
    ('Клейкие ленты'),
    ('Фломастеры'),
    ('Шариковые ручки'),
    ('Цветные карандаши'),
    ('Кисти для рисования'),
    ('Бумажные скрепки'),
    ('Пеналы'),
    ('Флажки'),
    ('Перфораторы');
go
-- Вставка менеджеров
INSERT INTO Manager ([FName], [LName]) 
VALUES 
    ('Иван', 'Иванов'),
    ('Петр', 'Петров'),
    ('Мария', 'Сидорова'),
    ('Елена', 'Козлова'),
    ('Александр', 'Смирнов'),
    ('Ольга', 'Морозова'),
    ('Сергей', 'Попов'),
    ('Татьяна', 'Новикова'),
    ('Дмитрий', 'Зайцев'),
    ('Анна', 'Павлова');
go
-- Вставка фирм покупателей
INSERT INTO Company ([Name]) 
VALUES 
    ('ООО "Рога и копыта"'),
    ('ОАО "Пчелка"'),
    ('ИП "Кузнецов"'),
    ('ООО "Звезда"'),
    ('АО "Дельфин"'),
    ('ИП "Луч"'),
    ('ООО "Торговый дом"'),
    ('ОАО "Маяк"'),
    ('ИП "Жар-Птица"'),
    ('ООО "Экспресс-Транс"');
go
-- Вставка канцтоваров
INSERT INTO Item ([Name], [Type_ID], [Count], [CostPrice], [Price]) 
VALUES 
    ('Карандаши B2+', 4, 100, 50, 100),
    ('Карандаши HB', 1, 100, 50, 100),
    ('Ручка гелевая', 2, 200, 80, 150),
    ('Тетрадь в клетку', 3, 50, 120, 200),
    ('Степлер "Крепыш"', 4, 30, 500, 800),
    ('Клей "Момент"', 5, 50, 80, 120),
    ('Блокнот А5', 6, 100, 200, 300),
    ('Маркеры для досок', 7, 150, 100, 200),
    ('Ластик "Радуга"', 8, 80, 30, 50),
    ('Ножницы канцелярские', 9, 70, 150, 200),
    ('Корректор жидкий', 10, 120, 60, 100),
    ('Канцелярские кнопки', 11, 200, 20, 50),
    ('Клейкая лента', 12, 100, 40, 80),
    ('Фломастеры для детей', 13, 150, 50, 100),
    ('Шариковая ручка', 14, 180, 70, 120),
    ('Цветные карандаши', 15, 120, 70, 150),
    ('Кисти для рисования', 16, 90, 100, 200),
    ('Бумажные скрепки', 17, 200, 10, 20),
    ('Пенал для канцелярии', 18, 80, 150, 250),
    ('Флажки для маркировки', 19, 120, 30, 50),
    ('Перфоратор для бумаги', 20, 50, 300, 400),
    ('Карандаши 2B', 1, 150, 60, 110),
    ('Ручка шариковая', 2, 220, 90, 160),
    ('Тетрадь в линейку', 3, 70, 130, 220),
    ('Степлер "Стандарт"', 4, 40, 600, 900),
    ('Клей моментальный', 5, 60, 100, 150),
    ('Блокнот А4', 6, 120, 250, 350),
    ('Маркеры для текста', 7, 170, 120, 220),
    ('Ластик "Совёнок"', 8, 100, 40, 60),
    ('Ножницы профессиональные', 9, 90, 200, 250),
    ('Корректор карандашный', 10, 150, 70, 110),
    ('Кнопки для бумаги', 11, 250, 30, 60),
    ('Лента двусторонняя', 12, 150, 50, 100),
    ('Фломастеры профессиональные', 13, 180, 60, 120),
    ('Ручка гелевая металлик', 14, 200, 80, 130),
    ('Карандаши цветные Profi', 15, 150, 80, 170),
    ('Кисти синтетические', 16, 110, 120, 250),
    ('Скрепки для бумаги', 17, 250, 15, 25),
    ('Пенал для ручек', 18, 100, 180, 280),
    ('Флажки разноцветные', 19, 130, 40, 70),
    ('Перфоратор для кожи', 20, 60, 400, 500),
    ('Карандаши 2H', 1, 170, 70, 120),
    ('Ручка гелевая тонкая', 2, 240, 100, 170),
    ('Тетрадь в клетку большая', 3, 80, 150, 250),
    ('Степлер "Мастер"', 4, 50, 700, 1000),
    ('Клей быстро схватывающий', 5, 70, 120, 180),
    ('Блокнот А6', 6, 150, 180, 280),
    ('Маркеры для выделения', 7, 190, 150, 250),
    ('Ластик "Пингвин"', 8, 120, 50, 70),
    ('Ножницы для ткани', 9, 80, 250, 300),
    ('Корректор кистевой', 10, 160, 80, 130),
    ('Кнопки цветные', 11, 270, 40, 70),
    ('Лента односторонняя', 12, 170, 60, 110),
    ('Фломастеры для взрослых', 13, 190, 70, 130),
    ('Ручка гелевая синяя', 14, 220, 90, 140),
    ('Карандаши цветные детские', 15, 180, 90, 180),
    ('Кисти натуральные', 16, 130, 130, 280),
    ('Скрепки кольцевые', 17, 270, 20, 30),
    ('Пенал для кистей', 18, 120, 200, 300),
    ('Флажки яркие', 19, 150, 50, 80),
    ('Перфоратор для пластика', 20, 70, 500, 600),
    ('Карандаши 4B', 1, 190, 80, 130),
    ('Ручка гелевая черная', 2, 250, 100, 160),
    ('Тетрадь в клетку маленькая', 3, 90, 140, 230),
    ('Степлер "Профессионал"', 4, 60, 800, 1200),
    ('Клей универсальный', 5, 80, 150, 200),
    ('Блокнот в клетку', 6, 170, 220, 320),
    ('Маркеры для планшетов', 7, 200, 170, 270),
    ('Ластик "Коала"', 8, 130, 60, 80),
    ('Ножницы для бумаги', 9, 100, 300, 350),
    ('Корректор в стике', 10, 180, 90, 140),
    ('Кнопки большие', 11, 300, 50, 80),
    ('Лента маскировочная', 12, 200, 70, 120),
    ('Фломастеры для школы', 13, 200, 80, 140),
    ('Ручка гелевая зеленая', 14, 230, 90, 150),
    ('Карандаши цветные профессиональные', 15, 200, 100, 200),
    ('Кисти для акварели', 16, 150, 150, 300),
    ('Скрепки металлические', 17, 300, 25, 35),
    ('Пенал для ручек и карандашей', 18, 130, 220, 320),
    ('Флажки прозрачные', 19, 170, 60, 90),
    ('Перфоратор для картона', 20, 80, 600, 700);
go
-- Вставка истории продаж
INSERT INTO HistoryOfSells ([ItemId], [ManagerID], [BuyersCompany], [Count], [Date])
VALUES 
    (1, 1, 1, 20, '2023-01-05'),
    (2, 2, 2, 30, '2023-01-10'),
    (3, 3, 3, 15, '2023-01-15'),
    (4, 1, 4, 25, '2023-01-20'),
    (5, 2, 5, 40, '2023-01-25'),
    (6, 3, 6, 10, '2023-02-01'),
    (7, 1, 7, 35, '2023-02-05'),
    (8, 2, 8, 20, '2023-02-10'),
    (9, 3, 9, 45, '2023-02-15'),
    (10, 1, 10, 15, '2023-02-20'),
    (11, 2, 1, 30, '2023-02-25'),
    (12, 3, 2, 25, '2023-03-01'),
    (13, 1, 3, 20, '2023-03-05'),
    (14, 2, 4, 40, '2023-03-10'),
    (15, 3, 5, 35, '2023-03-15'),
    (16, 1, 6, 15, '2023-03-20'),
    (17, 2, 7, 25, '2023-03-25'),
    (18, 3, 8, 30, '2023-04-01'),
    (19, 1, 9, 20, '2023-04-05'),
    (20, 2, 10, 45, '2023-04-10'),
    (21, 3, 1, 10, '2023-04-15'),
    (22, 1, 2, 35, '2023-04-20'),
    (23, 2, 3, 20, '2023-04-25'),
    (24, 3, 4, 30, '2023-05-01'),
    (25, 1, 5, 25, '2023-05-05'),
    (26, 2, 6, 40, '2023-05-10'),
    (27, 3, 7, 15, '2023-05-15'),
    (28, 1, 8, 30, '2023-05-20'),
    (29, 2, 9, 20, '2023-05-25'),
    (30, 3, 10, 45, '2023-06-01'),
    (31, 1, 1, 15, '2023-06-05'),
    (32, 2, 2, 30, '2023-06-10'),
    (33, 3, 3, 20, '2023-06-15'),
    (34, 1, 4, 40, '2023-06-20'),
    (35, 2, 5, 35, '2023-06-25'),
    (36, 3, 6, 25, '2023-07-01'),
    (37, 1, 7, 15, '2023-07-05'),
    (38, 2, 8, 30, '2023-07-10'),
    (39, 3, 9, 20, '2023-07-15'),
    (40, 1, 10, 45, '2023-07-20'),
    (41, 2, 1, 10, '2023-07-25'),
    (42, 3, 2, 35, '2023-08-01'),
    (43, 1, 3, 20, '2023-08-05'),
    (44, 2, 4, 25, '2023-08-10'),
    (45, 3, 5, 30, '2023-08-15'),
    (46, 1, 6, 20, '2023-08-20'),
    (47, 2, 7, 35, '2023-08-25'),
    (48, 3, 8, 25, '2023-09-01'),
    (49, 1, 9, 15, '2023-09-05'),
    (50, 2, 10, 30, '2023-09-10');
go
