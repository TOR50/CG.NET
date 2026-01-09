using System;
using System.Collections.Generic;

namespace EcommerceAssessment
{
    public class Repository<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> GetAll()
        {
            return items;
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public double Amount { get; set; }

        public override string ToString()
        {
            return $"OrderId: {OrderId}, Customer: {CustomerName}, Amount: {Amount}";
        }
    }

    public delegate void OrderCallback(string message);

    public class OrderProcessor
    {
        public event Action<string> OrderProcessed;

        public void ProcessOrder(Order order, Func<double, double> taxCalculator, Func<double, double> discountCalculator, Predicate<Order> validator, OrderCallback callback)
        {
            if (!validator(order))
            {
                callback("Order validation failed.");
                return;
            }

            double taxValue = taxCalculator(order.Amount);
            double discountValue = discountCalculator(order.Amount);

            order.Amount = order.Amount + taxValue - discountValue;

            callback($"Order {order.OrderId} processed successfully.");

            if (OrderProcessed != null)
            {
                OrderProcessed($"Order {order.OrderId} completed.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Repository<Order> orderRepo = new Repository<Order>();

            Order order1 = new Order { OrderId = 1, CustomerName = "Alice", Amount = 5000 };
            Order order2 = new Order { OrderId = 2, CustomerName = "Bob", Amount = 2000 };
            Order order3 = new Order { OrderId = 3, CustomerName = "Charlie", Amount = 8000 };

            orderRepo.Add(order1);
            orderRepo.Add(order2);
            orderRepo.Add(order3);

            OrderProcessor processor = new OrderProcessor();

            Func<double, double> taxLogic = (amt) => amt * 0.18;
            Func<double, double> discountLogic = (amt) => amt * 0.10;
            Predicate<Order> validateLogic = (ord) => ord.Amount > 2500;

            OrderCallback myCallback = (msg) => Console.WriteLine("Callback: " + msg);

            Action<string> logger = (msg) => Console.WriteLine("Logger: Event: " + msg);
            Action<string> notifier = (msg) => Console.WriteLine("Notifier: Event: " + msg);

            Action<string> multicastNotification = logger + notifier;
            processor.OrderProcessed += multicastNotification;

            List<Order> allOrders = orderRepo.GetAll();

            foreach (Order o in allOrders)
            {
                processor.ProcessOrder(o, taxLogic, discountLogic, validateLogic, myCallback);
                Console.WriteLine();
            }

            Comparison<Order> compareByAmount = (x, y) => y.Amount.CompareTo(x.Amount);
            allOrders.Sort(compareByAmount);

            Console.WriteLine("Sorted Orders (Descending Amount):");
            foreach (Order o in allOrders)
            {
                Console.WriteLine(o.ToString());
            }

            Console.ReadLine();
        }
    }
}