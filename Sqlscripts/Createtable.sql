Create table Addresses (
AddressID INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
StreetName VARCHAR(35),
Housenumber varchar(10),
City VARCHAR(50),
Postalcode int
);

CREATE TABLE Person (
    PersonID INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    FirstName VARCHAR(20),
    LastName VARCHAR(25),
    Email VARCHAR(60),
    PhoneNumber VARCHAR(20),
    AddressID INT,
    FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID)
);

CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    PersonID INT,
    Fullname varchar(255),
    LastBuy Datetime
    FOREIGN KEY (PersonID) REFERENCES Person(PersonID)
);

CREATE TABLE SalesOrderHeader (
OrderNumber int Primary Key IDENTITY (1,1) ,
CreationTime DATE Default(GETDATE()),
ImplementationTime dateTime,
CustomerID int FOREIGN KEY REFERENCES Customer(CustomerID),
Status int
);

CREATE TABLE OrderLines (
OrderLineID int PRIMARY KEY IDENTITY (1,1),
OrderNumber int FOREIGN KEY REFERENCES SalesOrderHeader(OrderNumber),
Vare varchar(255),
Pris Decimal,
Antal Int
);

CREATE TABLE companies (
CompanyID int PRIMARY KEY IDENTITY (1,1),
CompanyName varchar(32),
StreetName varchar(32),
Housenumber varchar(32),
Zipcode varchar(10),
City varchar(32),
Country varchar(32),
Currency int
);

CREATE TABLE Products (
ProductId int PRIMARY KEY IDENTITY (1,1) not null,
ProductName varchar(32),
ProductDescription varchar(200),
ProductQuantity int,
ProductUnits int,
ProductSalesPrice decimal,
ProductPurchasePrice decimal,
ProductAvanceInPercent decimal,
ProductAvanceInKr decimal
);
