using System;

namespace LibrarySystem
{
    public enum UserRole { Admin, Librarian, Member }
    public enum ItemStatus { Available, Borrowed, Reserved, Lost }

    public interface IReservable
    {
        void ReserveItem();
    }

    public interface INotifiable
    {
        void SendNotification(string message);
    }

    namespace Items
    {
        public abstract class LibraryItem
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public int ItemID { get; set; }
            public ItemStatus Status { get; set; }


            public abstract void DisplayItemDetails();
            public abstract double CalculateLateFee(int d);
        }

        public class Book : LibraryItem, IReservable, INotifiable
        {
            public override void DisplayItemDetails()
            {
                Console.WriteLine("Item Type: Book");
                Console.WriteLine($"Title is {Title} , Author is {Author} , ID = {ItemID}");
            }

            public override double CalculateLateFee(int d)
            {
                return d * 1.0;
            }

            void IReservable.ReserveItem()
            {
                Console.WriteLine("Book is reserved");
            }

            void INotifiable.SendNotification(string message)
            {
                Console.WriteLine($"Notification {message}");
            }
        }

        public class Magazine : LibraryItem
        {
            public override void DisplayItemDetails()
            {
                Console.WriteLine("Item Type: Magazine");
                Console.WriteLine($"Title is {Title} , Author is {Author} , ID = {ItemID}");
            }

            public override double CalculateLateFee(int d)
            {
                return d * 0.5;
            }
        }

        public class eBook : LibraryItem
        {
            public override void DisplayItemDetails() => Console.WriteLine($"Item Type eBook  {Title}");
            public override double CalculateLateFee(int d) => 0;
            public void Download() { Console.WriteLine("eBook downloaded"); }
        }
    }

    namespace Users
    {
        public class Member
        {
            public string Name { get; set; }
            public UserRole Role { get; set; }
        }
    }

    public partial class LibraryAnalytics
    {
        public static int TotalBorrowedItems { get; set; }
    }
}