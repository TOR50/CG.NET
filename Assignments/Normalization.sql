--Question 1 � Normalization (Design Thinking)

use Sales;

create table CustomerMaster(
CustomerId int identity(1,1) primary key,
CustomerName varchar(50) Not null,
CustomerPhone varchar(20) unique,
CustomerCity varchar(50)
);
insert into CustomerMaster(CustomerName, CustomerPhone, CustomerCity)
values 
('Rauhan','9687455655','kanpur'),
('Deepak','8794561526','Etawah'),
('Nityam','9876445656','Jammu')
;

create table SalesMaster(
SalesPersonID int identity(1,1) primary key,
SalesPersonName varchar(100) not null
); 
insert into SalesMaster(SalesPersonName)
values
('Anitha'),
('Suresh')
;

create table ProductMaster(
ProductId int identity(1,1) primary key,
ProductName varchar(50) Not null unique,
UnitPrice Decimal(10,2) not null
);
Insert into ProductMaster(ProductName,UnitPrice)
values
('Laptop',55000),
('Mouse',600),
('KeyBoard',1000),
('Monitor',3000)
;

create table CustomerOrders(
OrderId int primary key,
OrderDate date,
CustomerId int,
SalesPersonID int,
foreign key(CustomerId) references CustomerMaster(CustomerId),
foreign key(SalesPersonID) references SalesMaster(SalesPersonID)
);
insert into CustomerOrders(OrderId, OrderDate, CustomerID, SalesPersonID)
values
(101, '2024-01-05', 1, 1),
(102, '2024-01-06', 2, 1),
(103, '2024-01-10', 1, 2),
(104, '2024-02-01', 3, 1),
(105, '2024-02-10', 2, 2);

create table OrderDetails(
DetailId int identity(1,1) primary key,
OrderId int,
ProductId int,
Quantity int not null,

foreign key(OrderId) references CustomerOrders(OrderId),
foreign key(ProductId) references ProductMaster(ProductId )
);

insert into OrderDetails(OrderId,ProductId,Quantity)
values
(101,1,1),
(101,2,2),
(102,3,1),
(102,2,1),
(103,1,1),
(104,4,1),
(104,2,1),
(105,1,1),
(105,3,1);

--Question2�Third Highest Total Sales(Analytical Query)
with Ordertotal as(
select co.OrderId,sum(od.Quantity*p.UnitPrice) as TotalSales from CustomerOrders co
join OrderDetails od on co.OrderId=od.OrderId
join ProductMaster p on p.ProductId=od.ProductId
group by co.OrderId
)
select OrderId,TotalSales from Ordertotal 
order by TotalSales desc offset 2 rows fetch next 1 row only;

--Question3�GROUP BY & HAVING(Business Rule)
select s.SalesPersonName,sum(od.Quantity*p.UnitPrice) as TotalSales
from CustomerOrders co
join SalesMaster s on s.SalesPersonId=co.SalesPersonID
join OrderDetails od on co.OrderId=od.OrderId
join ProductMaster p on od.ProductId=p.ProductId
group by s.SalesPersonName
having sum(od.Quantity*p.UnitPrice) >60000;

--Question4�Subquery Usage

with CustomerTotal as(
select cm.CustomerName,sum(od.Quantity*p.UnitPrice) as TotalSpent from CustomerMaster cm
join CustomerOrders co on cm.CustomerId=co.CustomerId
join OrderDetails od on co.OrderId=od.OrderId
join ProductMaster p on p.ProductId=od.ProductId
group by cm.CustomerName
)
select CustomerName,TotalSpent from CustomerTotal 
where TotalSpent>(select avg(TotalSpent) from CustomerTotal);


--Question5�String & Date Functions

Select upper(cm.CustomerName) as Name_upper,
datename(month,co.OrderDate) as Order_Month,
co.OrderId,co.OrderDate from CustomerOrders co
join CustomerMaster cm on co.CustomerId=cm.CustomerId
where month(co.OrderDate)=1 and year(co.OrderDate)=2024;