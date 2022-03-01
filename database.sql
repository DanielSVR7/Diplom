use master;
GO

    /*** Создание базы данных ***/
if DB_ID('ApplianceStore') is NOT NULL
	drop database ApplianceStore;		
GO
create database ApplianceStore;			
GO


use ApplianceStore;


	/*** Создание таблицы Managers ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'Managers')
	DROP TABLE Managers;						
GO
CREATE TABLE Managers
(	ManagerID	INT	    		NOT NULL,	        /*  */
	FullName	NVARCHAR(30)    NOT NULL,			/*  */
	Login		NVARCHAR(16)    NOT NULL UNIQUE,	/*  */
	Password	NVARCHAR(16)	NOT NULL,			/*  */
	CONSTRAINT PK_Managers
		PRIMARY KEY (ManagerID)
);
GO


	/*** Создание таблицы DiscountLevels ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'DiscountLevels')
	DROP TABLE DiscountLevels;						
GO
CREATE TABLE DiscountLevels
(	LevelID    	        TINYINT	    	NOT NULL,	        /*  */
	Name		        NVARCHAR(20)    NOT NULL UNIQUE,	/*  */
	AmountOfPurchases	INT				NOT NULL,
	PercentDiscount     DECIMAL(4,2)    NOT NULL,	        /*  */
	CONSTRAINT PK_DiscountLevels
		PRIMARY KEY (LevelID)
);
GO


	/*** Создание таблицы Clients ***/
GO
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'Clients')
	DROP TABLE Clients;						
GO
CREATE TABLE Clients
(	ClientID    	INT				NOT NULL,	                /*  */
	PhoneNumber		NVARCHAR(25)    NOT NULL UNIQUE,	        /*  */
	Password		NVARCHAR(16)    NOT NULL,	                /*  */
	Surname        	NVARCHAR(25)   	NOT NULL,                   /*  */   
	Firstname       NVARCHAR(25)   	NOT NULL,                   /*  */   
    Lastname        NVARCHAR(25),                  				/*  */             
	Account			Money           NOT NULL DEFAULT 0,         /*  */
	DiscountLevel	TINYINT        	NOT NULL DEFAULT 0,    		/*  */
	LastPurchase	DATETIME2(0),			                    /*  */
    RegisterDate    DATETIME2(0), /*  */
	CONSTRAINT PK_Clients
		PRIMARY KEY (ClientID),
    CONSTRAINT FK_Clients_DiscountLevel
        FOREIGN KEY (DiscountLevel)
            REFERENCES DiscountLevels(LevelID) 
);
GO


	/*** Создание таблицы Categories ***/
GO
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'Categories')
	DROP TABLE Categories;						
