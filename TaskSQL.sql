Select @@VERSION as SQLServerVersion; 

Microsoft SQL Server 2012 - 11.0.5058.0 (X64) 
	May 14 2014 18:34:29 
	Copyright (c) Microsoft Corporation
	Enterprise Edition (64-bit) on Windows NT 6.1 <X64> (Build 7601: Service Pack 1) (Hypervisor)



Задание 1.
Решение
CREATE TABLE [dbo].[Task1](
	[Field1] [int] NULL,
	[Field2] [int] NULL,
	[Field3] [int] NULL,
	[Field4] [int] NULL,
	[Field5] [int] NULL,
	[Field6] [int] NULL,
	[Field7] [int] NULL
) ON [PRIMARY]

INSERT INTO [dbo].[Task1]
           ([Field1]
           ,[Field2]
           ,[Field3]
           ,[Field4]
           ,[Field5]
           ,[Field6]
           ,[Field7])
     VALUES
           (10,
			20,
			30,
			60,
			50,
			60,
			70)
SELECT [Field1]
      ,[Field2]
      ,[Field3]
      ,[Field4]
      ,[Field5]
      ,[Field6]
      ,[Field7]
  FROM [dbo].[Task1]
  WHERE Field1 >= 0 or
	 Field2 >= Field3 or
	 Field3 < Field5 or
	 Field4 = Field6 or
	 Field7 > 80 or
	 Field6 < Field7 or
	 Field7 > Field2

Задание 2
SQL query for Impala.
CREATE VIEW IF NOT EXISTS Product_Info_view AS
SELECT id_item
      ,item_name
      ,item_price
      ,timestamp
      ,deleted
  FROM product one
  where deleted = '0' and timestamp = (select max(timestamp) from product two where one.id_item=two.id_item)

Rezult
id_item 	item_name	item_price	timestamp	deleted
2		strong shield	30		1454292060		0
3		helmet		10		1454385660		0


Задание 3
CREATE TABLE [dbo].[factSales](
	[Date] [date] NULL,
	[ProductID] [int] NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[factRateSale](
	[Date] [date] NULL,
	[ProductID] [int] NULL,
	[Rate] [float] NULL
) ON [PRIMARY]


INSERT INTO [dbo].[factSales]
           ([Date]
           ,[ProductID]
           ,[Price]
           ,[Quantity])
     VALUES ('2012.06.10', 1, 10, 100), 
			('2012.06.27', 2, 30, 200), 
			('2012.06.29', 2, 30, 300), 
			('2012.06.30', 1, 10, 100),
			('2012.07.15', 1, 20, 200), 
			('2012.07.16', 2, 20, 300), 
			('2012.08.01', 1, 15, 100), 
			('2012.08.10', 2, 15, 200);



INSERT INTO [dbo].[factRateSale]
           ([Date]
           ,[ProductID]
           ,[Rate])
VALUES  ('2012.05.01', 1, 0.1), 
		('2012.05.01', 2, 0.2), 
		('2012.06.15', 1, 0.3), 
		('2012.06.27', 1, 0.2),
		('2012.07.12', 1, 0.2), 
		('2012.07.12', 2, 0.6);



SELECT
	T1.ProductID,
  SUM(T2.Price * T2.Quantity * T1.Rate) AS Profit
FROM [dbo].[factRateSale] T1
  JOIN [dbo].[factSales]  T2 ON T2.ProductID = T1.ProductID
WHERE (T2.Date BETWEEN '2012-06-01' AND '2012-06-30') AND (T1.Date = (SELECT
                                                                           max(T1.Date)
                                                                         FROM [dbo].[factRateSale] T1
                                                                         WHERE T1.ProductID = T2.ProductID AND
T1.Date < T2.Date))
GROUP BY T1.ProductID

SELECT
  MONTH(T2.Date) as ProfitMonth,
  SUM(T2.Price * T2.Quantity * T1.Rate) AS Profit
FROM [dbo].[factRateSale] T1
  JOIN [dbo].[factSales] T2 ON T2.ProductID = T1.ProductID
WHERE (T1.Date = (SELECT
                       max(T1.Date)
                     FROM [dbo].[factRateSale] T1
                     WHERE T1.ProductID = T2.ProductID AND T1.Date < T2.Date))
GROUP BY MONTH(T2.Date), T2.ProductID
HAVING SUM(Quantity) = 200;

