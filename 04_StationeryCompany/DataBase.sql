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
--					�����������
---------------------------------------------------------------------
CREATE PROCEDURE ShowAllHystoryOfSells
AS
BEGIN
    SELECT *
    FROM HistoryOfSells h
END;
go
-- �������� ����������, ������� ������ ���������� �������� �� ��������:
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
-- �������� ����������, ������� ������ ���������� ����� ����������;
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
-- �������� �������� �����������, ������� �� ����������� �������� ���������� ����.
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
--					����
---------------------------------------------------------------------
-- ����������� ���� ����� �����������:
CREATE PROCEDURE ShowAllItemTypesWithID
AS
BEGIN
    SELECT * FROM TypeOfItem;
END;
go



---------------------------------------------------------------------
--					������
---------------------------------------------------------------------
-- �������� ����������
CREATE PROCEDURE ShowAllItemsInformationWithID
AS
BEGIN
    SELECT * FROM Item;
END;
go
-- �������� ���������� � ������������ ����������� ������:
CREATE PROCEDURE ShowItemsWithMaxQuantityWithID
AS
BEGIN
    SELECT * 
    FROM Item 
    WHERE Count = (SELECT MAX(Count) FROM Item);
END;
go
-- �������� ���������� � ����������� ����������� ������:
CREATE PROCEDURE ShowItemsWithMinQuantityWithID
AS
BEGIN
    SELECT * 
    FROM Item 
    WHERE Count = (SELECT MIN(Count) FROM Item);
END;
go
-- �������� ���������� � ����������� �������������� ������� 
CREATE PROCEDURE ShowItemsWithMinCostPriceWithID
AS
BEGIN
    SELECT * 
    FROM Item 
    WHERE CostPrice = (SELECT MIN(CostPrice) FROM Item);
END;
go
-- �������� ���������� � ������������ �������������� �������:
CREATE PROCEDURE ShowItemsWithMaxCostPriceWithID
AS
BEGIN
    SELECT * 
    FROM Item 
    WHERE CostPrice = (SELECT MAX(CostPrice) FROM Item);
END;
go


-- �������� �������� ����� ���������� �����������. ������������ ����������� �� ���������� ��������� ������;
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
--					��������
---------------------------------------------------------------------
-- �������� ���������� � ����� ����������
CREATE PROCEDURE GetCompanyInfo
AS
BEGIN
    SELECT * FROM Company;
END;
GO
-- �������� ���������� � ����� ����������, ������� ������ �� ����� ������� �����
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
--					���������
---------------------------------------------------------------------
-- ����������� ���� ���������� �� ��������
CREATE PROCEDURE ShowAllManagersWithID
AS
BEGIN
    SELECT * FROM Manager;
END;
go
--�������� ���������� � ��������� � ���������� ����������� ������ �� ���������� ������;
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
-- �������� ���������� � ��������� �� �������� � ���������� ����� ������ �������;
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
-- �������� ���������� � ��������� �� �������� � ���������� ����� ������ ������� �� ��������� ���������� �������;
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
--					�������
---------------------------------------------------------------------

-- �������� ���������� � ����� �������� �������;
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
--					������
---------------------------------------------------------------------
-- �������� ������� ���������� ������� �� ������� ���� �����������.
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

-- �������� ���������� � ���� ����������� � ���������� ����������� ������ �� ��������
--drop procedure GetItemTypeWithMostSales
CREATE PROCEDURE GetItemTypeWithMostSales
    @NumberOfItemsToShow INT
AS
BEGIN
    -- ��������� ������� ��� �������� �����������
    CREATE TABLE #TempMostSoldItems (
        TypeId INT,
        TypeName NVARCHAR(100),
        TotalSales INT
    );

    -- ��������� ��������� ������� ������� � ����� ������� � ���������� ����������� ������
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

    -- ���������� ��� ���������� � ���� ������ � ���������� ����������� ������
    SELECT
        T.*,
        M.TotalSales
    FROM
        TypeOfItem T
    INNER JOIN
        #TempMostSoldItems M ON T.Id = M.TypeId;

    -- ������� ��������� �������
    DROP TABLE #TempMostSoldItems;
