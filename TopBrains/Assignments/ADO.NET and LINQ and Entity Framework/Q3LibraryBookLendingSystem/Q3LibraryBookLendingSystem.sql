
USE AdoNet;
GO

--Tables
CREATE TABLE Members (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL
);

CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1
);

CREATE TABLE Loans (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    MemberId INT NOT NULL,
    LoanDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ReturnDate DATETIME2 NULL,
    CONSTRAINT FK_Loans_Books FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Loans_Members FOREIGN KEY (MemberId) REFERENCES Members(Id) ON DELETE CASCADE
);
GO

-- Sample Data
INSERT INTO Members (Name, Email) VALUES 
('Rauhan Kumar', 'rauhan@gmail.com'),
('Shivansh', 'shivansh@gmail.com'),
('mohit', 'mohit@gmail.com');

INSERT INTO Books (Title, Author, IsAvailable) VALUES 
('Wings Of Fire', 'APJ Abdul Kalam', 1),
('Power', 'Robert Greene', 0),
('Enlightened Minds', 'APJ Abdul Kalam', 0),
('A Programmers Mind', 'Aishwarya shiva', 1),
('Just for Fun: The Story of an Accidental Revolutionary', 'Linus Torvalds', 1);

INSERT INTO Loans (BookId, MemberId, LoanDate, ReturnDate) 
VALUES (2, 1, GETUTCDATE(), NULL);

INSERT INTO Loans (BookId, MemberId, LoanDate, ReturnDate) 
VALUES (3, 2, DATEADD(DAY, -20, GETUTCDATE()), NULL);

INSERT INTO Loans (BookId, MemberId, LoanDate, ReturnDate) 
VALUES (4, 3, DATEADD(DAY, -10, GETUTCDATE()), DATEADD(DAY, -2, GETUTCDATE()));
