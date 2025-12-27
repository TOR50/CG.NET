using System;

class PatientBill
{
    public string BillId;
    public string PatientName;
    public bool HasInsurance;
    public decimal ConsultationFee;
    public decimal LabCharges;
    public decimal MedicineCharges;
    public decimal GrossAmount;
    public decimal DiscountAmount;
    public decimal FinalPayable;

    public void CalculateBill()
    {
        GrossAmount = ConsultationFee + LabCharges + MedicineCharges;
        
        if (HasInsurance == true)
        {
            DiscountAmount = GrossAmount * 0.10m;
        }
        else
        {
            DiscountAmount = 0;
        }
        
        FinalPayable = GrossAmount - DiscountAmount;
    }

    public void DisplayBill()
    {
        
        Console.WriteLine("PATIENT BILL DETAIL");
        Console.WriteLine($"Bill ID : {BillId}");
        Console.WriteLine($"Patient Name : {PatientName}");
        Console.WriteLine($"Has Insurance : {(HasInsurance ? "Yes" : "No")}");
        Console.WriteLine($"Consultation Fee : {ConsultationFee}");
        Console.WriteLine($"Lab Charges : {LabCharges}");
        Console.WriteLine($"Medicine Charges : {MedicineCharges}");
        Console.WriteLine($"Gross Amount : {GrossAmount}");
        Console.WriteLine($"Discount Amount : {DiscountAmount}");
        Console.WriteLine($"Final Payable : {FinalPayable}");
        
    }
}

class MediSureClinic
{
    static PatientBill LastBill;
    static bool HasLastBill = false;

    public static void CreateNewBill()
    {
        Console.WriteLine("Create New Bill");
        
        PatientBill bill = new PatientBill();
        
        Console.Write("Enter Bill ID : ");
        bill.BillId = Console.ReadLine();
        
        if (bill.BillId == "" || bill.BillId == null)
        {
            Console.WriteLine("Invalid Bill id");
            return;
        }
        
        Console.Write("Enter Patient Name : ");
        bill.PatientName = Console.ReadLine();
        
        Console.Write("Does patient have insurance? (Y/N) : ");
        string insurance = Console.ReadLine();
        
        if (insurance == "Y" || insurance == "y")
        {
            bill.HasInsurance = true;
        }
        else if (insurance == "N" || insurance == "n")
        {
            bill.HasInsurance = false;
        }
        else
        {
            Console.WriteLine("Invalid input");
            return;
        }
        
        Console.Write("Enter Consultation Fee : ");
        string Input = Console.ReadLine();
        bill.ConsultationFee = Convert.ToDecimal(Input);
        
        if (bill.ConsultationFee <= 0)
        {
            Console.WriteLine("Invalid Fee");
            return;
        }
        
        Console.Write("Enter Lab Charges : ");
        string lab = Console.ReadLine();
        bill.LabCharges = Convert.ToDecimal(lab);
        
        if (bill.LabCharges < 0)
        {
            Console.WriteLine("Invalid Charge");
            return;
        }
        
        Console.Write("Enter Medicine Charges : ");
        string med = Console.ReadLine();
        bill.MedicineCharges = Convert.ToDecimal(med);
        
        if (bill.MedicineCharges < 0)
        {
            Console.WriteLine("Invalid Charge");
            return;
        }
        
        bill.CalculateBill();
        
        LastBill = bill;
        HasLastBill = true;
        
        Console.WriteLine("Bill created successfully");
    }
    public static void ViewLastBill()
    {
        if (HasLastBill == false)
        {
            Console.WriteLine("No bill available. Please create a new bill");
        }
        else
        {
            LastBill.DisplayBill();
        }
    }

    public static void ClearLastBill()
    {
        LastBill = null;
        HasLastBill = false;
        Console.WriteLine("Last bill cleared.");
    }

    public static void ShowMenu()
    {
        Console.WriteLine("MEDISURE CLINIC BILLING SYSTEM");
        Console.WriteLine("1. Create New Bill (Enter Patient Details)");
        Console.WriteLine("2. View Last Bill");
        Console.WriteLine("3. Clear Last Bill");
        Console.WriteLine("4. Exit");
        Console.Write("Enter your choice : ");
    }
}