
using csharp_foundation;

//new EnumExploration().Run();


var map = new Dictionary<string, ITestInterface>
{
    ["a"] = new Class1(),
    ["b"] = new Class2()
};

map["a"].Run();

interface ITestInterface
{
    void Run();
}

class Class1 : ITestInterface
{
    public Class1()
    {
        Console.WriteLine("Class 1");
    }
    
    public void Run()
    {
        Console.WriteLine("Run Class 1");
    }
}

class Class2 : ITestInterface
{
    public Class2()
    {
        Console.WriteLine("Class 2");
    }
    
    public void Run()
    {
        Console.WriteLine("Run Class 2");
    }
}