END;
-- �������� ���������� � ���� ����� ���������� �����������
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
--					�������
---------------------------------------------------------------------
-- ������, ������� ����������� ��� ������� � �������
CREATE TRIGGER UpdateTypeItemCount
ON Item
AFTER INSERT
AS
BEGIN
    -- ��������� ���������� ������� � ��������������� ���� � ������� TypeOfItem
    UPDATE t
    SET t.[Count] = t.[Count] + i.[Count]
    FROM TypeOfItem t
    INNER JOIN inserted i ON t.[Id] = i.[Type_ID];
END;
go

-- ������� ��� ���������� ��� ��������
CREATE TRIGGER UpdateTypeItemCount_Delete
ON Item
AFTER DELETE
AS
BEGIN
    DECLARE @DeletedTypeId INT;
    DECLARE @DeletedCount INT;

    -- �������� Id ���������� ���� ������ � ���������� ��������� ���������
    SELECT @DeletedTypeId = d.[Type_ID], @DeletedCount = d.[Count]
    FROM deleted d;

    -- ��������� ���������� ������� � ��������������� ���� � ������� TypeOfItem
    UPDATE TypeOfItem
    SET [Count] = [Count] - @DeletedCount
    WHERE [Id] = @DeletedTypeId;
END;
GO

-- ������� ��� ���������� ��� ����������
CREATE TRIGGER UpdateTypeItemCount_Update
ON Item
AFTER UPDATE
AS
BEGIN
    DECLARE @UpdatedTypeId INT;
    DECLARE @OldCount INT;
    DECLARE @NewCount INT;

    -- �������� Id ������������ ���� ������ � ������ � ����� ���������� ���������
    SELECT @UpdatedTypeId = i.[Type_ID], @OldCount = d.[Count], @NewCount = i.[Count]
    FROM inserted i
    JOIN deleted d ON i.[Id] = d.[Id];

    -- ��������� ���������� ������� � ��������������� ���� � ������� TypeOfItem
    UPDATE TypeOfItem
    SET [Count] = [Count] + (@NewCount - @OldCount)
    WHERE [Id] = @UpdatedTypeId;
END;
go

-- ������� �������� ���� �����������
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

-- ������� �������� ����� �����������
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

-- ������� �������� ���������� �� ��������
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

-- ������� �������� �����������:
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
--					�������
---------------------------------------------------------------------

-- ������� ����� �����������;
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

-- ������� ����� ����� �����������;
CREATE PROCEDURE InsertNewItemType
    @TypeName NVARCHAR(30)
AS
BEGIN
    INSERT INTO TypeOfItem ([Type])
    VALUES (@TypeName);
END;
go
-- ������� ����� ���������� �� ��������
CREATE PROCEDURE InsertNewManager
    @FirstName NVARCHAR(25),
    @LastName NVARCHAR(25)
AS
BEGIN
    INSERT INTO Manager ([FName], [LName])
    VALUES (@FirstName, @LastName);
END;
go
-- ������� ����� ���� �����������:
CREATE PROCEDURE InsertNewBuyerCompany
    @CompanyName NVARCHAR(30)
AS
BEGIN
    INSERT INTO Company ([Name])
    VALUES (@CompanyName);
END;
go

---------------------------------------------------------------------
--					����������
---------------------------------------------------------------------

-- ���������� ���������� � ������������ �����������
CREATE PROCEDURE UpdateItemInformation
    @ItemId INT,
    @ItemName NVARCHAR(30),
    @TypeId INT,
    @Count INT,
    @CostPrice DECIMAL(10, 2),
    @Price DECIMAL(10, 2)
AS
BEGIN
    -- �������� ���������� � ����������
    UPDATE Item
    SET [Name] = @ItemName,
        [Type_ID] = @TypeId,
        [Count] = @Count,
        [CostPrice] = @CostPrice,
        [Price] = @Price
    WHERE Id = @ItemId;
END;
go
-- ���������� ���������� � ������������ ������ �����������;
CREATE PROCEDURE UpdateCompanyInformation
    @CompanyName NVARCHAR(30),
    @NewCompanyName NVARCHAR(30)
AS
BEGIN
    DECLARE @CompanyId INT;

    SELECT @CompanyId = Id FROM Company WHERE [Name] = @CompanyName;

    -- ��������� ������� ����� ����������
    IF @CompanyId IS NULL
    BEGIN
        -- ���� ����� ���������� �� �������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '����� ���������� ' + @CompanyName + ' �� �������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- �������� ���������� � ����� ����������
    UPDATE Company
    SET [Name] = @NewCompanyName
    WHERE Id = @CompanyId;
