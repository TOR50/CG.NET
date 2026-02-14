using System;
using System.Collections.Generic;
using System.Linq;


using System.Threading.Tasks;

public class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public List<string> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Pending,
    PaymentProcessing,
    Cooking,
    ReadyForDelivery,
    Delivered,
    Failed
}

public class PaymentResult
{
    public bool IsSuccessful { get; set; }
    public string TransactionId { get; set; }
    public string ErrorMessage { get; set; }
}




public class KitchenStation
{
    public async Task<TimeSpan> PrepareItemAsync(string itemName)
    {
        // Simulate varying cooking times
        var random = new Random();
        int cookingTime = random.Next(1000, 5000); // 1-5 seconds
        await Task.Delay(cookingTime);

        Console.WriteLine($"Finished preparing: {itemName} in {cookingTime}ms");
        return TimeSpan.FromMilliseconds(cookingTime);
    }
}

public class PaymentGateway
{
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount)
    {
        // Simulate API call to payment provider
        await Task.Delay(2000); // Simulate network delay

        // Simulate occasional payment failures
        var random = new Random();
        bool isSuccessful = random.Next(0, 10) > 1; // 90% success rate

        return new PaymentResult
        {
            IsSuccessful = isSuccessful,
            TransactionId = isSuccessful ? Guid.NewGuid().ToString() : null,
            ErrorMessage = isSuccessful ? null : "Payment declined"
        };
    }
}

public class NotificationService
{
    public async Task SendNotificationAsync(string customerName, string message)
    {
        await Task.Delay(500); // Simulate notification delay
        Console.WriteLine($"Notification sent to {customerName}: {message}");
    }
}

public class OrderProcessor
{
    private readonly PaymentGateway _paymentGateway;
    private readonly NotificationService _notificationService;
    private readonly KitchenStation _kitchenStation;

    public OrderProcessor()
    {
        _paymentGateway = new PaymentGateway();
        _notificationService = new NotificationService();
        _kitchenStation = new KitchenStation();
    }

    // Main method to process an order asynchronously
    public async Task<Order> ProcessOrderAsync(Order order)
    {
        try
        {
            Console.WriteLine($"Starting order #{order.OrderId} for {order.CustomerName}");

            // 1. Update order status
            order.Status = OrderStatus.PaymentProcessing;
            await _notificationService.SendNotificationAsync(
                order.CustomerName,
                $"Order #{order.OrderId} received. Processing payment..."
            );

            // 2. Process payment asynchronously with timeout
            var paymentTask = _paymentGateway.ProcessPaymentAsync(order.TotalAmount);
            var timeoutTask = Task.Delay(10000); // 10 second timeout

            var completedTask = await Task.WhenAny(paymentTask, timeoutTask);

            if (completedTask == timeoutTask)
            {
                throw new TimeoutException("Payment processing timed out");
            }

            var paymentResult = await paymentTask;

            if (!paymentResult.IsSuccessful)
            {
                order.Status = OrderStatus.Failed;
                throw new Exception($"Payment failed: {paymentResult.ErrorMessage}");
            }

            // 3. Prepare items concurrently
            order.Status = OrderStatus.Cooking;
            await _notificationService.SendNotificationAsync(
                order.CustomerName,
                $"Payment successful! Cooking your order..."
            );

            var cookingTasks = order.Items
                .Select(item => _kitchenStation.PrepareItemAsync(item))
                .ToList();

            // Wait for all items to be prepared
            var cookingTimes = await Task.WhenAll(cookingTasks);
            var totalCookingTime = cookingTimes.Aggregate(TimeSpan.Zero, (sum, time) => sum + time);

            Console.WriteLine($"All items prepared in {totalCookingTime.TotalSeconds:F2} seconds");

            // 4. Mark order as ready
            order.Status = OrderStatus.ReadyForDelivery;
            await _notificationService.SendNotificationAsync(
                order.CustomerName,
                $"Order #{order.OrderId} is ready for delivery!"
            );

            return order;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing order #{order.OrderId}: {ex.Message}");
            order.Status = OrderStatus.Failed;

            await _notificationService.SendNotificationAsync(
                order.CustomerName,
                $"Sorry! Order #{order.OrderId} failed: {ex.Message}"
            );

            return order;
        }
    }

    // Advanced: Process multiple orders with controlled concurrency
    public async Task<List<Order>> ProcessBulkOrdersAsync(List<Order> orders, int maxConcurrentOrders = 3)
    {
        var allTasks = new List<Task<Order>>();
        var processingTasks = new List<Task<Order>>();

        foreach (var order in orders)
        {
            // If we've reached max concurrency, wait for one to complete
            if (processingTasks.Count >= maxConcurrentOrders)
            {
                var completedTask = await Task.WhenAny(processingTasks);
                processingTasks.Remove(completedTask);
            }

            // Start processing this order
            var task = ProcessOrderAsync(order);
            processingTasks.Add(task);
            allTasks.Add(task);
        }

        // Wait for all remaining tasks to complete
        await Task.WhenAll(allTasks);

        return allTasks.Select(t => t.Result).ToList();
    }

    // Example of async event handling
    public event Func<Order, Task> OrderStatusChanged;

    protected virtual async Task OnOrderStatusChanged(Order order)
    {
        if (OrderStatusChanged != null)
        {
            await OrderStatusChanged(order);
        }
    }
}

// Usage Example
class Program
{
    static async Task Main(string[] args)
    {
        var orderProcessor = new OrderProcessor();

        // Subscribe to order status changes
        orderProcessor.OrderStatusChanged += async (order) =>
        {
            Console.WriteLine($"Event: Order #{order.OrderId} status changed to {order.Status}");
            await Task.CompletedTask; // Async event handler
        };

        // Create sample order
        var order = new Order
        {
            OrderId = 1,
            CustomerName = "John Doe",
            Items = new List<string> { "Burger", "Fries", "Coke", "Ice Cream" },
            TotalAmount = 25.99m,
            Status = OrderStatus.Pending
        };

        Console.WriteLine("=== Processing Single Order ===");
        var result = await orderProcessor.ProcessOrderAsync(order);
        Console.WriteLine($"Final order status: {result.Status}");

        // Process multiple orders
        Console.WriteLine("\n=== Processing Multiple Orders ===");
        var orders = new List<Order>
        {
            new Order { OrderId = 2, CustomerName = "Alice", Items = {"Pizza", "Salad"}, TotalAmount = 18.50m },
            new Order { OrderId = 3, CustomerName = "Bob", Items = {"Pasta"}, TotalAmount = 12.75m },
            new Order { OrderId = 4, CustomerName = "Charlie", Items = {"Steak", "Mashed Potatoes"}, TotalAmount = 32.00m },
            new Order { OrderId = 5, CustomerName = "Diana", Items = {"Soup", "Sandwich"}, TotalAmount = 10.25m }
        };

        var results = await orderProcessor.ProcessBulkOrdersAsync(orders, maxConcurrentOrders: 2);

        Console.WriteLine("\n=== Processing Summary ===");
        foreach (var processedOrder in results)
        {
            Console.WriteLine($"Order #{processedOrder.OrderId}: {processedOrder.Status}");
        }
    }
}

