using dynamo_db_exploration.Operations;

RetrieveOperation retrieveOperation = new ();

//retrieveOperation.UseLoadAsync();
//await retrieveOperation.UseGetItem();
//await retrieveOperation.UseBatchGetItem();
//await retrieveOperation.UseQueryFromDbClient();
await retrieveOperation.UseQueryFromDbContext();