END;
go
-- ���������� ���������� � ������������ ���������� �� ��������;
CREATE PROCEDURE UpdateManagerInformation
    @ManagerFirstName NVARCHAR(25),
    @ManagerLastName NVARCHAR(25),
    @NewFirstName NVARCHAR(25),
    @NewLastName NVARCHAR(25)
AS
BEGIN
    DECLARE @ManagerId INT;

    -- ����� Id ��������� �� ��� ����� � �������
    SELECT @ManagerId = Id FROM Manager WHERE [FName] = @ManagerFirstName AND [LName] = @ManagerLastName;

    -- ��������� ������� ���������
    IF @ManagerId IS NULL
    BEGIN
        -- ���� �������� �� ������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '�������� ' + @ManagerFirstName + ' ' + @ManagerLastName + ' �� ������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- �������� ���������� � ���������
    UPDATE Manager
    SET [FName] = @NewFirstName,
        [LName] = @NewLastName
    WHERE Id = @ManagerId;
END;
go
-- ���������� ���������� � ������������ ����� �����������.
CREATE PROCEDURE UpdateItemTypeInformation
    @TypeName NVARCHAR(30),
    @NewTypeName NVARCHAR(30)
AS
BEGIN
    DECLARE @TypeId INT;

    -- ����� Id ���� ���������� �� ��� ��������
    SELECT @TypeId = Id FROM TypeOfItem WHERE [Type] = @TypeName;

    -- ��������� ������� ���� ����������
    IF @TypeId IS NULL
    BEGIN
        -- ���� ��� ���������� �� ������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '��� ���������� ' + @TypeName + ' �� ������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- �������� ���������� � ���� ����������
    UPDATE TypeOfItem
    SET [Type] = @NewTypeName
    WHERE Id = @TypeId;
END;
go

---------------------------------------------------------------------
--					��������
---------------------------------------------------------------------

-- �������� �����������:
CREATE PROCEDURE DeleteItem
    @ItemName NVARCHAR(30)
AS
BEGIN
    DECLARE @ItemId INT;

    -- ����� Id ���������� �� ��� ��������
    SELECT @ItemId = Id FROM Item WHERE [Name] = @ItemName;

    -- ��������� ������� ����������
    IF @ItemId IS NULL
    BEGIN
        -- ���� ��������� �� ������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '��������� ' + @ItemName + ' �� ������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- ������� ���������
    DELETE FROM Item
    WHERE Id = @ItemId;
END;
go

CREATE PROCEDURE DeleteItemById
    @ItemId INT
AS
BEGIN
    -- ��������� ������� ���������� �� ��������� ID
    IF NOT EXISTS (SELECT 1 FROM Item WHERE Id = @ItemId)
    BEGIN
        -- ���� ��������� �� ������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '��������� � ID ' + CONVERT(NVARCHAR(10), @ItemId) + ' �� ������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- ������� ���������
    DELETE FROM Item
    WHERE Id = @ItemId;
END;
GO

-- �������� ���������� �� ��������:
CREATE PROCEDURE DeleteManager
    @ManagerFirstName NVARCHAR(25),
    @ManagerLastName NVARCHAR(25)
AS
BEGIN
    DECLARE @ManagerId INT;

    -- ����� Id ��������� �� ��� ����� � �������
    SELECT @ManagerId = Id FROM Manager WHERE [FName] = @ManagerFirstName AND [LName] = @ManagerLastName;

    -- ��������� ������� ���������
    IF @ManagerId IS NULL
    BEGIN
        -- ���� �������� �� ������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '�������� ' + @ManagerFirstName + ' ' + @ManagerLastName + ' �� ������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- ������� ���������
    DELETE FROM Manager
    WHERE Id = @ManagerId;
END;
go

CREATE PROCEDURE DeleteManagerById
    @ManagerId INT
AS
BEGIN
    -- ��������� ������� ��������� �� ��������� ID
    IF NOT EXISTS (SELECT 1 FROM Manager WHERE Id = @ManagerId)
    BEGIN
        -- ���� �������� �� ������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '�������� � ID ' + CONVERT(NVARCHAR(10), @ManagerId) + ' �� ������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- ������� ���������
    DELETE FROM Manager
    WHERE Id = @ManagerId;
END;
GO

-- �������� ����� �����������:
CREATE PROCEDURE DeleteItemType
    @TypeName NVARCHAR(30)
