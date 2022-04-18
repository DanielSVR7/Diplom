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
(3, 'Стиральные машины', null),
(4, 'Микроволновки', 'Ваша пища всегда будет теплой');
GO


INSERT INTO Manufacturers(ManufacturerID, CompanyName, Country, TechnicalSupportNumber)
	VALUES
(1, 'Samsung', 'Корея', '8-800-555-55-55'),
(2, 'LG', 'Корея', '8-800-200-76-76'),
(3, 'Xiaomi', 'Китай', '8-800-775-66-15'),
(4, 'DEXP', 'Китай', '8-800-770-78-88'),
(5, 'INDESIT', 'Китай', '8-800-333-38-87'),
(6, 'Bosch', 'Германия', '8-800-200-29-61'),
(7, 'Gorenje', 'Словения', '8-800-700-05-15');
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
(5, 'Яндекс ТВ'),
(6, 'Без ОС');
GO


INSERT INTO Products	(ProductID, Model, Category, Price, Description, Manufacturer, Color, 
						Width, Height, Depth, Warranty, PowerConsumption, EnergyClass, Image, 
						FreezerLocation, FreshnessZone, InverterCompressor, RefrigeratorVolume, 
						FreezerVolume, TemperatureDisplay)
	VALUES
(1, 'GA-B459CLWL', 2, 37999, 
'Холодильник LG GA-B459CLWL в матовом серебристом корпусе имеет солидную вместительность – 341 л. Благодаря этому в нем можно разместить большие продуктовые запасы. Морозильная камера на 107 л состоит из 3 выдвижных ящиков с возможностью замораживания до 16 кг продуктов в сутки. Режим «Отпуск» позволит минимизировать и без того символическое энергопотребление во время вашего отсутствия. Индикатор температуры и внешний дисплей делают модель понятной в обращении. Блокировка управления отвечает за безопасность эксплуатации.',
2, 2, 59.5, 186, 68.2, 12, 309, 2, 'Data/Images/Products/Refregerators/GA-B459CLWL.jpg', 
2, 1, 1, 234, 107, 1),

(2, 'SBS510M', 2, 49999, 
'Холодильник DEXP SBS510M, оборудованный распашными дверями, обладает значительным полезным объемом: он равен 510 л. На холодильную и морозильную камеры приходится 335 и 175 л соответственно. Модель подойдет для размещения значительного количества продуктов и напитков. Холодильник оснащен дисплеем, на котором отображается информация о температуре в обеих камерах. В число функций устройства входят суперохлаждение, суперзаморозка, режим отпуска и блокировка от детей. Заниматься размораживанием холодильника не потребуется: в обеих камерах размораживание происходит автоматически, с использованием технологии No Frost.', 
4, 2, 89.5, 178.8, 71, 36, 405, 2, 'Data/Images/Products/Refregerators/SBS510M.png', 
3, 0, 0, 335, 175, 1),

(3, 'RB37A5000SA/WT', 2, 59999, 
'Холодильник Samsung RB37A5000SA/WT − удобный холодильный аппарат, выполненный в серебристом корпусе размером 59.5x201x65 см. Это двухкамерный холодильник с нижним расположением морозильной камеры. Его общая вместимость соответствует 367 л: на холодильное отделение приходится 269 л, а на морозильную камеру − 98 л. Благодаря нижнему расположению морозильной камеры вы получаете легкий доступ к продуктам, хранящимся в ящиках. Прозрачные стенки ящиков обеспечивают обзор их содержимого, что позволяет не открывать их все, а открыть нужный ящик, достав то, что необходимо.',
1, 2, 59.5, 201, 66, 12, 314, 2, 'Data/Images/Products/Refregerators/RB37A5000SAWT.jpg', 
2, 1, 1, 269, 98, 1),

