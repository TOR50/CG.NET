namespace HelloWorld
{
class Calculator
{
    int number1;

    int number2;

    int result;

    public void Add()
    {
        Console.WriteLine("Enter First Number : ");
        number1 = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter Second Number : ");
        number2 = int.Parse(Console.ReadLine());

        result=number1+number2;
        Console.WriteLine("Sum is "+result);

    }

    public void Multiply()
    {
        Console.WriteLine("Enter First Number : ");
        number1 = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter Second Number : ");
        number2 = int.Parse(Console.ReadLine());

        result=number1*number2;
        Console.WriteLine("Producr is "+result);
    }
    public void Subtract()
    {
        Console.WriteLine("Enter First Number : ");
        number1 = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter Second Number : ");
        number2 = int.Parse(Console.ReadLine());

        if(number1>number2)
        {
            result=number1-number2;
        }
        else
        {
            result=number2-number1;
        }
    }
}
}