GO
CREATE TABLE Categories
(
	CategoryID		SMALLINT 		NOT NULL,
	CategoryName	NVARCHAR(50) 	NOT NULL,
	Description		NVARCHAR(500),
	CONSTRAINT PK_Categories
		PRIMARY KEY (CategoryID),
);
GO


	/*** Создание таблицы Manufacturers ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'Manufacturers')
	DROP TABLE Manufacturers;						
GO
CREATE TABLE Manufacturers
(
	ManufacturerID			SMALLINT	 	NOT NULL,
	CompanyName				NVARCHAR(50) 	NOT NULL,
	Country					NVARCHAR(20) 	NOT NULL,
	TechnicalSupportNumber	NVARCHAR(25),
	CONSTRAINT PK_Manufacturers
		PRIMARY KEY (ManufacturerID)
);
GO


	/*** Создание таблицы Colors ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'Colors')
	DROP TABLE Colors;						
GO
CREATE TABLE Colors
(
	ColorID			TINYINT 		NOT NULL,
	ColorName		NVARCHAR(50) 	NOT NULL,
	CONSTRAINT PK_Colors
		PRIMARY KEY (ColorID)
);
GO


	/*** Создание таблицы EnergyClasses ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'EnergyClasses')
	DROP TABLE EnergyClasses;						
GO
CREATE TABLE EnergyClasses
(
	EnergyClassID		TINYINT 		NOT NULL,
	EnergyClassName		NVARCHAR(10) 	NOT NULL,
	CONSTRAINT PK_EnergyClasses
		PRIMARY KEY (EnergyClassID)
);
GO


	/*** Создание таблицы FreezerLocations ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'FreezerLocations')
	DROP TABLE FreezerLocations;						
GO
CREATE TABLE FreezerLocations
(
	FreezerLocationID		TINYINT 		NOT NULL,
	FreezerLocationName		NVARCHAR(30) 	NOT NULL,
	CONSTRAINT PK_FreezerLocations
		PRIMARY KEY (FreezerLocationID)
);
GO


	/*** Создание таблицы BacklightTypes ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'BacklightTypes')
	DROP TABLE BacklightTypes;						
GO
CREATE TABLE BacklightTypes
(
	BacklightTypeID			TINYINT 		NOT NULL,
	BacklightTypeName		NVARCHAR(30) 	NOT NULL,
	CONSTRAINT PK_BacklightTypes
		PRIMARY KEY (BacklightTypeID)
);
GO


	/*** Создание таблицы ScreenSizes ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'ScreenSizes')
	DROP TABLE ScreenSizes;						
GO
CREATE TABLE ScreenSizes
(
	ScreenSizeID			TINYINT 	NOT NULL,
	ScreenSizeInInches		TINYINT 	NOT NULL,
	ScreenSizeInCentimeters	TINYINT		NOT NULL,
	CONSTRAINT PK_ScreenSizes
		PRIMARY KEY (ScreenSizeID)
);
GO


	/*** Создание таблицы ScreenResolutions ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'ScreenResolutions')
	DROP TABLE ScreenResolutions;						
GO
CREATE TABLE ScreenResolutions
(
	ScreenResolutionID		TINYINT 		NOT NULL,
	ScreenResolutionName	NVARCHAR(10) 	NOT NULL,
	ScreenResolution		NVARCHAR(10)	NOT NULL,
	CONSTRAINT PK_ScreenResolutions
		PRIMARY KEY (ScreenResolutionID)
);
GO


	/*** Создание таблицы OperatingSystems ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'OperatingSystems')
	DROP TABLE OperatingSystems;						
GO
CREATE TABLE OperatingSystems
(
	OperatingSystemID		TINYINT 		NOT NULL,
	OperatingSystemName		NVARCHAR(20) 	NOT NULL,
	
	CONSTRAINT PK_OperatingSystems
		PRIMARY KEY (OperatingSystemID)
);
GO


	/*** Создание таблицы Products ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'Products')
	DROP TABLE Products;						
GO
CREATE TABLE Products
(	ProductID    		INT				NOT NULL,		/*  */
	Model				NVARCHAR(100)	NOT NULL,			
	Category			SMALLINT 		NOT NULL,		/*  */
	Price 				DECIMAL(10,2)	DEFAULT 0,		/*  */
	Description			NVARCHAR(1000),
	Manufacturer		SMALLINT,
	Color 				TINYINT,
	Width				DECIMAL(6,2),
	Height				DECIMAL(6,2),					/*  */
	Depth				DECIMAL(6,2),
	Warranty			SMALLINT,
	PowerConsumption	SMALLINT,
	EnergyClass			TINYINT,
	Image 				NVARCHAR(100)	DEFAULT('Data/Images/default.png'),
	/* Холодильники*/
	FreezerLocation		TINYINT,						/*  */
	FreshnessZone		BIT,
	InverterCompressor	BIT,
	RefrigeratorVolume	SMALLINT,
	FreezerVolume		SMALLINT,
	TemperatureDisplay	BIT,
	/* Телевизоры */	
	BacklightType		TINYINT,
	ScreenSize			TINYINT,
	ScreenResolution	TINYINT,
	SmartTVSupport		BIT,
	OperatingSystem		TINYINT,
	Bluetooth			BIT,
	HDRSupport			BIT,
	/* Посудомойки и стиралки*/
	WaterConsumption	TINYINT,
	NumberOfPrograms	TINYINT,
	NumberOfPlacedSets	TINYINT,
	LaundryLoad			TINYINT,
	TemperatureRange	NVARCHAR(10),
	DirectDrive			BIT,
	MaximumSpinSpeed	SMALLINT,
	/* Микроволновки */
	InternalVolume		SMALLINT,
	Grill				BIT,
	CONSTRAINT PK_Products
		PRIMARY KEY (ProductID),
	CONSTRAINT FK_Products_Categories
		FOREIGN KEY (Category)
			REFERENCES Categories(CategoryID),
	CONSTRAINT FK_Products_Manufacturers
		FOREIGN KEY (Manufacturer)
			REFERENCES Manufacturers(ManufacturerID),
	CONSTRAINT FK_Products_Colors
		FOREIGN KEY (Color)
			REFERENCES Colors(ColorID),
	CONSTRAINT FK_Products_EnergyClasses
		FOREIGN KEY (EnergyClass)
			REFERENCES EnergyClasses(EnergyClassID),
	CONSTRAINT FK_Products_FreezerLocations
		FOREIGN KEY (FreezerLocation)
			REFERENCES FreezerLocations(FreezerLocationID),
	CONSTRAINT FK_Products_BacklightTypes
		FOREIGN KEY (BacklightType)
			REFERENCES BacklightTypes(BacklightTypeID),
	CONSTRAINT FK_Products_ScreenSizes
		FOREIGN KEY (ScreenSize)
			REFERENCES ScreenSizes(ScreenSizeID),
	CONSTRAINT FK_Products_ScreenResolutions
		FOREIGN KEY (ScreenResolution)
			REFERENCES ScreenResolutions(ScreenResolutionID),
	CONSTRAINT FK_Products_OperatingSystems
		FOREIGN KEY (OperatingSystem)
			REFERENCES OperatingSystems(OperatingSystemID)
);
GO


	/*** Создание таблицы Purchases ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'Purchases')
	DROP TABLE Purchases;						
GO
CREATE TABLE Purchases
(
	PurchaseID		INT 			NOT NULL,
	PurchaseDate	DATETIME2(0) 	NOT NULL DEFAULT GETDATE(),
	ClientID		INT				NOT NULL,
	CONSTRAINT PK_Purchases
		PRIMARY KEY (PurchaseID),
	CONSTRAINT FK_Purchases_Clients
		FOREIGN KEY (ClientID)
			REFERENCES Clients(ClientID)
);
GO


	/*** Создание таблицы PurchaseItems ***/
