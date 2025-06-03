namespace csharp_foundation.Excercise;

public class SwitchExploration : IExcercise
{
    public void Run()
    {
        var employeeLevel = 22;

        var title = employeeLevel switch
        {
            100 => "Level1",
            200 => "Level 2",
            _ => "Default"
        };

        Console.WriteLine(title);
    }
}