AS
BEGIN
    DECLARE @TypeId INT;

    -- ����� Id ���� ���������� �� ��� ��������
    SELECT @TypeId = Id FROM TypeOfItem WHERE [Type] = @TypeName;

    -- ��������� ������� ���� ����������
    IF @TypeId IS NULL
    BEGIN
        -- ���� ��� ���������� �� ������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '��� ���������� ' + @TypeName + ' �� ������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- ������� ��� ����������
    DELETE FROM TypeOfItem
    WHERE Id = @TypeId;
END;
go

CREATE PROCEDURE DeleteItemTypeById
    @TypeId INT
AS
BEGIN
    -- ��������� ������� ���� ���������� �� ��������� ��������������
    IF NOT EXISTS (SELECT 1 FROM TypeOfItem WHERE Id = @TypeId)
    BEGIN
        -- ���� ��� ���������� �� ������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '��� ���������� � ID ' + CONVERT(NVARCHAR(10), @TypeId) + ' �� ������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- ������� ��� ����������
    DELETE FROM TypeOfItem
    WHERE Id = @TypeId;
END;
GO

-- �������� ���� �����������:
CREATE PROCEDURE DeleteCompany
    @CompanyName NVARCHAR(30)
AS
BEGIN
    DECLARE @CompanyId INT;

    -- ����� Id ����� ���������� �� � ��������
    SELECT @CompanyId = Id FROM Company WHERE [Name] = @CompanyName;

    -- ��������� ������� ����� ����������
    IF @CompanyId IS NULL
    BEGIN
        -- ���� ����� ���������� �� �������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '����� ���������� ' + @CompanyName + ' �� �������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- ������� ����� ����������
    DELETE FROM Company
    WHERE Id = @CompanyId;
END;
go
CREATE PROCEDURE DeleteCompanyById
    @CompanyId INT
AS
BEGIN
    -- ��������� ������� ����� ���������� �� ��������� ��������������
    IF NOT EXISTS (SELECT 1 FROM Company WHERE Id = @CompanyId)
    BEGIN
        -- ���� ����� ���������� �� �������, ��������� ���������������� ����������
        DECLARE @ErrorMessage NVARCHAR(100) = '����� ���������� � ID ' + CONVERT(NVARCHAR(10), @CompanyId) + ' �� �������.';
        RAISERROR (@ErrorMessage, 16, 1);
        RETURN;
    END;

    -- ������� ����� ����������
    DELETE FROM Company
    WHERE Id = @CompanyId;
END;
GO




---------------------------------------------------------------------
--					�������������
---------------------------------------------------------------------

-- ������� ����� �����������
INSERT INTO TypeOfItem ([Type]) 
VALUES 
    ('���������'),
    ('�����'),
    ('�������'),
    ('��������'),
    ('����'),
    ('��������'),
    ('�������'),
    ('������'),
    ('�������'),
    ('����������'),
    ('������������ ������'),
    ('������� �����'),
    ('����������'),
    ('��������� �����'),
    ('������� ���������'),
    ('����� ��� ���������'),
    ('�������� �������'),
    ('������'),
    ('������'),
    ('�����������');
go
-- ������� ����������
INSERT INTO Manager ([FName], [LName]) 
VALUES 
    ('����', '������'),
    ('����', '������'),
    ('�����', '��������'),
    ('�����', '�������'),
    ('���������', '�������'),
    ('�����', '��������'),
    ('������', '�����'),
    ('�������', '��������'),
    ('�������', '������'),
    ('����', '�������');
go
-- ������� ���� �����������
INSERT INTO Company ([Name]) 
VALUES 
    ('��� "���� � ������"'),
    ('��� "������"'),
    ('�� "��������"'),
    ('��� "������"'),
    ('�� "�������"'),
    ('�� "���"'),
    ('��� "�������� ���"'),
    ('��� "����"'),
    ('�� "���-�����"'),
    ('��� "��������-�����"');
