using Amazon.DynamoDBv2.DataModel;

namespace dynamo_db_exploration.Dao;

[DynamoDBTable("-qr-manager")]
public class ShowConfig
{
    public ShowConfig() { }
    
    [DynamoDBHashKey("GroupId")] public string PartitionKey { get; private set; }
    [DynamoDBProperty("AlternateSortKey")] public string EventEditionId { get; private set; }
    [DynamoDBRangeKey("CustomSortKey")] public string EventEditionGbsCode { get; private set; }

    public void PrintEventEditionId()
    {
        Console.WriteLine("{ \"EventEditionId\" : \""+EventEditionId+"\" },");
    }
    
    public void PrintGbsCode()
    {
        Console.WriteLine("{ \"GbsCode\" : \""+EventEditionGbsCode+"\" },");
    }
}