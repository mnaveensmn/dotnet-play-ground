using System.Diagnostics;
using dynamo_db_load_testing.Test;using dynamo_db_load_testing.Utilities;

//await ConcurrentDbCall();

//await new ServiceClient().SendRequest();
for (int i = 0; i < 100; i++)
{
    await new LoadTest("").TestQueryAsyncMethod();

}

async Task ConcurrentDbCall()
{
    const int numberOfConcurrentCalls = 1000;
    Console.WriteLine($"Attempting to run {numberOfConcurrentCalls} LoadTest.Execute() calls concurrently...");
    Console.WriteLine("Testing is running...\n\nIt will take sometime to complete");

    var stopwatch = Stopwatch.StartNew();

    Console.WriteLine("Time,ExhibitorId,ElapsedTime,Error");

    var loadTestTasks = new List<Task>();

    for (var i = 0; i < numberOfConcurrentCalls; i++)
    {
        loadTestTasks.Add(new LoadTest($"thread-{i}").Execute());
    }

    await Task.WhenAll(loadTestTasks);

    stopwatch.Stop();
    Console.WriteLine($"\nAll {numberOfConcurrentCalls} LoadTest.Execute() calls completed.");
    Console.WriteLine($"Total elapsed time: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
}