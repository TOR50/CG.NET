using System;
public class Program
{
    public static void Main()
    {
        // bool running = true;
        // while (running == true)
        // {
        //     MediSureClinic.ShowMenu();
        //     string choice = Console.ReadLine();
        //     switch (choice)
        //     {
        //         case "1":
        //             MediSureClinic.CreateNewBill();
        //             break;
        //         case "2":
        //             MediSureClinic.ViewLastBill();
        //             break;
        //         case "3":
        //             MediSureClinic.ClearLastBill();
        //             break;
        //         case "4":
        //             Console.WriteLine("Thank you. Application closed normally.");
        //             running = false;
        //             break;
        //         default:
        //             Console.WriteLine("Invalid Input");
        //             break;
        // }
        // }

        bool running = true;
        while(running == true)
        {
            QuickMartTransaction.ShowMenu();
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    QuickMartTransaction.CreateNewTransaction();
                    break;
                case "2":
                    QuickMartTransaction.ViewLastTransaction();
                    break;
                case "3":
                    QuickMartTransaction.CalculateProfitLoss();
                    break;
                case "4":
                    Console.WriteLine("Thank you. Application closed normally.");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }

        }

}
}