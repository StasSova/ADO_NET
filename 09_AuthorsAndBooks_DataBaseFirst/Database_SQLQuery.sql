use master
go

create database AuthorAndBook
go
use AuthorAndBook
go

create table Authors(
	[Id] int not null primary key identity(1,1),
	[FullName] nvarchar(30) not null unique
)
go

create table Books(
	[Id] int primary key identity (1,1),
	[AuthorId] int foreign key references Authors(Id),
	[Name] nvarchar(60) not null 
)
go


INSERT INTO Authors (FullName)
VALUES
    ('������ �� ����-��������'),
    ('��� �������'),
    ('����� �����������'),
    ('������ ���������'),
    ('������ ������'),
    ('�������� �������'),
    ('����� �������'),
    ('����� �����'),
    ('����� ����� ��'),
    ('������ �������'),
    ('�������� ������ ������'),
    ('������'),
    ('������ ����'),
    ('������ ������'),
    ('������ ����')
GO

INSERT INTO Books (AuthorId, Name)
VALUES
    (1, '��������� �����'),
    (1, '����� �����'),
    (2, '����� � ���'),
    (2, '���� ��������'),
    (3, '������������ � ���������'),
    (3, '������ ����������'),
    (4, '������ � ����'),
    (4, '�� ��� ������ �������'),
    (5, '1984'),
    (5, '������� ����'),
    (6, '��������� ������'),
    (6, '������� � ������� ������'),
    (7, '����� ������ � ����������� ������'),
    (7, '����� ������ � ������ �������'),
    (8, '�������� � �������������'),
    (8, '���'),
    (9, '����� ������� ������'),
    (9, '������ �����'),
    (10, '������ �����'),
    (10, '����� �����������'),
    (11, '��� ��� �����������'),
    (11, '����������� �����'),
    (12, '��������� �����'),
    (12, '������'),
    (13, '���'),
    (13, '������� ����'),
    (14, '���� ���������'),
    (14, '���� �����'),
    (15, '������ ��������� �����'),
    (15, '����� ����')
GO