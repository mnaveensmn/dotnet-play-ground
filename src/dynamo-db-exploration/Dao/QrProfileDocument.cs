using Amazon.DynamoDBv2.DataModel;
using Microsoft.VisualBasic;

namespace dynamo_db_exploration.Dao;


[DynamoDBTable("-qr-manager")]
public class QrProfileDocument
{
    public QrProfileDocument()
    {
    }

    [DynamoDBHashKey]
    public string? GroupId { get; private set; }

    [DynamoDBRangeKey]
    public string? CustomSortKey { get; private set; }

    public string? AlternateSortKey { get; private set; }

    public string? ShortCode { get; private set; }
    
    public void PrintGbsCodeAndShortCode()
    {
        Console.WriteLine("{ \"GbsCode\" : \""+GroupId+"\", \"ShortCode\": \""+ShortCode+"\" },");
    }

}