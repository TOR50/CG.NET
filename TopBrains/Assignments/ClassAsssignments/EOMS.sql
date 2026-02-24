CREATE DATABASE CompanyDB;

USE CompanyDB;

CREATE TABLE Employees (
    EmpId INT PRIMARY KEY,
    Name VARCHAR(50),
    Department VARCHAR(50),
    Phone VARCHAR(15),
    Email VARCHAR(50)
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    EmpId INT FOREIGN KEY REFERENCES Employees(EmpId),
    OrderAmount DECIMAL(10,2),
    OrderDate DATE
);

INSERT INTO Employees VALUES 
(1, 'Rauhan', 'Sales', '6685866664', 'rauhan@gmail.com'),
(2, 'Shivansh', 'IT', '9564865266', 'shivansh@gmail.com'),
(3, 'Mohit', 'Sales', '9986545564', 'deepak@gmail.com'),
(4, 'Deepak', 'HR', '6841651235', 'deepak@gmail.com'),
(5, 'Arjun', 'IT', '9986545564', 'arjun@gmail.com');

INSERT INTO Orders VALUES 
(101, 1, 500.00, '2023-10-01'),
(102, 3, 750.50, '2023-10-02'),
(103, 1, 200.00, '2023-10-05');
GO


-- Part 1: Department Employee Lookup
CREATE PROCEDURE sp_GetEmployeesByDepartment
    @Department VARCHAR(50)
AS
BEGIN
    SELECT * FROM Employees WHERE Department = @Department;
END;
GO

-- Part 2: Department Strength Count (OUTPUT Parameter)
CREATE PROCEDURE sp_GetDepartmentEmployeeCount
    @Department VARCHAR(50),
    @TotalEmployees INT OUTPUT
AS
BEGIN
    SELECT @TotalEmployees = COUNT(*) 
    FROM Employees 
    WHERE Department = @Department;
END;
GO

-- Part 3: Employee Order Report (INNER JOIN)
CREATE PROCEDURE sp_GetEmployeeOrders
AS
BEGIN
    SELECT e.Name, e.Department, o.OrderId, o.OrderAmount, o.OrderDate
    FROM Employees e
    INNER JOIN Orders o ON e.EmpId = o.EmpId;
END;
GO

-- Part 4: Duplicate Employees (We find phones OR emails that appear more than once)
CREATE PROCEDURE sp_GetDuplicateEmployees
AS
BEGIN
    SELECT * FROM Employees 
    WHERE Phone IN (SELECT Phone FROM Employees GROUP BY Phone HAVING COUNT(Phone) > 1)
       OR Email IN (SELECT Email FROM Employees GROUP BY Email HAVING COUNT(Email) > 1);
END;
GO