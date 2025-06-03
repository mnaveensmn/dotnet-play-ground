using Amazon.DynamoDBv2.DataModel;
using dynamo_db_load_testing.Shared;

namespace dynamo_db_load_testing.Entities;

[DynamoDBTable(Constants.TableName)]
public class SomeItem 
{
    [DynamoDBHashKey(Attributes.PartitionKey)]
    public string SomePartitionKey { get; private set; }
    
    [DynamoDBRangeKey(Attributes.SortKey)]
    public string SomeSortKey { get; private set; }
    
    [DynamoDBProperty(Attributes.SomeProperty)]
    public string? EventEditionId { get; private set; }
    
    public SomeItem() { }
}

