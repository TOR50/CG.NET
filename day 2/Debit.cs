public class Debit()
{
    public float NetSal(float netsal)
    {
        float deduction = netsal  / 100 ;
        netsal = netsal - deduction;
        return netsal;
    }

    public float MaturityAmount(float p , float r , float t)
    {
        float si = (p * r *(t/12))/100;
        float maturity = si + p;
        return maturity;
    }

    public float RewardPoint(float sp)
    {
        float   reward = sp /100;
        return reward;
    }

    public bool BonusCheck(float anualsal , float years)
    {
        if(anualsal>=500000 && years >= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
}