(4, 'GC-Q22FTBKL', 2, 179999, 
'Холодильник LG GC-Q22FTBKL – шикарный выбор для кухни большой семьи и людей, которые любят делать солидные продуктовые запасы. Прибор состоит из 2 независимых камер. Объем морозильной составляет 143 л, а холодильной – 315 л. Оба отсека оснащены системой No Frost, благодаря чему их не нужно размораживать вручную. Температура в морозильной камере, состоящей из 6 ящиков, способна опускаться до -24°. Это значит, что внутри можно держать большой запас замороженного мяса, рыбы, овощей и ягод. Холодильное отделение представлено 3 большими полками, двумя ящиками с зоной свежести для скоропортящихся продуктов и пластиковыми полочками в дверцах.',
2, 1, 83.5, 178.7, 73.4, 12, 370, 2, 'Data/Images/Products/Refregerators/GC-Q22FTBKL.jpg',
4, 1, 1, 315, 143, 1),

(5, 'RS62R5031B4/WT', 2, 124999,
'Холодильник SAMSUNG RS62R5031B4/WT в матовом черном корпусе состоит из 2 больших камер. Их совокупный объем составляет 647 л. Морозильная камера состоит из 2 больших выдвижных ящиков и 3 полок. Также в дверцах обеих камер располагаются пластиковые «балкончики» для хранения продуктов в герметичных упаковках. Опции суперзаморозки и суперохлаждения отвечают за то, чтобы в камерах быстро создались условия для быстрого охлаждения запасов.
Холодильная камера представлена 4 полками и 2 ящиками для овощей и фруктов. В сочетании оба отсека холодильника SAMSUNG RS62R5031B4/WT позволяют хранить колоссальное количество продуктов, что будет по достоинству оценено любителями делать запасы и большими семьями.',
1, 1, 91.2, 178, 71.6, 12, 420, 2, 'Data/Images/Products/Refregerators/RS62R5031B4WT.jpg',
3, 1, 1, 418, 229, 1),

(6, 'RTM 16', 2, 27999,
'Холодильник INDESIT RTM 16 выполнен в белом корпусе размерами 60x167x63 см. Общий объем устройства составляет 296 л: он распределяется между двумя камерами – морозильной (51 л) и холодильной (245 л). Морозильное отделение находится в верхней части корпуса прибора. Пространство в нем разделено одной полочкой. Модель предусматривает ручное размораживание морозильного отделения и капельное размораживание холодильного, что защищает ваши продукты от пересыхания, гарантируя поддержание оптимальной влажности.',
5, 4, 60, 167, 63, 12, 317.55, 3, 'Data/Images/Products/Refregerators/RTM 16.jpg',
1, 0, 0, 245, 51, 0);



INSERT INTO Products	(ProductID, Model, Category, Price, Description, Manufacturer, Color, 
						Warranty, Image,	BacklightType, ScreenSize, ScreenResolution, 
						SmartTVSupport, OperatingSystem,	Bluetooth, HDRSupport)
	VALUES
(7, 'Mi TV 4S 50', 1, 38999, 
'Телевизор LED Xiaomi Mi TV 4S 50 является воплощением элегантного фирменного стиля и технологичности. Он оборудован 50-дюймовым экраном с разрешением 3840x2160 пикселей, технологией расширенного динамического диапазона цветов и подсветкой Direct LED. Это означает, что с любого угла обзора картинка будет впечатлять детализацией и естественной цветопередачей. Тонкая рамка позволяет максимально погрузиться в просмотр. Акустическая система выходной мощностью 20 Вт воспроизводит чистый звук с пространственным эффектом.', 
3, 3, 12, 'Data/Images/Products/TVs/MiTV4S50.jpg', 1, 7, 2, 1, 1, 1, 1),

(8, 'UE43TU7002UXRU', 1, 37499, 
'43-дюймовый телевизор LED Samsung UE43TU7002UXRU сочетает в себе высокий уровень функциональных возможностей и безупречное качество изображения. Разрешение модели (3840x2160) соответствует формату 4K. В устройстве реализована поддержка HDR. Подсветка экрана Edge LED отличается высоким уровнем равномерности.', 
1, 1, 12, 'Data/Images/Products/TVs/UE43TU7002UXRU.jpg', 2, 5, 2, 1, 2, 1, 1),

(9, '65UP75006LF', 1, 54999, 
'Телевизор LED LG 65UP75006LF является удобным аппаратом для просмотра как стандартных телевизионных программ, так и для потоковой трансляции видео из Интернета. Он выполнен в черном корпусе с диагональю экрана 65" (165 см) и устанавливается на подставку, которая крепится по обе стороны корпуса, обеспечивая устойчивость устройства и не позволяя ему наклоняться. Благодаря модулю Wi-Fi, установленному на LG 65UP75006LF, вы сможете насладиться плавностью потоковой трансляции видео без задержек.', 
2, 1, 12, 'Data/Images/Products/TVs/65UP75006LF.jpg', 1, 10, 2, 1, 3, 1, 1),

