docker run --name OrderUserServiceDB_SqlServer -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=abc123!!@" -p 1433:1433 --net e-commerce -d mcr.microsoft.com/mssql/server:2019-latest

CREATE DATABASE OrdersUserServiceDB;

 

USE OrdersUserServiceDB;

 

//to run the thing
CREATE TABLE Items (
    ItemUuid UNIQUEIDENTIFIER NOT NULL,
    OrderGuid UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    UnitPrice Decimal NOT NULL,
    PRIMARY KEY (ItemUuid, OrderGuid)   
);
 
CREATE TABLE Orders (
    OrderGuid UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    UserGuid UNIQUEIDENTIFIER NOT NULL,
    BasketGuid UNIQUEIDENTIFIER NOT NULL,
    CreatedDate DATETIME NOT NULL CONSTRAINT DF_Orders_CreatedDate DEFAULT getdate()
);

 

CREATE TABLE Users (
    UserGuid UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),    
    Username NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,   
    CreatedDate DATETIME NOT NULL CONSTRAINT DF_Users_CreatedDate DEFAULT getdate()
);

 

ALTER TABLE dbo.Items ADD CONSTRAINT FK_Items_Orders FOREIGN KEY (OrderGuid) REFERENCES dbo.Orders (OrderGuid);
ALTER TABLE dbo.Orders ADD CONSTRAINT FK_Orders_Users FOREIGN KEY (UserGuid) REFERENCES dbo.Users (UserGuid);

 

INSERT INTO "Users" ("UserGuid", "Username", "Email", "Password", "CreatedDate") VALUES ('{E8E369C0-960B-4584-9A81-F9FF9F98DBD6}', 'chris', 'ccantera@yahoo.com', 'abc123', '2023-03-31 13:56:55.483');

