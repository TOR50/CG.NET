using System;
using System.Reflection;

class Employee
{
    public string Name { get; set; }
    public double _salary = 50000.00;
    public void Work()
        {
            Console.WriteLine("work");
        }
}

class Program
{
    static void Main()
    {
        // Assembly assembly = Assembly.GetExecutingAssembly();
        // Console.WriteLine(assembly);
        
        // Assembly.LoadFrom("FirstApp.dll");

        // Console.WriteLine(assembly.FullName);
        

        // Type type = typeof(Employee);
        // Type type = Type.GetType("FirstApp.Models.Employee");


        // Employee obj = new Employee();
        // Console.WriteLine(type.Attributes);
        
        // Type tobj = obj.GetType();
        // Console.WriteLine(tobj);

        // Type type = typeof(Employee);
        // Employee obj = new Employee();
        // MethodInfo method = type.GetMethod("Work");
        // method.Invoke(obj, null);

        // Employee obj = new Employee();
        Type type = typeof(Employee);

        // PropertyInfo prop = type.GetProperty("Name");
        // prop.SetValue(obj, "John");
        // Console.WriteLine(obj.Name);

        // FieldInfo field = type.GetField("_salary", BindingFlags.NonPublic | BindingFlags.Instance);
        // field.SetValue(obj, 80000.00);
        // Console.WriteLine(obj._salary);


        // ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
        // object obj = ctor.Invoke(null);
        // new object is created and stored in obj
        // Create instances when types are unknown at compile time
        // Resolve constructor dependencies dynamically
        // Support inversion of control and dependency injection


        ConstructorInfo ctor = type.GetConstructor(new Type[] { typeof(string), typeof(int) });
        
        


    }
}