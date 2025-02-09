namespace csharp_foundation;

public class CastingExploration
{
    public void Run()
    {
      
        var parentClass = new ParentClass();
        var childClass = new ChildClass();
        var newVar = (ChildClass)parentClass;
        Console.WriteLine(newVar.Name);
    }
    
}


class ParentClass
{
    public string Name { get; set; } = "Parent Class";
}

internal class ChildClass : ParentClass
{
}