using System;
using System.Collections;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter Number of Products : ");
        int n = Convert.ToInt32(Console.ReadLine());
        int[] price = new int[n];
        int sumPrice = 0;

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter price for Element {i} : ");
            int priceInput = Convert.ToInt32(Console.ReadLine());
            
            if (priceInput <= 0)
            {
                Console.WriteLine("Only Positive Price is Accepted");
                i--;
            }
            else
            {
                price[i] = priceInput;
                sumPrice += priceInput;
            }
        }

        int avgPrice = sumPrice / n;
        Array.Sort(price);

        for (int i = 0; i < n; i++)
        {
            if (price[i] > avgPrice)
            {
                price[i] = 0;
            }
        }

        int oldSize = price.Length;
        Array.Resize(ref price, oldSize + 5);

        for (int i = oldSize; i < price.Length; i++)
        {
            price[i] = avgPrice;
        }

        Console.WriteLine("Final Array Elements : ");
        for (int i = 0; i < price.Length; i++)
        {
            Console.WriteLine($"Index {i} : {price[i]}");
        }

        Console.WriteLine("Task 2");
        Console.WriteLine("Enter Number Of Branches : ");
        int branch = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Number Of Months : ");
        int months = Convert.ToInt32(Console.ReadLine());

        int[,]  branchMonthData = new int[branch , months];
        for(int i = 0 ; i < branch ; i++)
        {
            Console.WriteLine($"Entering data for Branch {i + 1} :");
            for(int j = 0 ; j< months ; j++)
            {
                Console.Write($"  Month {j + 1} Sales : ");
                branchMonthData[i, j] = Convert.ToInt32(Console.ReadLine());
            }
        }

        int[] branchTotals = new int[branch];
        int highestMonthSale = int.MinValue;

        for (int i = 0; i < branch; i++)
        {
            int sum = 0;
            for (int j = 0; j < months; j++)
            {
                sum += branchMonthData[i, j];
                if (branchMonthData[i, j] > highestMonthSale)
                {
                    highestMonthSale = branchMonthData[i, j];
                }
            }
            branchTotals[i] = sum;
        }

        Console.WriteLine("Sales Summary");
        for (int i = 0; i < branch; i++)
        {
            Console.WriteLine($"Total Sales for Branch {i + 1} : {branchTotals[i]}");
        }

        Console.WriteLine($"Highest Monthly Sale  : {highestMonthSale}");

        Console.WriteLine("");

        int[][] jaggedSales = new int[branch][];

        for (int i = 0; i < branch; i++)
        {
            int count = 0;
            for (int j = 0; j < months; j++)
            {
                if (branchMonthData[i, j] >= avgPrice) count++;
            }

            jaggedSales[i] = new int[count];
            
            int index = 0;
            for (int j = 0; j < months; j++)
            {
                if (branchMonthData[i, j] >= avgPrice)
                {
                    jaggedSales[i][index] = branchMonthData[i, j];
                    index++;
                }
            }
        }
        for (int i = 0; i < jaggedSales.Length; i++)
        {
            Console.WriteLine($"Branch {i + 1} Qualifying Sales : {string.Join(", ", jaggedSales[i])}");
        }

        Console.WriteLine("Duplicate Removal");
        Console.Write("Enter number of customer transactions: ");
        int transCount = Convert.ToInt32(Console.ReadLine());
        List<int> customerIds = new List<int>();

        for (int i = 0; i < transCount; i++)
        {
            Console.Write($"Enter Customer ID for transaction {i + 1}: ");
            customerIds.Add(Convert.ToInt32(Console.ReadLine()));
        }

        HashSet<int> uniqueIds = new HashSet<int>(customerIds);
        List<int> cleanedList = uniqueIds.ToList();

        Console.WriteLine("Customer List : " + string.Join(", ", cleanedList));
        Console.WriteLine($"Duplicates Removed : {customerIds.Count - cleanedList.Count}");

        Console.WriteLine("High Value Transactions");
        Console.Write("Enter number of transactions: ");
        int dictCount = Convert.ToInt32(Console.ReadLine());
        Dictionary<int, double> transactions = new Dictionary<int, double>();

        for (int i = 0; i < dictCount; i++)
        {
            Console.Write("Enter Transaction ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            
            if (transactions.ContainsKey(id))
            {
                Console.WriteLine("ID exists");
                i--;
                continue;
            }

            Console.Write("Enter Amount : ");
            double amount = Convert.ToDouble(Console.ReadLine());
            transactions.Add(id, amount);
        }

        SortedList<int, double> highValueSales = new SortedList<int, double>();
        foreach (var entry in transactions)
        {
            if (entry.Value >= avgPrice)
            {
                highValueSales.Add(entry.Key, entry.Value);
            }
        }

        Console.WriteLine($"Sorted High Value Transactions for Amount >= {avgPrice} : ");
        foreach (var kvp in highValueSales)
        {
            Console.WriteLine($"ID: {kvp.Key} | Amount: {kvp.Value}");
        }
        Console.WriteLine("Task 6");
        Console.Write("Enter number of operations: ");
        int operationCount = Convert.ToInt32(Console.ReadLine());

        Queue<string> operationQueue = new Queue<string>();
        Stack<string> operationStack = new Stack<string>();

        for (int i = 0; i < operationCount; i++)
        {
            Console.Write($"Enter operation {i + 1}: ");
            string operation = Console.ReadLine();
            operationQueue.Enqueue(operation);
            operationStack.Push(operation);
        }

        Console.WriteLine("Processed Operations FIFO : ");
        int pcount = 0;
        while (operationQueue.Count > 0)
        {
            string pcomplete = operationQueue.Dequeue();
            Console.WriteLine($"  {++pcount}. {pcomplete}");
        }

        Console.WriteLine("Undo Last Two Operation : ");
        int undoCount = 0;
        while (operationStack.Count > 0 && undoCount < 2)
        {
            string undoneOp = operationStack.Pop();
            Console.WriteLine($"  Undo : {undoneOp}");
            undoCount++;
        }

        Console.WriteLine("\nRemaining Operations in Stack:");
        if (operationStack.Count > 0)
        {
            foreach (var op in operationStack)
            {
                Console.WriteLine($"{op}");
            }
        }
        else
        {
            Console.WriteLine("No operation remaining.");
        }

        Console.WriteLine("Task 7");
        Console.Write("Enter number of users : ");
        int userCount = Convert.ToInt32(Console.ReadLine());

        Hashtable userRoles = new Hashtable();
        ArrayList userDataList = new ArrayList();

        for (int i = 0; i < userCount; i++)
        {
            Console.Write($"Enter username {i + 1}: ");
            string username = Console.ReadLine();
            Console.Write($"Enter role for {username}: ");
            string role = Console.ReadLine();

            userRoles.Add(username, role);
            userDataList.Add(username);
            userDataList.Add(role);
        }

        Console.WriteLine("Hashtable :");
        foreach (DictionaryEntry entry in userRoles)
        {
            Console.WriteLine($"  Username: {entry.Key} Role: {entry.Value}");
        }

        Console.WriteLine("ArrayList : ");
        for (int i = 0; i < userDataList.Count; i++)
        {
            Console.WriteLine($"  Index {i}: {userDataList[i]} (Type: {userDataList[i].GetType().Name})");
        }

    }
}