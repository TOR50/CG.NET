class Wallet()
{
    private double money ;

    public void AddMoney(double amount)
    {
        money  += amount;
    }

    public double GetBalance()
    {
        return money;
    }

}