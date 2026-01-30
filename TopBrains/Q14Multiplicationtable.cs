using System;

public class TableGenerator
{
    public static void Main(string[] args)
    {
        int n = 3;
        int upto = 5;
        int[] result = GetMultiplicationRow(n, upto);

        Console.WriteLine("[" + string.Join(", ", result) + "]");
    }

    public static int[] GetMultiplicationRow(int n, int upto)
    {
        if (upto <= 0)
        {
            return new int[0];
        }
        int[] row = new int[upto];

        for (int i = 1; i <= upto; i++)
        {
            row[i - 1] = n * i;
        }

        return row;
    }
}
