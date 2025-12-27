using System;
using System.Collections.Generic;

public struct PriceSnapshot
{
    public string Symbol;
    public double Price;
}

public abstract class Trade
{
    public int TradeId;
    public string Symbol;
    public int Quantity;

    public abstract double CalculateValue();

    public override string ToString()
    {
        return $"TradeId: {TradeId} Symbol: {Symbol}, Qty: {Quantity}";
    }
}

public class EquityTrade : Trade
{
    public double? MarketPrice { get; set; }

    public override double CalculateValue()
    {
        double price = MarketPrice ?? 0;
        return price * Quantity;
    }
}

public class TradeRepository<T> where T : Trade
{
    private List<T> trades = new List<T>();

    public void AddTrade(T trade)
    {
        trades.Add(trade);
        TradeAnalytics.TotalTrades++;
        Console.WriteLine("Trade added successfully");
    }

    public List<T> GetAll()
    {
        return trades;
    }
}

public static class TradeAnalytics
{
    public static int TotalTrades = 0;

    public static void DisplayAnalytics()
    {
        Console.WriteLine($"Total Trades Executed : {TotalTrades}");
    }
}

public class Program
{
    public static void Main()
    {
        PriceSnapshot snapshot;
        snapshot.Symbol = "AAPL";
        snapshot.Price = 150.50;
        Console.WriteLine($"Stock Symbol : {snapshot.Symbol}");
        Console.WriteLine($"Stock Price : {snapshot.Price}");
        
        TradeRepository<EquityTrade> repo = new TradeRepository<EquityTrade>();

        EquityTrade trade1 = new EquityTrade();
        trade1.TradeId = 1;
        trade1.Symbol = "AAPL";
        trade1.Quantity = 100;
        trade1.MarketPrice = 150.50;

        EquityTrade trade2 = new EquityTrade();
        trade2.TradeId = 2;
        trade2.Symbol = "MSFT";
        trade2.Quantity = 50;
        trade2.MarketPrice = null;

        TradeAnalytics.DisplayAnalytics();



        
    }

    
}