(10, 'Mi TV Q1 75', 1, 139999,
'Mi TV Q1 75" может отображать 1,07 миллиарда цветов, делая каждый оттенок точным и чистым, что приводит к более плавным переходам. QLED-дисплей использует наноразмерный материал квантовых точек для достижения более широкой цветовой гаммы и показывает изображения более яркие, яркие и реалистичные. Под дисплеем, есть 384 светодиода в 192 локализованных зонах, которые могут самостоятельно регулировать яркость для повышения цветового контраста. Эта технология делает яркие элементы ярче, а темные-темнее, значительно улучшая четкость и яркость изображения. Поддерживая Dolby Vision и HDR10+, QLED-экран может обрабатывать и оптимизировать качество изображения кадр за кадром, четко показывая каждую яркую и темную деталь с качеством изображения, более близким к реальности.',
3, 2, 24, 'Data/Images/Products/TVs/MiTVQ175.jpg', 1, 11, 2, 1, 1, 1, 1),

(11, 'F42F7000C/G', 1, 19999, 
'Телевизор LED DEXP F42F7000C/G является стандартным устройством для просмотра телеканалов и воспроизведения контента при подключении дополнительной техники. Благодаря поддержке интерфейса HDMI, представленного тремя разъемами на корпусе, вы получаете возможность смотреть видео в высоком качестве. Аппарат поддерживает разрешение Full HD, соответствующее 1920x1080. Матрица VA характеризуется оптимальными показателями контрастности и яркости, чтобы в итоге вы могли получить максимально реалистичную и насыщенную картинку. Углы обзора 170°/170° позволяют экрану не искажать изображение, гарантируя четкость его отображения вне зависимости от ракурса просмотра.',
4, 3, 36, 'Data/Images/Products/TVs/F42F7000CG.jpg', 2, 5, 3, 0, 6, 0, 0),

(12, 'UE50AU9010UXRU', 1, 67999,
'Телевизор LED Samsung UE50AU9010UXRU примечателен элегантным цветовым оформлением и безрамочным дизайном. На экране диагональю 50 дюйма с разрешением 3840x2160 пикселей отображается детализированная картинка с яркими и насыщенными цветами. Технология HDR помогает добиться глубины воспроизводимых оттенков. За передачу реалистичного звука отвечает акустическая система выходной мощностью 20 Вт при поддержке технологии Dolby Digital Plus.',
1, 4, 12, 'Data/Images/Products/TVs/UE50AU9010UXRU.jpg', 2, 7, 3, 1, 2, 1, 1);
GO

INSERT INTO Products	(ProductID, Model, Category, Price, Description, Manufacturer, Color, 
						Warranty, EnergyClass, Image, WaterConsumption, NumberOfPrograms, LaundryLoad,
						TemperatureRange, DirectDrive, MaximumSpinSpeed)
	VALUES
(13, 'F1296NDS1', 3, 42999, 
'Стиральная машина LG F1296NDS1 станет практичным решением вопроса с чистотой белья даже в небольшой ванной. Ее ширина и глубина составляют 60 и 44 см соответственно. При этом за один раз прибор способен очистить до 6 кг белья за цикл. За счет большого количества встроенных программ получится создать оптимальные условия для стирки вещей из любой ткани. Для дополнительной чистоты можно обработать вещи паром. Если вам сейчас неудобно стирать, перенесите время начала работы прибора на удобный момент с помощью таймера на 19 ч.',
2, 4, 12, 3, 'Data/Images/Products/WashingMachines/F1296NDS1.jpg', 56, 13, 6, '20-95', 1, 1200),

