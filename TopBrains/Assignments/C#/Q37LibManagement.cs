using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrarySystem
{
    class Program
    {
        static List<dynamic> bookList = new List<dynamic>();

        static void Main(string[] args)
        {
            SeedData();

            while (true)
            {
                Console.WriteLine("\n=== BOOK LIBRARY MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. User Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select Role: ");
                
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AdminMenu();
                        break;
                    case "2":
                        UserMenu();
                        break;
                    case "3":
                        Console.WriteLine("Exiting System. Thank you!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- ADMIN MENU ---");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Update Book");
                Console.WriteLine("3. Delete Book");
                Console.WriteLine("4. View All Books");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": UpdateBook(); break;
                    case "3": DeleteBook(); break;
                    case "4": ViewAllBooks(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Enter Book ID: ");
            int id = int.Parse(Console.ReadLine());

            if (bookList.Exists(b => b.Id == id))
            {
                Console.WriteLine("Error: Book ID already exists.");
                return;
            }

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Author: ");
            string author = Console.ReadLine();
            Console.Write("Enter Publisher: ");
            string publisher = Console.ReadLine();
            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            var newBook = new { Id = id, Title = title, Author = author, Publisher = publisher, Price = price };
            bookList.Add(newBook);

            Console.WriteLine("Book added successfully!");
        }

        static void UpdateBook()
        {
            Console.Write("Enter Book ID to update: ");
            int id = int.Parse(Console.ReadLine());

            var bookToUpdate = bookList.FirstOrDefault(b => b.Id == id);

            if (bookToUpdate != null)
            {
                bookList.Remove(bookToUpdate);

                Console.WriteLine($"Updating Book: {bookToUpdate.Title}");
                Console.Write("Enter New Title: ");
                string title = Console.ReadLine();
                Console.Write("Enter New Author: ");
                string author = Console.ReadLine();
                Console.Write("Enter New Publisher: ");
                string publisher = Console.ReadLine();
                Console.Write("Enter New Price: ");
                decimal price = decimal.Parse(Console.ReadLine());

                var updatedBook = new { Id = id, Title = title, Author = author, Publisher = publisher, Price = price };
                bookList.Add(updatedBook);

                Console.WriteLine("Book updated successfully!");
            }
            else
            {
                Console.WriteLine("Book ID not found.");
            }
        }

        static void DeleteBook()
        {
            Console.Write("Enter Book ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            var book = bookList.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                bookList.Remove(book);
                Console.WriteLine("Book deleted successfully.");
            }
            else
            {
                Console.WriteLine("Book ID not found.");
            }
        }

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- USER MENU ---");
                Console.WriteLine("1. Browse All Books");
                Console.WriteLine("2. Search by Title");
                Console.WriteLine("3. Search by Publisher");
                Console.WriteLine("4. View Highest Price Book");
                Console.WriteLine("5. View Lowest Price Book");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAllBooks(); break;
                    case "2": SearchByTitle(); break;
                    case "3": SearchByPublisher(); break;
                    case "4": ViewHighestPrice(); break;
                    case "5": ViewLowestPrice(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void ViewAllBooks()
        {
            if (bookList.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            Console.WriteLine("\n{0,-5} | {1,-20} | {2,-15} | {3,-15} | {4,10}", "ID", "Title", "Author", "Publisher", "Price");
            Console.WriteLine(new string('-', 75));

            foreach (var b in bookList)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-15} | {3,-15} | {4,10:C}", b.Id, b.Title, b.Author, b.Publisher, b.Price);
            }
        }

        static void SearchByTitle()
        {
            Console.Write("Enter Book Title to search: ");
            string keyword = Console.ReadLine().ToLower();

            var results = bookList.Where(b => b.Title.ToLower().Contains(keyword)).ToList();
            DisplayResults(results);
        }

        static void SearchByPublisher()
        {
            Console.Write("Enter Publisher to search: ");
            string keyword = Console.ReadLine().ToLower();

            var results = bookList.Where(b => b.Publisher.ToLower().Contains(keyword)).ToList();
            DisplayResults(results);
        }

        static void ViewHighestPrice()
        {
            if (bookList.Count == 0) return;
            
            decimal maxPrice = 0;
            foreach(var b in bookList)
            {
                if(b.Price > maxPrice) maxPrice = b.Price;
            }

            var results = bookList.Where(b => b.Price == maxPrice).ToList();
            Console.WriteLine("\n--- Most Expensive Book(s) ---");
            DisplayResults(results);
        }

        static void ViewLowestPrice()
        {
            if (bookList.Count == 0) return;

            decimal minPrice = decimal.MaxValue;
            foreach (var b in bookList)
            {
                if (b.Price < minPrice) minPrice = b.Price;
            }

            var results = bookList.Where(b => b.Price == minPrice).ToList();
            Console.WriteLine("\n--- Cheapest Book(s) ---");
            DisplayResults(results);
        }

        static void DisplayResults(List<dynamic> results)
        {
            if (results.Count == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            foreach (var b in results)
            {
                Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Publisher: {b.Publisher}, Price: {b.Price:C}");
            }
        }

        static void SeedData()
        {
            bookList.Add(new { Id = 101, Title = "C# Mastery", Author = "Jon Skeet", Publisher = "Manning", Price = 45.00m });
            bookList.Add(new { Id = 102, Title = "Clean Code", Author = "Robert Martin", Publisher = "Pearson", Price = 50.00m });
            bookList.Add(new { Id = 103, Title = "Design Patterns", Author = "GoF", Publisher = "Addison", Price = 60.00m });
            bookList.Add(new { Id = 104, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Publisher = "Addison", Price = 55.00m });
        }
    }
}