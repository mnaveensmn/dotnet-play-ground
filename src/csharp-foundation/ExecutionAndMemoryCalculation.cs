using System.Diagnostics;

namespace csharp_foundation;

public class ExecutionAndMemoryCalculation
{
    public static void Run()
    {
        int n1 = 10 * 9 + 40;

        var qrProfileEntity = new List<string>();
        var algoliaSearch = new List<string>();

        GC.Collect();
        long memoryBefore = GC.GetTotalMemory(true);

        var taskWatch = new Stopwatch();

        taskWatch.Restart();
        for (int i = 0; i < n1; i++)
        {
            qrProfileEntity.Add(i.ToString());
            algoliaSearch.Add(i + " Modified Data");
        }

        taskWatch.Stop();

        long memoryAfter = GC.GetTotalMemory(true);

        Console.WriteLine($"Memory Used: {(memoryAfter - memoryBefore) / 1024.0} KB");
        Console.WriteLine("Old Code " + taskWatch.ElapsedMilliseconds + "ms");

//===========================

        GC.Collect();
        memoryBefore = GC.GetTotalMemory(true);
        var qrProfileEntity2 = new List<string>();
        taskWatch.Restart();

        for (int i = 0; i < n1; i++)
        {
            qrProfileEntity2.Add(i.ToString());
        }

        var algoliaSearch2 = qrProfileEntity2.Select(x => x + " Modified Data").ToList();
        taskWatch.Stop();

        memoryAfter = GC.GetTotalMemory(true);

        Console.WriteLine();
        Console.WriteLine($"Memory Used: {(memoryAfter - memoryBefore) / 1024.0} KB");
        Console.WriteLine("New Code " + taskWatch.ElapsedMilliseconds + "ms");
    }
}