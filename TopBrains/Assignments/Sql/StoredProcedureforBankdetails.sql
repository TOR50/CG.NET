use Bank;

CREATE TABLE Customers
(
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    PhoneNumber VARCHAR(15),
    City VARCHAR(50),
    CreatedDate DATE
);

CREATE TABLE Accounts
(
    AccountID INT PRIMARY KEY,
    CustomerID INT,
    AccountNumber VARCHAR(20),
    AccountType VARCHAR(20),
    OpeningBalance DECIMAL(12,2),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE Transactions
(
    TransactionID INT PRIMARY KEY,
    AccountID INT,
    TransactionDate DATE,
    TransactionType VARCHAR(10),
    Amount DECIMAL(12,2),
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

CREATE TABLE Bonus
(
    BonusID INT PRIMARY KEY,
    AccountID INT,
    BonusMonth INT,
    BonusYear INT,
    BonusAmount DECIMAL(10,2),
    CreatedDate DATE,
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

INSERT INTO Customers VALUES
(1, 'Ravi Kumar', '9876543210', 'Chennai', '2023-01-10'),
(2, 'Priya Sharma', '9123456789', 'Bangalore', '2023-03-15'),
(3, 'John Peter', '9988776655', 'Hyderabad', '2023-06-20');

INSERT INTO Accounts VALUES
(101, 1, 'SB1001', 'Savings', 20000),
(102, 2, 'SB1002', 'Savings', 15000),
(103, 3, 'SB1003', 'Savings', 30000);

INSERT INTO Transactions VALUES
(1, 101, '2024-01-05', 'Deposit', 30000),
(2, 101, '2024-01-18', 'Withdraw', 5000),
(3, 101, '2024-02-10', 'Deposit', 25000),
(4, 102, '2024-01-07', 'Deposit', 20000),
(5, 102, '2024-01-25', 'Deposit', 35000),
(6, 102, '2024-02-05', 'Withdraw', 10000),
(7, 103, '2024-01-10', 'Deposit', 15000),
(8, 103, '2024-01-20', 'Withdraw', 5000);

-- Question 1 stored procedure

create or alter procedure Summary
    @StartDate date,
    @EndDate date,
    @AccountID int
as
begin
declare @DepositTotal decimal(12,2);
    declare @WithdrawTotal decimal(12,2);
    
    select @DepositTotal = sum(Amount) from Transactions where AccountID = @AccountID and 
    TransactionType = 'Deposit' and TransactionDate between @StartDate and @EndDate;

    select @WithdrawTotal = sum(Amount) from Transactions where AccountID = @AccountID and 
    TransactionType = 'Withdraw'and TransactionDate between @StartDate and @EndDate;

    select
        isnull(@DepositTotal, 0) as TotalDeposited, 
        isnull(@WithdrawTotal, 0) as TotalWithdrawn;
end;

--exec Summary '2024-01-05', '2024-02-05', 102;

-- Question 2 update bonus
insert into Bonus (BonusID, AccountID, BonusMonth, BonusYear, BonusAmount, CreatedDate) 
select 
    ROW_NUMBER() over (order by AccountID) + (select isnull(max(BonusID), 0) from Bonus),
    AccountID, 
    month(TransactionDate), 
    year(TransactionDate), 
    1000, 
    getdate()
from Transactions where TransactionType = 'Deposit' group by AccountID, month(TransactionDate),
year(TransactionDate) having sum(Amount) > 50000;

-- Question 3 current balance

select
    Customers.CustomerName,
    Accounts.AccountNumber,
    (
        Accounts.OpeningBalance + 
        ISNULL((select sum(Amount) from Transactions 
                where Transactions.AccountID = Accounts.AccountID and TransactionType = 'Deposit'), 0) - 
        ISNULL((select sum(Amount) from Transactions
                where Transactions.AccountID = Accounts.AccountID and TransactionType = 'Withdraw'), 0) + 
        ISNULL((select sum(BonusAmount) from Bonus 
                where Bonus.AccountID = Accounts.AccountID), 0)
    ) as CurrentBalance from Customers join Accounts on Customers.CustomerID = Accounts.CustomerID;

