--- CASE is for no WHERE condition
SELECT ProductID, 
       ProductName,       
       CASE
           WHEN Discontinued = 1
           THEN 'Yes'
           ELSE 'No'
       END AS Discontinued
FROM dbo.Products
--- Get active products
WHERE dbo.Products.Discontinued = 0;