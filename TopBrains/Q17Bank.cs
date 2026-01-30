using System;

public class Solution
{
    public static int GetFinalBalance(int initialBalance, int[] transactions)
    {
        int balance = initialBalance;

        foreach (int txn in transactions)
        {
            if (txn >= 0)
            {
                balance += txn;
            }
            else
            {
                if (balance + txn >= 0)
                {
                    balance += txn;
                }
            }
        }

        return balance;
    }
}