IF EXISTS (SELECT * FROM sys.tables WHERE NAME = 'PurchaseItems')
	DROP TABLE PurchaseItems;						
GO
CREATE TABLE PurchaseItems
(
	PurchaseID		INT 		NOT NULL,
	ProductID		INT 		NOT NULL,
	ProductCount	SMALLINT	NOT NULL DEFAULT 1,
	CONSTRAINT PK_PurchaseItems
		PRIMARY KEY (PurchaseID, ProductID),
	CONSTRAINT FK_PurchaseItems_Purchases
		FOREIGN KEY (PurchaseID)
			REFERENCES Purchases(PurchaseID),
	CONSTRAINT FK_PurchaseItems_Products
		FOREIGN KEY (ProductID)
			REFERENCES Products(ProductID)
);
GO


INSERT INTO Managers(ManagerID, FullName, Login, Password)
	VALUES
(1, 'Свириденко Даниил Дмитриевич', 'abc', 'def'),
(2, 'Иванов Иван Иванович', '2','2');
GO


INSERT INTO DiscountLevels(LevelID, Name, AmountOfPurchases, PercentDiscount)
	VALUES
(0, 'Нет уровня', 0, 0),
(1, 'Бронза', 5000, 1),
(2, 'Серебро', 10000, 1.5),
(3, 'Золото', 20000, 3),
(4, 'Платина', 40000, 6),
(5, 'VIP', 100000, 12);
GO


INSERT INTO Clients(ClientID, PhoneNumber, Password, Surname, Firstname, Lastname, RegisterDate)
	VALUES
(0, 'm', 'm', 'Менеджер', '', '', GETDATE()),
(1, '4', '4', 'Иванова', 'Виктория', 'Тимофеевна', GETDATE()),
(2, '3', '3', 'Денисов', 'Владислав', 'Мирославович', GETDATE()),
(3, '1', '1', 'Свириденко', 'Даниил', 'Дмитриевич', GETDATE());
GO


