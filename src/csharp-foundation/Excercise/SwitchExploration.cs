namespace csharp_foundation.Excercise;

public class SwitchExploration : IExcercise
{
    public void Run()
    {
        int employeeLevel = 100;
        string title = "";
        
        switch (employeeLevel)
        {
            case 100:
                title = "Level1";
                break;
            case 200:
                title = "Level 2";
                break;
            default:
                title = "Default";
                break;
        }
        
        Console.WriteLine(title);
    }
}