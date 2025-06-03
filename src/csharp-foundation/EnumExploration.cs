namespace csharp_foundation;

public class EnumExploration
{
    private Dictionary<FirstEnum, SecondEnum> map = new()
    {
        [FirstEnum.One] = SecondEnum.A,
        [FirstEnum.Two] = SecondEnum.B,
    };
    
    public void Run()
    {
        Console.WriteLine(map.GetValueOrDefault(FirstEnum.Two, SecondEnum.A).ToString());
        Console.WriteLine(map.GetValueOrDefault(FirstEnum.Three, SecondEnum.A).ToString());
    }
    
}

enum FirstEnum
{
    One,
    Two,
    Three
}

enum SecondEnum
{
   A,
   B,
}