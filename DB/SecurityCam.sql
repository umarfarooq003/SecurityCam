CREATE TABLE Login (
    Id INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing ID
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Type NVARCHAR(20) DEFAULT 'user',
    Approval BIT DEFAULT 0             -- New column for approval with default value false (0)
);



CREATE TABLE Alerts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Message NVARCHAR(255) NOT NULL,
    ImagePath NVARCHAR(255) NOT NULL,
	Timestamp DATETIME NOT NULL,
	IsRead BIT NOT NULL DEFAULT 0
);





CREATE TABLE ProblemReports (
    Id INT IDENTITY PRIMARY KEY,
    ImageData VARBINARY(MAX),
    Description NVARCHAR(MAX),
    ReportDate DATETIME DEFAULT GETDATE()
);



select * from login