INSERT INTO Categories(CategoryID, CategoryName, Description)
	VALUES
(1, 'Телевизоры', null),
(2, 'Холодильники', 'Холодильники и холодильное оборудование'),
(3, 'Посудомоечные машины', null),
(4, 'Стиральные машины', null),
(5, 'Микроволновки', 'Ваша пища всегда будет теплой');
GO


INSERT INTO Manufacturers(ManufacturerID, CompanyName, Country, TechnicalSupportNumber)
	VALUES
(1, 'Samsung', 'Корея', '8-800-555-55-55'),
(2, 'LG', 'Корея', '8-800-200-76-76'),
(3, 'Xiaomi', 'Китай', '8-800-775-66-15'),
(4, 'DEXP', 'Китай', '8-800-770-78-88');
GO


INSERT INTO Colors(ColorID, ColorName)
	VALUES
(1, 'Чёрный'),
(2, 'Серебристый'),
(3, 'Серый'),
(4, 'Белый'),
(5, 'Красный');
GO


INSERT INTO EnergyClasses(EnergyClassID, EnergyClassName)
	VALUES
(1, 'A++'),
(2, 'A+'),
(3, 'A'),
(4, 'B'),
(5, 'C'),
(6, 'D'),
(7, 'E'),
(8, 'F'),
(9, 'G');
GO


INSERT INTO FreezerLocations(FreezerLocationID, FreezerLocationName)
	VALUES
(1, 'с морозильной камерой сверху'),
(2, 'с морозильной камерой снизу'),
(3, 'Side-By-Side'),
(4, 'многодверный');
GO


INSERT INTO BacklightTypes(BacklightTypeID, BacklightTypeName)
	VALUES
(1, 'Direct LED'),
(2, 'Edge LED'),
(3, 'OLED'),
(4, 'LCD');
GO


INSERT INTO ScreenSizes(ScreenSizeID, ScreenSizeInInches, ScreenSizeInCentimeters)
	VALUES
(1, 24, 60),
(2, 27, 68),
(3, 32, 81),
(4, 40, 102),
(5, 43, 109),
(6, 47, 120),
(7, 50, 127),
(8, 55, 140),
(9, 60, 152),
(10, 65, 165),
(11, 75, 191),
(12, 80, 203), 
(13, 85, 216);
GO


INSERT INTO ScreenResolutions(ScreenResolutionID, ScreenResolutionName, ScreenResolution)
	VALUES
(1, '8K UltraHD', '7680x4320'),
(2, '4K UltraHD', '3840x2160'),
(3, 'FullHD', '1920x1080'),
(4, 'HD', '1366x768');
GO


INSERT INTO OperatingSystems(OperatingSystemID, OperatingSystemName)
	VALUES
(1, 'Android TV'),
(2, 'Tizen'),
(3, 'Web OS'),
(4, 'Android (AOSP)'),
(5, 'Яндекс ТВ');
GO


INSERT INTO Products	(ProductID, Model, Category, Price, Description, Manufacturer, Color, 
						Width, Height, Depth, Warranty, PowerConsumption, EnergyClass, Image, 
						FreezerLocation, FreshnessZone, InverterCompressor, RefrigeratorVolume, 
						FreezerVolume, TemperatureDisplay)
	VALUES
(1, 'GA-B459CLWL', 2, 37999, 
'Холодильник LG GA-B459CLWL в матовом серебристом корпусе имеет солидную вместительность – 341 л. Благодаря этому в нем можно разместить большие продуктовые запасы. Морозильная камера на 107 л состоит из 3 выдвижных ящиков с возможностью замораживания до 16 кг продуктов в сутки. Режим «Отпуск» позволит минимизировать и без того символическое энергопотребление во время вашего отсутствия. Индикатор температуры и внешний дисплей делают модель понятной в обращении. Блокировка управления отвечает за безопасность эксплуатации.',
	2, 2, 59.5, 186, 68.2, 12, 309, 2, 'Data/Images/Products/Refregerators/GA-B459CLWL.jpg', 2, 1, 1, 234, 107, 1),
