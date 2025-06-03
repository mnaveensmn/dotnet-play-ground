namespace dynamo_db_load_testing.Test;

public abstract class BaseLoadTest
{
    public abstract Task TestCallBack();

    public async Task Execute()
    {
        TimeSpan programDuration = TimeSpan.FromSeconds(300); // Run for 10 seconds
        
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            cts.CancelAfter(programDuration); // Automatically cancels after the duration

            try
            {
                await ExecuteCallBack(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nProgram execution cancelled due to duration limit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }
        }
    }

    private async Task ExecuteCallBack(CancellationToken cancellationToken)
    {
        int counter = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await TestCallBack();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Work in progress was cancelled.");
                throw;
            }
        }
    }
}