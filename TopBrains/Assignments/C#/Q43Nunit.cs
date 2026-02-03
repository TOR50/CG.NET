
public class UnitTest
{
    private Program _account;

    [SetUp]
    public void Setup()
    {
        _account = new Program(100.0m);
    }

    [Test]
    public void Test_Deposit_ValidAmount()
    {
        _account.Deposit(50.0m);
        
        Assert.AreEqual(150.0m, _account.Balance);
    }

    [Test]
    public void Test_Deposit_NegativeAmount()
    {
        var ex = Assert.Throws<Exception>(() => _account.Deposit(-10.0m));
        Assert.AreEqual("Deposit amount cannot be negative", ex.Message);
    }

    [Test]
    public void Test_Withdraw_ValidAmount()
    {
        _account.Withdraw(40.0m);
        
        Assert.AreEqual(60.0m, _account.Balance);
    }

    [Test]
    public void Test_Withdraw_InsufficientFunds()
    {
        var ex = Assert.Throws<Exception>(() => _account.Withdraw(200.0m));
        Assert.AreEqual("Insufficient funds.", ex.Message);
    }
}