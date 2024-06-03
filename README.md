# DemExam
Scaffold-DbContext "Server=192.112.01.1; User=NameUser; Password=Pass; Database=NameDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -f -OutputDir DataApp/Models

CREATE DATABASE DEMEXAM
GO
USE[DEMEXAM]

GO
CREATE TABLE[FullName] (
    Id uniqueidentifier NOT NULL DEFAULT NEWID(),
    FirstName nvarchar(50) NOT NULL,
    SecondName nvarchar(50) NOT NULL,
    Patronymic nvarchar(50),

    CONSTRAINT PK_FullName_Id PRIMARY KEY CLUSTERED(Id),
)
GO

CREATE TABLE[Role] (
    Id uniqueidentifier NOT NULL DEFAULT NEWID(),
    RoleName nvarchar(255) NOT NULL,

    CONSTRAINT PK_Role_Id PRIMARY KEY CLUSTERED(Id),
    CONSTRAINT AK_Role_Name UNIQUE(RoleName),
)
GO
CREATE TABLE[User] (
    Id uniqueidentifier NOT NULL DEFAULT NEWID(),
    FullNameId uniqueidentifier NOT NULL DEFAULT NEWID(),
    RoleId uniqueidentifier NOT NULL DEFAULT NEWID(),
    Login nvarchar(20) NOT NULL,
    Password nvarchar(255) NOT NULL,
    DateOfCreation datetime,
    SessionKey varchar(100),
    Age int CONSTRAINT CHK_Age CHECK(Age >= 14 and Age <= 100),
    Email nvarchar(255) CONSTRAINT CHK_Email CHECK(Email LIKE '%@%.%'),
    Phone varchar(11) CONSTRAINT CHK_Phone CHECK(Phone LIKE '89_________'),


    CONSTRAINT AK_User_Login UNIQUE(Login),
    CONSTRAINT PK_User_Id PRIMARY KEY CLUSTERED(Id),
    CONSTRAINT FK_User_FullName

    FOREIGN KEY(FullNameId) REFERENCES[FullName] (Id),
    CONSTRAINT FK_User_Role

    FOREIGN KEY(RoleId) REFERENCES[Role] (Id),
)
