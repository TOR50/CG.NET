using System;
//struct
struct StockPrice
{
    public string Symbol;
    public int Price;
}

class Trade
{
    public int TradeId;
    public int Quantity;
    public string Symbol;
}

class Program
{
    static void Main()
    {
        
        StockPrice originalPrice = new StockPrice();
        originalPrice.Symbol = "AAPL";
        originalPrice.Price = 150;

        
        StockPrice copiedPrice = originalPrice;
        
        
        copiedPrice.Price = 160;
        Trade trade1 = new Trade
        {
            TradeId = 101,
            Symbol = "AAPL",
            Quantity = 100
        };

        Trade trade2 = trade1;
        trade2.Quantity = 200;

        Console.WriteLine($"Original Stock Price: {originalPrice.Symbol} - ${originalPrice.Price}");
        
        Console.WriteLine($"Copied Stock Price: {copiedPrice.Symbol} - ${copiedPrice.Price}");
        
        Console.WriteLine($"Trade1 Details: ID={trade1.TradeId}, Symbol={trade1.Symbol}, Quantity={trade1.Quantity}");
        Console.WriteLine($"Trade2 Details: ID={trade2.TradeId}, Symbol={trade2.Symbol}, Quantity={trade2.Quantity}");

    }
}