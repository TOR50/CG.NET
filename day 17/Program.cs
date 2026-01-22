using System;
using System.Reflection;
using System.Linq;
// namespace PettyCash
// {
    // public abstract class BaseEntity
    // {
    //     public Guid Id { get; set; } = Guid.NewGuid();
    //     public DateTime CreatedAt { get; set; } = DateTime.Now;
    // }

    // public interface IRepository<T> where T : BaseEntity
    // {
    //     void Add(T entity);
    //     IEnumerable<T> GetAll();
    //     T GetById(Guid id);
    // }
    // public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    // {
    //     private readonly List<T> _items = new List<T>();
    //     public void Add(T entity) => _items.Add(entity);
    //     public IEnumerable<T> GetAll() => _items;
    //     public T GetById(Guid id) => _items.FirstOrDefault(x => x.Id == id);
    // }
    // public enum TransactionStatus { Pending, Approved, Rejected }

    // public abstract class Transaction : BaseEntity
    // {
    //     public decimal Amount { get; set; }
    //     public string Narration { get; set; }
    //     public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
    //     public abstract decimal GetImpact(); 
    // }

    // public class Expense : Transaction
    // {
    //     public string VoucherNumber { get; set; }
    //     public string Category { get; set; }
    //     public override decimal GetImpact() => -Amount;
    // }

    // public class Reimbursement : Transaction
    // {
    //     public override decimal GetImpact() => Amount;
    //     public Reimbursement() => Status = TransactionStatus.Approved;
    // }

    // public class PettyCashFund : BaseEntity
    // {
    //     public string Name { get; set; }
    //     public decimal OpeningBalance { get; set; }
    // }
    public interface I1
{
    void m1();
    void m2();
}
public class ClassA : I1
{
    public void m1()
        {
            Console.WriteLine("Executing m1...");
        }

        public void m2()
        {
            Console.WriteLine("Executing m2...");
        }
        public string MyProperty { get; set; }
}

    

    class program
    {
        static void Main()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.IsClass)
                {
                    Console.WriteLine($"Class: {type.Name}");
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Length > 0)
                    {
                        Console.WriteLine("  Implemented Interfaces:");
                        foreach (var i in interfaces)
                        {
                            Console.WriteLine($"    - {i.Name}");
                        }
                    }
                    Console.WriteLine("  Members:");
                    var members = type.GetMembers(BindingFlags.Public | 
                                                  BindingFlags.Instance | 
                                                  BindingFlags.DeclaredOnly);
                    
                    foreach (var member in members)
                    {
                        Console.WriteLine($"    - [{member.MemberType}] {member.Name}");
                    }
                    
                    Console.WriteLine(new string('-', 30));
                }
            }
        }
    }
// }