go
-- ������� �����������
INSERT INTO Item ([Name], [Type_ID], [Count], [CostPrice], [Price]) 
VALUES 
    ('��������� B2+', 4, 100, 50, 100),
    ('��������� HB', 1, 100, 50, 100),
    ('����� �������', 2, 200, 80, 150),
    ('������� � ������', 3, 50, 120, 200),
    ('������� "������"', 4, 30, 500, 800),
    ('���� "������"', 5, 50, 80, 120),
    ('������� �5', 6, 100, 200, 300),
    ('������� ��� �����', 7, 150, 100, 200),
    ('������ "������"', 8, 80, 30, 50),
    ('������� ������������', 9, 70, 150, 200),
    ('��������� ������', 10, 120, 60, 100),
    ('������������ ������', 11, 200, 20, 50),
    ('������� �����', 12, 100, 40, 80),
    ('���������� ��� �����', 13, 150, 50, 100),
    ('��������� �����', 14, 180, 70, 120),
    ('������� ���������', 15, 120, 70, 150),
    ('����� ��� ���������', 16, 90, 100, 200),
    ('�������� �������', 17, 200, 10, 20),
    ('����� ��� ����������', 18, 80, 150, 250),
    ('������ ��� ����������', 19, 120, 30, 50),
    ('���������� ��� ������', 20, 50, 300, 400),
    ('��������� 2B', 1, 150, 60, 110),
    ('����� ���������', 2, 220, 90, 160),
    ('������� � �������', 3, 70, 130, 220),
    ('������� "��������"', 4, 40, 600, 900),
    ('���� ������������', 5, 60, 100, 150),
    ('������� �4', 6, 120, 250, 350),
    ('������� ��� ������', 7, 170, 120, 220),
    ('������ "������"', 8, 100, 40, 60),
    ('������� ����������������', 9, 90, 200, 250),
    ('��������� �����������', 10, 150, 70, 110),
    ('������ ��� ������', 11, 250, 30, 60),
    ('����� ������������', 12, 150, 50, 100),
    ('���������� ����������������', 13, 180, 60, 120),
    ('����� ������� ��������', 14, 200, 80, 130),
    ('��������� ������� Profi', 15, 150, 80, 170),
    ('����� �������������', 16, 110, 120, 250),
    ('������� ��� ������', 17, 250, 15, 25),
    ('����� ��� �����', 18, 100, 180, 280),
    ('������ ������������', 19, 130, 40, 70),
    ('���������� ��� ����', 20, 60, 400, 500),
    ('��������� 2H', 1, 170, 70, 120),
    ('����� ������� ������', 2, 240, 100, 170),
    ('������� � ������ �������', 3, 80, 150, 250),
    ('������� "������"', 4, 50, 700, 1000),
    ('���� ������ ������������', 5, 70, 120, 180),
    ('������� �6', 6, 150, 180, 280),
    ('������� ��� ���������', 7, 190, 150, 250),
    ('������ "�������"', 8, 120, 50, 70),
    ('������� ��� �����', 9, 80, 250, 300),
    ('��������� ��������', 10, 160, 80, 130),
    ('������ �������', 11, 270, 40, 70),
    ('����� �������������', 12, 170, 60, 110),
    ('���������� ��� ��������', 13, 190, 70, 130),
    ('����� ������� �����', 14, 220, 90, 140),
    ('��������� ������� �������', 15, 180, 90, 180),
    ('����� �����������', 16, 130, 130, 280),
    ('������� ���������', 17, 270, 20, 30),
    ('����� ��� ������', 18, 120, 200, 300),
    ('������ �����', 19, 150, 50, 80),
    ('���������� ��� ��������', 20, 70, 500, 600),
    ('��������� 4B', 1, 190, 80, 130),
    ('����� ������� ������', 2, 250, 100, 160),
    ('������� � ������ ���������', 3, 90, 140, 230),
    ('������� "������������"', 4, 60, 800, 1200),
    ('���� �������������', 5, 80, 150, 200),
    ('������� � ������', 6, 170, 220, 320),
    ('������� ��� ���������', 7, 200, 170, 270),
    ('������ "�����"', 8, 130, 60, 80),
    ('������� ��� ������', 9, 100, 300, 350),
    ('��������� � �����', 10, 180, 90, 140),
    ('������ �������', 11, 300, 50, 80),
    ('����� �������������', 12, 200, 70, 120),
    ('���������� ��� �����', 13, 200, 80, 140),
    ('����� ������� �������', 14, 230, 90, 150),
    ('��������� ������� ����������������', 15, 200, 100, 200),
    ('����� ��� ��������', 16, 150, 150, 300),
    ('������� �������������', 17, 300, 25, 35),
    ('����� ��� ����� � ����������', 18, 130, 220, 320),
    ('������ ����������', 19, 170, 60, 90),
    ('���������� ��� �������', 20, 80, 600, 700);
go
-- ������� ������� ������
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