(2, 'SBS510M', 2, 49999, 
'Холодильник DEXP SBS510M, оборудованный распашными дверями, обладает значительным полезным объемом: он равен 510 л. На холодильную и морозильную камеры приходится 335 и 175 л соответственно. Модель подойдет для размещения значительного количества продуктов и напитков. Холодильник оснащен дисплеем, на котором отображается информация о температуре в обеих камерах. В число функций устройства входят суперохлаждение, суперзаморозка, режим отпуска и блокировка от детей. Заниматься размораживанием холодильника не потребуется: в обеих камерах размораживание происходит автоматически, с использованием технологии No Frost.', 
4, 2, 89.5, 178.8, 71, 36, 405, 2, 'Data/Images/Products/Refregerators/SBS510M.png', 3, 0, 0, 335, 175, 1);
GO


INSERT INTO Products	(ProductID, Model, Category, Price, Description, Manufacturer, Color, 
						Warranty, Image,	BacklightType, ScreenSize, ScreenResolution, 
						SmartTVSupport, OperatingSystem,	Bluetooth, HDRSupport)
	VALUES
(3, 'Mi TV 4S 50', 1, 38999, 
'Телевизор LED Xiaomi Mi TV 4S 50 является воплощением элегантного фирменного стиля и технологичности. Он оборудован 50-дюймовым экраном с разрешением 3840x2160 пикселей, технологией расширенного динамического диапазона цветов и подсветкой Direct LED. Это означает, что с любого угла обзора картинка будет впечатлять детализацией и естественной цветопередачей. Тонкая рамка позволяет максимально погрузиться в просмотр. Акустическая система выходной мощностью 20 Вт воспроизводит чистый звук с пространственным эффектом.', 
3, 3, 12, 'Data/Images/Products/TVs/MiTV4S50.jpg', 1, 7, 2, 1, 1, 1, 1),
(4, 'UE43TU7002UXRU', 1, 37499, 
'43-дюймовый телевизор LED Samsung UE43TU7002UXRU сочетает в себе высокий уровень функциональных возможностей и безупречное качество изображения. Разрешение модели (3840x2160) соответствует формату 4K. В устройстве реализована поддержка HDR. Подсветка экрана Edge LED отличается высоким уровнем равномерности.', 
1, 1, 12, 'Data/Images/Products/TVs/UE43TU7002UXRU.jpg', 2, 5, 2, 1, 2, 1, 1),
(5, '65UP75006LF', 1, 54999, 
'Телевизор LED LG 65UP75006LF является удобным аппаратом для просмотра как стандартных телевизионных программ, так и для потоковой трансляции видео из Интернета. Он выполнен в черном корпусе с диагональю экрана 65" (165 см) и устанавливается на подставку, которая крепится по обе стороны корпуса, обеспечивая устойчивость устройства и не позволяя ему наклоняться. Благодаря модулю Wi-Fi, установленному на LG 65UP75006LF, вы сможете насладиться плавностью потоковой трансляции видео без задержек.', 
2, 1, 12, 'Data/Images/Products/TVs/65UP75006LF.jpg', 
1, 10, 2, 1, 3, 1, 1);
GO

INSERT INTO Purchases	(PurchaseID, ClientID)
	VALUES	(0, 0);
GO


IF EXISTS (SELECT * FROM sys.server_triggers WHERE NAME = 'T_Purchases_Insert')  
	DROP TRIGGER T_Purchases_Insert ON ALL SERVER;
GO  
CREATE TRIGGER T_Purchases_Insert
	ON Purchases AFTER INSERT
	AS 	UPDATE Clients
			SET LastPurchase = GETDATE()
				WHERE ClientID = (SELECT ClientID FROM inserted)
		UPDATE Purchases
			SET PurchaseDate = GETDATE()
				WHERE PurchaseID = (SELECT PurchaseID FROM inserted)
GO

IF EXISTS (SELECT * FROM sys.server_triggers WHERE NAME = 'T_Clients_Insert')  
	DROP TRIGGER T_Clients_Insert ON ALL SERVER;
GO  
CREATE TRIGGER T_Clients_Insert
	ON Clients AFTER INSERT
	AS 	UPDATE Clients
			SET RegisterDate = GETDATE()
				WHERE ClientID = (SELECT ClientID FROM inserted)
GO