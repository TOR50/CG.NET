// class Parent
// {
//     public Parent(int x)
//     {
//         Console.WriteLine("Parent constructor: " + x);
//     }
// }

// class Child : Parent
// {
//     public Child() : base(10)
//     {
//         Console.WriteLine("Child constructor");
//     }
// }

// class BankAcc()
// {
//      int accnum = Convert.ToInt32(012222121);
//      int balance = Convert.ToInt32(520000000);

//      static Disp()
// {
//     Console.WriteLine("constructor");
// }
    
// }
// static constructor used to initialize static member of the class

class Product
{
    public string Name;
    public int Price;

    public Product() { }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }
}
