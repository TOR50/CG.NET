using System;
using System.Collections.Generic;
using LibrarySystem;
using LibrarySystem.Users;
using Items = LibrarySystem.Items;

namespace LibrarySystem
{
    public partial class LibraryAnalytics
    {
        public static void DisplayAnalytics()
        {
            Console.WriteLine($"Total Items Borrowed : {TotalBorrowedItems}");
        }
    }
}

class Program
{
    static void Main()
    {
        Items.Book myBook = new Items.Book { Title = "C# Fundamentals", Author = "John Doe", ItemID = 101, Status = ItemStatus.Borrowed };
        Items.Magazine myMag = new Items.Magazine { Title = "Tech Today", Author = "Jane Doe", ItemID = 201, Status = ItemStatus.Available };

        
        myBook.DisplayItemDetails();
        Console.WriteLine("Late Fee for 3 days : " + myBook.CalculateLateFee(3));
        myMag.DisplayItemDetails();
        Console.WriteLine("Late Fee for 3 days : " + myMag.CalculateLateFee(3));

        IReservable res = myBook;
        res.ReserveItem();
        INotifiable note = myBook;
        note.SendNotification("Please return book on time");


        List<Items.LibraryItem> inventory = new List<Items.LibraryItem> { myBook, myMag };
        foreach (var item in inventory)
        {
            item.DisplayItemDetails();
        }
        LibraryAnalytics.TotalBorrowedItems = 5;
        LibraryAnalytics.DisplayAnalytics();

        Member m = new Member { Name = "Student", Role = UserRole.Member };
        Console.WriteLine($"User Role {m.Role}");
        Console.WriteLine($"Item Status {myBook.Status}");

        Items.eBook digital = new Items.eBook { Title = "Digital C#" };
        digital.Download();
    }
}