using System;
class Paras
{
    public static int Sum(params int[] n)
    {
        int total = 0;
       foreach(int  i in n)
        {
            total += i;
        }
        return total;
    }
}
