
create table Employees (
    Id int,
    Name varchar(50),
    Dept varchar(50),
    Salary int
);

insert into Employees (Id, Name, Dept, Salary) values
(1,'Rauhan','IT',90000),
(2,'Shivansh','IT',100000),
(3,'Arun','HR',120000);



select Employees.Dept, Employees.Name, Employees.Salary
from Employees
inner join (
    select Dept, max(Salary) as MaxSalary
    from Employees
    group by Dept
) as Highest
on Employees.Dept = Highest.Dept 
and Employees.Salary = Highest.MaxSalary;