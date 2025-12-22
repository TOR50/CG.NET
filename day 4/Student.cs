class Student()
{
    private string name;
    private int age;
    private int marks;

    private int stid;
    private string pass;
    public int admissionyear;
    private int regno;

    

    public int Regno
    {
        get;private set;
    }

    public int AdmissionYear 
    { 
        get; init; 
    }

    public double Percentage => (marks / 100.0) * 100;

    public int Stdid{get;set;}

    public string Status
    {
        get{return marks>= 40 ? "pass":"fail";}
        
    }

    public String Pass
    {
        set
        {
            if (value.Length >= 6)
            {
                pass = value;
            }
        }
    }

    public int Age
    {
        get{return age;}
        set{if (value > 0){
                age= value;
            }
        }
    }
    public int Marks
    {
        get{return marks;}
        set{if(value >= 0 && value <= 100)
            {
                marks = value;
            }
        }
    }
     public string Name
    {
        get
        {
            return name;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                name = value;
            }
        }
    }



}