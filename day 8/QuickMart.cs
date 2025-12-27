class SaleTransaction
{
    public string InvoiceNo;
    public string CustomerName;
    public string ItemName;
    public int Quantity;
    public double PurchaseAmount;
    public double SellingAmount;
    public string ProfitOrLossStatus;
    public double ProfitOrLossAmount;
    public double ProfitMarginPercent;
    public void DisplayTransaction()
    {
        Console.WriteLine("==== Last Transaction ====");
        Console.WriteLine($"InvoiceNo :  {InvoiceNo}");
        Console.WriteLine($"Customer : {CustomerName}");
        Console.WriteLine($"Item : {ItemName}");
        Console.WriteLine($"Quantity : {Quantity}");
        Console.WriteLine($"Purchase Amount : {PurchaseAmount}");
        Console.WriteLine($"Selling Amount : {SellingAmount}");
        Console.WriteLine($"Status : {ProfitOrLossStatus}");
        Console.WriteLine($"Profit/Loss Amount :  {ProfitOrLossAmount}");
        Console.WriteLine($"Profit Margin : {ProfitMarginPercent}"+"%");
    }
    public void CalculateProfitLoss()
    {
        if (SellingAmount > PurchaseAmount)
        {
            ProfitOrLossStatus = "PROFIT";
            ProfitOrLossAmount = SellingAmount - PurchaseAmount;
        }
        else if (SellingAmount < PurchaseAmount)
        {
            ProfitOrLossStatus = "LOSS";
            ProfitOrLossAmount = PurchaseAmount - SellingAmount;
        }
        else
        {
            ProfitOrLossStatus = "BREAK-EVEN";
            ProfitOrLossAmount = 0;
        }

        ProfitMarginPercent = (ProfitOrLossAmount / PurchaseAmount) * 100;
    }
}

class QuickMartTransaction
{
    static SaleTransaction LastTransaction;
    static bool HasLastTransaction = false;

    public static void CreateNewTransaction()
    {
        Console.WriteLine("Create New Transaction");
        SaleTransaction tr = new SaleTransaction();
        Console.Write("Enter Invoice No: ");
        tr.InvoiceNo = Console.ReadLine();
        if (tr.InvoiceNo == "" || tr.InvoiceNo == null)
        {
            Console.WriteLine("Invalid Invoice No");
            return;
        }

        Console.Write("Enter Customer Name : ");
        tr.CustomerName = Console.ReadLine();

        Console.Write("Enter Item Name : ");
        tr.ItemName = Console.ReadLine();

        Console.Write("Enter Quantity : ");
        string quantityInput = Console.ReadLine();
        if (!int.TryParse(quantityInput, out tr.Quantity) || tr.Quantity <= 0)
        {
            Console.WriteLine("Invalid Quantity");
            return;
        }

        Console.Write("Enter Purchase Amount (total) : ");
        string purchaseInput = Console.ReadLine();
        if (!double.TryParse(purchaseInput, out tr.PurchaseAmount) || tr.PurchaseAmount <= 0)
        {
            Console.WriteLine("Invalid Amount");
            return;
        }

        Console.Write("Enter Selling Amount (total) : ");
        string sellingInput = Console.ReadLine();
        if (!double.TryParse(sellingInput, out tr.SellingAmount) || tr.SellingAmount < 0)
        {
            Console.WriteLine("Invalid Amount");
            return;
        }

        tr.CalculateProfitLoss();
        LastTransaction = tr;
        HasLastTransaction = true;

        Console.WriteLine("Transaction saved successfully.");
        Console.WriteLine($"Status : {tr.ProfitOrLossStatus}");
        Console.WriteLine($"Profit/Loss Amount :  {tr.ProfitOrLossAmount}");
        Console.WriteLine($"Profit Margin (%) : {tr.ProfitMarginPercent}");
    }

    public static void ViewLastTransaction()
    {
        if (HasLastTransaction == false)
        {
            Console.WriteLine("No transaction available. Please create a new transaction");
            return;
        }

        Console.WriteLine();
        LastTransaction.DisplayTransaction();
    }

    public static void CalculateProfitLoss()
    {
        if (HasLastTransaction == false)
        {
            Console.WriteLine("Calculate Profit/Loss");
            Console.WriteLine("No transaction available. Please create a new transaction");
            return;
        }

        Console.WriteLine("Calculate Profit/Loss (Recompute & Print)");
        LastTransaction.CalculateProfitLoss();
        LastTransaction.DisplayTransaction();
    }

    public static void ShowMenu()
    {
        Console.WriteLine("===== QuickMart Traders =====");
        Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
        Console.WriteLine("2. View Last Transaction");
        Console.WriteLine("3. Calculate Profit Loss");
        Console.WriteLine("4. Exit");
        Console.Write("Enter your option : ");
    }
}