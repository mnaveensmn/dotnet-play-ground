using dynamo_db_exploration.Operations;

RetrieveOperation retrieveOperation = new ();

//retrieveOperation.UseLoadAsync();
//await retrieveOperation.UseGetItem();
//await retrieveOperation.UseBatchGetItem();
//await retrieveOperation.UseQueryFromDbClient();
//await retrieveOperation.GetShowConfig();

foreach (var x in Enumerable.Range(1,200))
{
    await retrieveOperation.GetQrProfile($"PERF-LOAD-TEST-{x}");
}

// foreach (var x in Enumerable.Range(1,201))
// {
//     await retrieveOperation.DeleteQrProfileRecords($"PERF-LOAD-TEST-{x}");
// }

// await retrieveOperation.DeleteShowRecords();