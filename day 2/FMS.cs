using System;
class Fmsclass
{
    public static void Fms()
    {  
        int Option = 0;
        int TotalWithdrawal = 0;
        while(Option != 3)
        { 
        Console.WriteLine("Main Menu");
        Console.WriteLine("1. Debit Operations");
        Console.WriteLine("2. Credit Operations");
        Console.WriteLine("3. Exit");
        Console.WriteLine("Enter Your Option : ");
        Option = Convert.ToInt32(Console.ReadLine());
        if(Option>0 && Option<4)
            {
                switch(Option)
                {
                    case 1:
                    Console.WriteLine("Credit Menu");
                    Console.WriteLine("1: ATM Withdrawal Limit Validation");
                    Console.WriteLine("2: EMI Burden Evaluation");
                    Console.WriteLine("3: Transaction-Based Daily Spending Calculator");
                    Console.WriteLine("4: Minimum Balance Compliance Check");
                    int tchoice = Convert.ToInt32(Console.ReadLine());
                        if(tchoice == 1)
                        {   
                            Console.WriteLine("Enter ATM Withdrawal amount : ");
                            int amount = Convert.ToInt32(Console.ReadLine());
                            TotalWithdrawal = TotalWithdrawal + amount;
                            Credit c = new Credit();
                            bool a = c.AtmLimit(amount);
                            if(a == true && TotalWithdrawal <= 40000)
                            {
                                Console.WriteLine("Withdrawal permitted within daily limit.");
                            }
                            else
                            {
                                Console.WriteLine("Daily ATM withdrawal limit exceeded.");
                            }
                        }
                        else if(tchoice == 2)
                        {   
                            Console.WriteLine("Enter Monthly Income : ");
                            int mincome = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter EMI amount : ");
                            int emiamount = Convert.ToInt32(Console.ReadLine());
                            Credit c = new Credit();
                            bool b = c.Emi(mincome , emiamount);
                            if(b == true )
                            {
                                Console.WriteLine("EMI is financially manageable.");
                            }
                            else
                            {
                                Console.WriteLine("EMI exceeds safe income limit.");
                            }
                        }
                        else if(tchoice == 3)
                        {
                            Console.WriteLine("Enter Number of transation : ");
                            int i = Convert.ToInt32(Console.ReadLine());
                            Credit c = new Credit();
                            int x = c.Spending(i);
                            Console.WriteLine($"Total debit amount for the day : {x}");

                        }
                        else if(tchoice == 4)
                        {
                            Console.WriteLine("Enter Current Balance : ");
                            int i = Convert.ToInt32(Console.ReadLine());
                            Credit c = new Credit();
                            bool m = c.MinBalance(i);
                            if(m == true)
                            {
                                Console.WriteLine("Minimum balance not maintained. Penalty applicable.");
                            }
                            else
                            {
                                 Console.WriteLine("Minimum balance requirement satisfied.");
                            }

                        }

                    break;

                    case 2:
                    Console.WriteLine("Debit Menu");
                    Console.WriteLine("1: Net Salary Credit Calculation");
                    Console.WriteLine("2: Fixed Deposit Maturity Calculation");
                    Console.WriteLine("3: Credit Card Reward Points Evaluation");
                    Console.WriteLine("4: Employee Bonus Eligibility Check");
                    
                    int dchoice = Convert.ToInt32(Console.ReadLine());
                        if(dchoice == 1)
                        {
                            Console.WriteLine("Enter Net Salary : ");
                            float netsal = Convert.ToSingle(Console.ReadLine());
                            Debit d = new Debit();
                            float nsal = d.NetSal(netsal);
                            Console.WriteLine($"Net salary credited : {nsal}");

                        }
                        else if(dchoice == 2)
                        {
                            Console.WriteLine("Enter Principal Amount : ");
                            float p = Convert.ToSingle(Console.ReadLine());
                            Console.WriteLine("Enter Rate of Intrest : ");
                            float r = Convert.ToSingle(Console.ReadLine());
                            Console.WriteLine("Enter TIme Period (in months) : ");
                            float t = Convert.ToSingle(Console.ReadLine());
                            Debit d = new Debit();
                            float maturity = d.MaturityAmount(p , r , t);
                            Console.WriteLine($"Fixed Deposit maturity amount : {maturity}");
                            
                        }
                        else if(dchoice == 3)
                        {
                            Console.WriteLine("Enter Credit card spending : ");
                            float sp = Convert.ToSingle(Console.ReadLine());
                            Debit d = new Debit();
                            float reward = d.RewardPoint(sp);
                            Console.WriteLine($"Reward points earned : {reward}");
                            

                        }
                        else if(dchoice == 4)
                        {
                            Console.WriteLine("Enter Annual salary  : ");
                            float anualsal = Convert.ToSingle(Console.ReadLine());
                            Console.WriteLine("Enter Years of service  : ");
                            float years = Convert.ToSingle(Console.ReadLine());
                            Debit d = new Debit();
                            bool a = d.BonusCheck(anualsal , years);
                            if (a == true)
                            {
                                Console.WriteLine("Employee is eligible for bonus.");
                            }
                            else
                            {
                                Console.WriteLine("Employee is not eligible for bonus.");
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