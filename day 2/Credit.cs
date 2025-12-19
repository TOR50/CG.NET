public class Credit()
    {
    public bool AtmLimit(int amount)
    {   
        if(amount<=40000)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Emi(int mincome , int emiamount)
    {
        if(emiamount < (mincome*0.40))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int Spending(int i)
    {   
        int totalAmount = 0;
        for(int j = 1 ; j <= i ; j++)
        {   
            Console.WriteLine($"Enter {j}th Transaction Withdrawal Amount : ");
            int m = Convert.ToInt32(Console.ReadLine());
            totalAmount += m;

        }
        return totalAmount;
    }

    public bool MinBalance(int current_balance)
    {
        if(current_balance < 2000)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}