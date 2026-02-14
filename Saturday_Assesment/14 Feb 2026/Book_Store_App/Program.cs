using System;

    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }

    public class BookUtility
    {
        private Book Thebook;

        public BookUtility(Book book)
        {
            Thebook = book;
        }

        public void GetBookDetails()
        {
            Console.WriteLine("Details:");
            Console.WriteLine($"{Thebook.Id} {Thebook.Title} {Thebook.Price} {Thebook.Stock}");
        }

        public void UpdateBookPrice(int newPrice)
        {
            Thebook.Price = newPrice;
            Console.WriteLine("Updated");
            Console.WriteLine($"Price: {Thebook.Price}");
        }

        public void UpdateBookStock(int newStock)
        {
            Thebook.Stock = newStock;
            Console.WriteLine("Updated");
            Console.WriteLine($"Stock: {Thebook.Stock}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string initialInput = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(initialInput)) return;

                string[] details = initialInput.Split(' ');

                Book myBook = new Book
                {
                    Id = details[0],
                    Title = details[1],
                    Author = "Unknown", 
                    Price = int.Parse(details[2]),
                    Stock = int.Parse(details[3])
                };

                BookUtility utility = new BookUtility(myBook);

                bool exit = false;
                while (!exit)
                {
                    string choiceInput = Console.ReadLine();
                    if (!int.TryParse(choiceInput, out int choice)) continue;

                    switch (choice)
                    {
                        case 1:
                            utility.GetBookDetails();
                            break;

                        case 2:
                            int newPrice = int.Parse(Console.ReadLine());
                            utility.UpdateBookPrice(newPrice);
                            break;

                        case 3:
                            int newStock = int.Parse(Console.ReadLine());
                            utility.UpdateBookStock(newStock);
                            break;

                        case 4:
                            Console.WriteLine("Thank");
                            Console.WriteLine("You");
                            exit = true;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }