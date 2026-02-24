using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<string> p = new List<string>();
        p.Add("Pen");
        p.Add("Book");
        p.Add("Pen");
        p.Add("Pencil");
        p.Add("Book");
        
        Console.WriteLine("All Products:");
        foreach (string product in p)
        {
            Console.WriteLine(product);
        }

        List<string> duplicates = new List<string>();

        for (int i = 0; i < p.Count; i++)
        {
            int count = 0;
            for (int j = 0; j < p.Count; j++)
            {
                if (p[i] == p[j])
                {
                    count++;
                }
            }

            if (count > 1)
            {
                bool alreadyAdded = false;
                for (int k = 0; k < duplicates.Count; k++)
                {
                    if (duplicates[k] == p[i])
                    {
                        alreadyAdded = true;
                        break;
                    }
                }

                if (!alreadyAdded)
                {
                    duplicates.Add(p[i]);
                }
            }
        }
        Console.WriteLine("Duplicate Products:");
        foreach (string duplicate in duplicates)
        {
            Console.WriteLine(duplicate);
        }

        Console.ReadLine();
    }
}