using System;
class Finance
{
    public static void Fin()
    {   
        int Option = 0;
        while(Option != 4)
        {   
        Console.WriteLine("Main Menu");
        Console.WriteLine("1. Check Loan Eligibility");
        Console.WriteLine("2. Calculate Tax");
        Console.WriteLine("3. Enter Transactions");
        Console.WriteLine("4. Exit");
        Console.WriteLine("Enter Your Option : ");
        Option = Convert.ToInt32(Console.ReadLine());
        if(Option>0 && Option<5)
            {
                switch(Option)
                {
                    case 1:
                    int age;
                    float income = 0.0f;
                    Console.WriteLine("Enter Age : ");
                    age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Income : ");
                    income = Convert.ToSingle(Console.ReadLine());

                    if(age >= 21 && income >= 30000)
                        {
                            Console.WriteLine("Yes , Eligible For Loan.");
                        }
                    else
                        {
                            Console.WriteLine("No , Not Eligible For Loan.");
                        }

                    break;

                    case 2:
                    float annual_income;
                    Console.WriteLine("Enter Annual Income : ");
                    annual_income = Convert.ToSingle(Console.ReadLine());
                    if(annual_income <= 250000)
                        {
                            Console.WriteLine($"Your income tax is {annual_income * 0}");
                        }
                    else if(annual_income >= 250001 && annual_income <= 500000)
                        {
                            Console.WriteLine($"Your income tax is {annual_income * 0.05}");
                        }
                    else if(annual_income >= 500001 && annual_income <= 1000000)
                        {
                            Console.WriteLine($"Your income tax is {annual_income * 0.2}");
                        }
                    else if(annual_income < 1000000)
                        {
                            Console.WriteLine($"Your income tax is {annual_income * 0.3}");
                        }
                    break;
                    
                    case 3:
                    Console.WriteLine("Transaction Entry System");
                    int transactions = 0;
                    int balance = 0;
                    while(transactions < 6)
                        {
                            Console.WriteLine($"Your current balance is {balance}");
                            Console.WriteLine("1. Deposit");
                            Console.WriteLine("2. Withdraw");
                            Console.WriteLine("Enter Choice : ");
                            int tchoice = Convert.ToInt32(Console.ReadLine());
                            if(tchoice == 1)
                                {
                                    Console.WriteLine("Enter amount to deposit : ");
                                    int damount = Convert.ToInt32(Console.ReadLine());
                                    if(damount>0){
                                    balance = balance + damount;
                                    Console.WriteLine($"Your current balance is{balance}");
                                    Console.WriteLine($"Number of Transactions completed{transactions}");
                                    
                                    transactions++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Amount");
                                    }
                                }
                            if(tchoice == 2)
                                {
                                    Console.WriteLine("Enter amount to withdraw : ");
                                    int wamount = Convert.ToInt32(Console.ReadLine());
                                    if(wamount > 0){
                                    if (balance - wamount > 0)
                                    {
                                        balance = balance - wamount;
                                        Console.WriteLine($"Your current balance is{balance}");
                                        Console.WriteLine($"Number of Transactions completed{transactions}");
                                        transactions++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Amount");
                                    }
                                }
                            }
                        }
                    break;
                }
            }
        else
        {
            Console.WriteLine("Invalid Input");
        }
    }
    }
}