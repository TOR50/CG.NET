public abstract class InsurancePolicy
{
    public string Policynum { get; init; }
    public string Name { get; set; }

    private int Premium;

    public int premium
    {
        get => Premium;
        set
        {
            if (value <= 0)
            {
                Console.WriteLine("Premium is zero");
            }
            else
            {
            Premium = value;
            }
        }
    }
    protected InsurancePolicy(string policynum , string name , int p)
    {
        Policynum = policynum;
        Name = name;
        Premium = p;
    }

    public virtual void CalcPremium()
    {
        Console.WriteLine($" Premium is {Premium}");
    }

    public void GenericPolicy()
    {
        Console.WriteLine("Generic Insurance Policy ... ");
    }
}
    public class Lic : InsurancePolicy
    {
    private int add = 10;

    public Lic(string policynum , string name , int basePremium) : base(policynum , name , basePremium){}

    public override void CalcPremium()
    {
        Console.WriteLine($"Life Premium {premium + add}");
    }

    public new void ShowPolicy()
    {
        Console.WriteLine("Life Insurance Policy");
    }
    }

public class HealthInsurance : InsurancePolicy
{
    public HealthInsurance(string policynum, string name, int basePremium) 
        : base(policynum, name, basePremium)
    {
    }

    public sealed override void CalcPremium()
    {
        Console.WriteLine($"Health Premium: {premium}");
    }
}

public class PolicyDirectory
{
    private List<InsurancePolicy> p = new List<InsurancePolicy>();

    public void AddPolicy(InsurancePolicy policy)
    {
        p.Add(policy);
    }

    public InsurancePolicy this[int i]
    {
        get
        {
            if (i >= 0 && i < p.Count)
                return p[i];
            return null;
        }
    }

    public InsurancePolicy this[string name]
    {
        get
        {
            foreach (var policy in p)
            {
                if (policy.Name == name)
                    return policy;
            }
            return null;
        }
    }
}




public sealed class Auth
{   
    public string Message(){
        return "User authenticated successfully";
    }
}
