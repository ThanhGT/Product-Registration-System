MERGE INTO Product AS Target 
USING (VALUES 
        (1, 'ROG Zephyrus GX501', 179.00, 101), 
        (2, 'iWALK Ultra Slim Power Bank', 124.24, 101), 
        (3, 'Campfire Audio Andromeda', 1253.50, 201),
        (4, 'Fiio M11', 629.00, 201),
        (5, 'ASUS ROG 5 Ultimate', 2789.99, 101)
) 
AS Source (ProductID, ProductName, Price, CategoryID) 
ON Target.ProductID = Source.ProductID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (ProductName, Price, CategoryID) 
VALUES (ProductName, Price, CategoryID);

MERGE INTO Category AS Target
USING (VALUES 
        (101, 'Mobile'), 
        (201, 'Audio')
)
AS Source (CategoryID, CategoryName)
ON Target.CategoryID = Source.CategoryID
WHEN NOT MATCHED BY TARGET THEN
INSERT (CategoryName)
VALUES (CategoryName);