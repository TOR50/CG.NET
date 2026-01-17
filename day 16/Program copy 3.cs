using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Thread thread = new Thread(new ParameterizedThreadStart(PrintMessage));
        // thread.Start("Hello from thread");
        // static void PrintMessage(object message)
        // {
        //     Console.WriteLine(message);
        // }

        // Thread worker = new Thread(DoWork);
        // worker.Start();
        // Console.WriteLine("main thread continue...");

        // Parallel.For(0, 5 , i =>
        // {
        //     Console.WriteLine($"Processing items {i}");
        // });

        // int[] numbers = new int[10];
        // for (int i = 0 ; i < numbers.Length; i++)
        // numbers[1] = i + 1;
        // int sum = 0;
        // Parallel.For(0, numbers.Length, i =>{
        //     sum += numbers[i];
        // });
        // Console.WriteLine("Sum : " + sum);


        // int[] numbers = new int[10];
        // for (int i = 0; i < numbers.Length; i++)
        //     numbers[i] = i + 1;

        // int sum = 0;

        // Parallel.For(
        //     0,
        //     numbers.Length,
        //     () => 0,                 // Thread-local initialization
        //     (i, loopState, localSum) =>
        //     {
        //         return localSum + numbers[i];
        //     },
        //     localSum =>
        //     {
        //         Interlocked.Add(ref sum, localSum);
        //     });

        // Console.WriteLine("Sum: " + sum);

        

    }
        async static Task<int> GetDataAsync()
        {
            await Task.Delay(1000);
            return 42;

        }


    // static void DoWork()
    // {
    //     for (int i = 1 ; i<=5 ; i++)
    //     {
    //         Console.WriteLine("Worker Thread ... "+i);
    //         Thread.Sleep(1000);
    //     }
    // }
}


