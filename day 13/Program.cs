// using System;
// delegate void PaymentDelegate(decimal amount);

// class PaymentService
// {
//     public void ProcessPayment(decimal amount)
//     {
//         Console.WriteLine("payment of " +amount +" processsed Sucessfuly ");
//     }
// }
// static class PaymentExtension
// {
//     public static bool IsvalidPayment(this decimal amount)
//     {
//         return amount > 0 && amount <= 1_000_000;

//     }
// }

// delegate void OrderDelegate(string orderID);
// class NotificationService
// {
//     public void SendEmail(string id)
//     {
//         Console.WriteLine("order Email");
//     }
//     public void SendSMS(string id)
//     {
//         Console.WriteLine("order SMS");
//     }
// }

// delegate void ErrorDelegate(string message);

// class Button
// {
//     // Step 1: Declare a delegate
//     public delegate void ClickHandler();
//     // Step 2: Declare an event using the delegate
//     public event ClickHandler Clicked; // CLICKED is the event name
//     // Step 3: Method that raises the event
//     public void Click()
//     {
//         Clicked?.Invoke();
//     }

//     public delegate void Subscribed();
//     public event Subscribed Sub;
//     public event Subscribed Liked;
//     public void Subscribe()
//     {
//         Sub?.Invoke();
//     }
//     public void Like()
//     {
//         Liked?.Invoke();
//     }
// }
// class Program
// {
//     static void Main()
//     {
        // PaymentService service  = new PaymentService();
        // PaymentDelegate payment = service.ProcessPayment; //delegate assignment
        // // payment(68000);
        // // Console.WriteLine("Enter amount : ");
        // decimal amount = 50000;

        // if (amount.IsvalidPayment())
        // {
        //     payment(amount);
        // }
        // else
        // {
        //     Console.WriteLine("Invalid payment amount !!!");
        // }

        // NotificationService service = new NotificationService();
        // OrderDelegate notify = null;
        // notify += service.SendEmail;
        // notify += service.SendSMS;
        // notify("ORD1001");

        // Action<string> logActivity = message => Console.WriteLine("Log Entry : "+message);
        // logActivity("User loged in at 1500 Hrs");

        // Func<decimal, decimal, decimal> calculateDiscount = (price, discount) => price - (price * discount/100);
        // Console.WriteLine(calculateDiscount(1000,10));

        // Predicate<int> isEligible = age => age >= 18;
        // Console.WriteLine(isEligible(20));

        // ErrorDelegate errorHandler = delegate(string msg)
        // {
        //     Console.WriteLine("Error : "+msg );     
        // };
        // errorHandler("File not found");

//         Button btn = new Button();
//         // Step 4: Subscribe a method to the event
//         btn.Clicked += () => Console.WriteLine("Button was clicked");
//         btn.Click();

//         btn.Sub += () => Console.WriteLine("You Subscribed");
//         btn.Subscribe();

//         btn.Liked += () => Console.WriteLine("Liked clicked");
//         btn.Like();


//     }
// }



// using System;
// using System.Collections.Generic;

// namespace SmartHomeSecurity
// {
//     // 1. DEFINITION: The "Contract" for any security response.
//     // Any method responding to an alert must be void and take a string location.

//     public delegate void SecurityAction(string zone); // definition

//     public class MotionSensor
//     {
//         // The delegate instance (The Panic Button)
//         public SecurityAction OnEmergency; // instance creation

//         public void DetectIntruder(string zoneName)
//         {
//             Console.WriteLine($"[SENSOR] Motion detected in {zoneName}!");
            
//             // 3. INVOCATION: Triggering the Panic Button
//             if (OnEmergency != null)
//             {
//                 OnEmergency(zoneName); // string value = Main Lobby or panicSequence?
//             }
//         }
//     }

//     // Diverse classes that don't know about each other
//     public class AlarmSystem
//     {
//         public void SoundSiren(string zone) => Console.WriteLine($"[ALARM] WOO-OOO! High-decibel siren active in {zone}.");
//     }

//     public class PoliceNotifier
//     {
//         public void CallDispatch(string zone) => Console.WriteLine($"[POLICE] Notifying local precinct of intrusion in {zone}.");
//     }

//     class Program
//     {
//         static void Main()
//         {
//             // Objects Initialization
//             MotionSensor livingRoomSensor = new MotionSensor();
//             AlarmSystem siren = new AlarmSystem();
//             PoliceNotifier police = new PoliceNotifier();

//             // 2. INSTANTIATION & MULTICASTING
//             // We "Subscribe" different methods to the sensor's delegate
//             SecurityAction panicSequence = siren.SoundSiren; // Assignment of methods
//             panicSequence += police.CallDispatch;

//             // Linking the sequence to the sensor
//             livingRoomSensor.OnEmergency = panicSequence;
// 	// class_object.delegate_instance = delegate_instance_multicast

//             // Simulation
//             livingRoomSensor.DetectIntruder("Main Lobby");
//         }
//     }
// }

using System;
using System.Threading;

namespace CallbackDemo
{
    // STEP 1: Define the Delegate
    public delegate void DownloadFinishedHandler(string fileName);

    class FileDownloader
    {
        // STEP 2: Method that accepts the callback
        public void DownloadFile(string name, DownloadFinishedHandler callback)
        {
            Console.Write($"Starting download : {name}\n");
            
            // Simulating work
            // Thread.Sleep(2000);

            for (int i = 0; i <= 100; i++)
            {
                Console.Write($"\rDownloading... {i}%");
                Thread.Sleep(3);
            }

            Console.WriteLine($"\n{name} download complete.");

            // STEP 3: Execute the Callback
            if (callback != null)
            {
                callback(name); 
            }
        }
    }

    class Program
    {
        // STEP 4: The actual Callback Method
        static void DisplayNotification(string file)
        {
            Console.WriteLine($"NOTIFICATION: You can now open {file}.");
        }

        static void Main()
        {
            FileDownloader downloader = new FileDownloader();

            // Pass the method 'DisplayNotification' as a callback
            downloader.DownloadFile("Presentation.pdf", DisplayNotification);
        }
    }
}