(14, 'F2V5NG0W', 3, 56999,
'Стирально-сушильная машина LG F2V5NG0W обеспечивает качественный уход за состоянием ваших вещей благодаря поддержке ряда фирменных технологий. Интеллектуальная система определения типа ткани AI DD, в память которой заложено около 20000 возможных сочетаний ткани, обеспечивает распознавание веса и характеристик ткани для точного подбора оптимальных параметров стирки. Функция сушки с тремя режимами (стандартная, бережная и легкая) способствует правильному уходу за состоянием ваших вещей.',
2, 4, 12, 5, 'Data/Images/Products/WashingMachines/F2V5NG0W.jpg', 50, 14, 6, '0-95', 1, 1200),

(15, 'Serie 4 PerfectCare WLP20266OE', 3, 44999,
'Стиральная машина Bosch WLP20266OE вместительностью 6.5 кг способна позаботиться о чистоте большого количества белья. Благодаря тому что она имеет 15 встроенных программ и множество полезных опций, от ее владельца не потребуется для этого дополнительных усилий. В основе работы прибора – бесконтактный мотор EcoSilence Drive, чрезвычайно тихий и долговечный. За цикл работы он расходует не больше 43 л воды. С учетом того, что стиральная машина относится к классу энергоэффективности А, это указывает на ее экономичность.',
6, 4, 12, 3, 'Data/Images/Products/WashingMachines/WLP20266OE.jpg', 43, 15, 6.5, '20-95', 1, 1200);
INSERT INTO Purchases	(PurchaseID, ClientID)
	VALUES	(0, 0);
GO

INSERT INTO Products	(ProductID, Model, Category, Price, Description, Manufacturer,
						Color, Warranty, Image, InternalVolume, Grill)
	VALUES
(16, 'MS-70', 4, 4499,
'Благодаря черному корпусу с лаконичным дизайном микроволновая печь DEXP MS-70 отлично впишется в кухню с любым интерьером. Камера вмещает 20 л и дополняется удобным поворотным столом со стеклянным блюдом диаметром 25.5 см. С эмалированных стенок легко удаляются засохшие частички пищи. Для простоты ухода перед очисткой камеры можно поставить на 1-2 минуты стакан с водой и лимонной кислотой.',
4, 1, 12, 'Data/Images/Products/MicroWave/MS-70.jpg', 20, 0),
(17, 'MS23J5133AT/BW', 4, 14999,
'Микроволновая печь Samsung MS23J5133AT/BW серебристого цвета является удобным и функциональным устройством, с помощью которого можно не только быстро разогреть готовую еду, но и приготовить некоторые блюда. Для этого она обладает довольно высокой мощностью (800 Вт) и оптимальным внутренним объемом (23 л). Диаметр вращающегося поддона составляет 28.8 см, что позволит устанавливать на него несколько маленьких или одну большую тарелку с пищей. Внутреннее покрытие из биокерамической эмали облегчает уход за микроволновкой.',
1, 2, 12, 'Data/Images/Products/MicroWave/MS23J5133ATBW.jpg', 23, 0),
(18, 'MO17E1S', 4, 6499,
'Микроволновая печь Gorenje MO17E1S в стильном черно-серебристом корпусе обладает мощностью 700 Вт. Функционал устройства позволяет размораживать и разогревать пищу. Камера модели обладает стандартным объемом (17 л) и дополнена поворотной тарелкой диаметром 24.5 см. За счет покрытия из эмалированной стали печь изнутри легко очищается. Для простоты управления модель оснащена таймером на 35 минут и поворотными механизмами. Навесная дверца микроволновой печи Gorenje MO17E1S открывается посредством нажатия кнопки.',
7, 2, 24, 'Data/Images/Products/MicroWave/MO17E1S.jpg', 17, 0),
(19, 'ME81MRTS', 4, 9999,
'Микроволновая печь Samsung ME81MRTS с системой равномерного распределения микроволн не только украсит ваш дом, но и сэкономит ваше время! Мощность микроволн у этой модели – 800 Вт. Она проста в эксплуатации, так как имеет механическое управление. Биокерамическая эмаль позволит забыть о таких неприятностях, как царапина, бактерия, грязь. Объем камеры изнутри в микроволновой печи Samsung ME81MRTS составит 23 литра, а диаметр поддона 288 мм позволит вам готовить в ней пиццу и разогревать большие порции блюд.',
1, 2, 12, 'Data/Images/Products/MicroWave/ME81MRTS.jpg', 23, 0);
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