using Amazon.DynamoDBv2.DataModel;
namespace dynamo_db_exploration.Dao;

[DynamoDBTable("-at-show-shared-data")]
public class ShowItem
{
    [DynamoDBHashKey(Attributes.PartitionKey)]
    public string PartitionKey { get; private set; }

    [DynamoDBRangeKey(Attributes.SortKey)]
    public string EventEditionId { get; set; }

    [DynamoDBProperty(Attributes.EventEditonGbsCode)]
    public string EventEditionGbsCode { get; set; }

    public ShowItem() { }
    
}