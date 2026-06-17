-- ============================================================
-- LibraryDB_Setup.sql
-- Creates the LibraryDB database and all tables required by
-- the Library Management System WinForms application.
--
-- Matches the column names referenced in App.Core/Services/*.cs
-- exactly. No FK constraints are created at the database level
-- (NVARCHAR primary keys only) — referential integrity between
-- Loans -> Books / Members is enforced in the application layer,
-- the same convention the reference HelpDeskTicketSystem project
-- uses for its Tickets/Responses tables.
-- ============================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'LibraryDB')
BEGIN
    CREATE DATABASE LibraryDB;
END
GO

USE LibraryDB;
GO

-- ------------------------------------------------------------
-- Members
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Members', 'U') IS NOT NULL DROP TABLE dbo.Members;
GO
CREATE TABLE dbo.Members
(
    Id    NVARCHAR(20)  NOT NULL PRIMARY KEY,
    Name  NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NULL,
    Phone NVARCHAR(30)  NULL
);
GO

-- ------------------------------------------------------------
-- Librarians
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Librarians', 'U') IS NOT NULL DROP TABLE dbo.Librarians;
GO
CREATE TABLE dbo.Librarians
(
    Id    NVARCHAR(20)  NOT NULL PRIMARY KEY,
    Name  NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NULL
);
GO

-- ------------------------------------------------------------
-- Books
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Books', 'U') IS NOT NULL DROP TABLE dbo.Books;
GO
CREATE TABLE dbo.Books
(
    Id        NVARCHAR(20)  NOT NULL PRIMARY KEY,
    Title     NVARCHAR(250) NOT NULL,
    Author    NVARCHAR(150) NOT NULL,
    ISBN      NVARCHAR(30)  NULL,
    Genre     NVARCHAR(30)  NOT NULL DEFAULT ('Fiction'),
    Status    NVARCHAR(30)  NOT NULL DEFAULT ('Available'),
    AddedDate DATETIME      NOT NULL DEFAULT (GETDATE())
);
GO

-- ------------------------------------------------------------
-- Loans (links a Book to a Member; no FK constraints, see header)
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Loans', 'U') IS NOT NULL DROP TABLE dbo.Loans;
GO
CREATE TABLE dbo.Loans
(
    Id         NVARCHAR(20) NOT NULL PRIMARY KEY,
    BookId     NVARCHAR(20) NOT NULL,
    MemberId   NVARCHAR(20) NOT NULL,
    IssueDate  DATETIME     NOT NULL DEFAULT (GETDATE()),
    DueDate    DATETIME     NOT NULL,
    ReturnDate DATETIME     NULL,
    Status     NVARCHAR(30) NOT NULL DEFAULT ('Issued')
);
GO

-- ============================================================
-- Optional sample data — uncomment to seed the database with a
-- few rows for an instant demo. Safe to skip entirely; the app
-- works against empty tables.
-- ============================================================
/*
INSERT INTO dbo.Members (Id, Name, Email, Phone) VALUES
    ('MB-000000001', 'Aisha Khan',     'aisha.khan@example.com',     '555-0101'),
    ('MB-000000002', 'Daniel Brooks',  'daniel.brooks@example.com',  '555-0102');

INSERT INTO dbo.Librarians (Id, Name, Email) VALUES
    ('LB-000000001', 'Farah Iqbal',    'farah.iqbal@example.com'),
    ('LB-000000002', 'Tom Whitfield',  'tom.whitfield@example.com');

INSERT INTO dbo.Books (Id, Title, Author, ISBN, Genre, Status, AddedDate) VALUES
    ('BK-000000001', 'Clean Code',                 'Robert C. Martin', '9780132350884', 'NonFiction', 'Available', GETDATE()),
    ('BK-000000002', 'The Hobbit',                  'J.R.R. Tolkien',   '9780547928227', 'Fiction',    'Borrowed',  GETDATE()),
    ('BK-000000003', 'Oxford English Dictionary',   'Oxford Press',     '9780198611868', 'Reference',  'Available', GETDATE()),
    ('BK-000000004', 'National Geographic - March', 'NatGeo Staff',     '9991234567000', 'Periodical', 'Available', GETDATE());

INSERT INTO dbo.Loans (Id, BookId, MemberId, IssueDate, DueDate, ReturnDate, Status) VALUES
    ('LN-000000001', 'BK-000000002', 'MB-000000001', GETDATE(), DATEADD(DAY, 14, GETDATE()), NULL, 'Issued');
*/
