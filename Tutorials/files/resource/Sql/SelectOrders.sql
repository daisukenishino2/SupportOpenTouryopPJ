SELECT
    Orders.OrderID, Customers.CompanyName, Customers.ContactName, Employees.LastName As EmployeeLastName, Employees.FirstName As EmployeeFirstName, Orders.OrderDate
FROM
    Orders
        INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID
        INNER JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID