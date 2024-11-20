using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using dynamo_db_exploration.Dao;
using dynamo_db_exploration.Shared;

namespace dynamo_db_exploration.Operations;


public class RetrieveOperation
{
    private readonly AmazonDynamoDBClient _dbClient;
    private readonly DynamoDBContext _dbContext;
    private const string HashKey = Constant.HashKey;
    private const string RangeKey = Constant.RangeKey;
   
    public RetrieveOperation()
    {
        _dbClient = new AmazonDynamoDBClient();
        _dbContext = new DynamoDBContext(_dbClient, new DynamoDBContextConfig
        {
            TableNamePrefix = Constant.TableNamePrefix
        });
    }
    
    public async void UseLoadAsync()
    {
        var data = await _dbContext.LoadAsync<DynamoTable>(HashKey,RangeKey,CancellationToken.None);
        Console.WriteLine(data?.ColumnTwo);
    }

    public async Task UseGetItem()
    {
        var request = new GetItemRequest
        {
            TableName = Constant.TableName,
            Key = new Dictionary<string,AttributeValue>() {
                { Constant.ColumnOne, new AttributeValue { S = HashKey } },
                { Constant.ColumnTwo, new AttributeValue { S = RangeKey } }
            }
        };
        
        var response = await _dbClient.GetItemAsync(request);
        var result = response.Item;
        Console.WriteLine(result.Count);
    }

    public async Task UseBatchGetItem()
    {
        var request = new BatchGetItemRequest
        {
            RequestItems = new Dictionary<string, KeysAndAttributes>()
            {
                { 
                    Constant.TableName,
                    new KeysAndAttributes
                    {
                        Keys = new List<Dictionary<string, AttributeValue>>()
                        {
                            new Dictionary<string, AttributeValue>()
                            {
                                { Constant.ColumnOne, new AttributeValue { S = HashKey } },
                                { Constant.ColumnTwo, new AttributeValue { S = RangeKey } }
                            }
                        }
                    } 
                } 
            } 
        };
        var response = await _dbClient.BatchGetItemAsync(request);
        var result= response.Responses;
        Console.WriteLine(result.ToString());
    }
    

    public async Task UseQueryFromDbClient()
    {
        var queryRequest = new QueryRequest
        {
            TableName = Constant.TableName,
            KeyConditionExpression = $"{Constant.ColumnOne} = :HashKey",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":HashKey", new AttributeValue { S = HashKey} }
            },
            ScanIndexForward = false
        };
        var queryResponse = await _dbClient.QueryAsync(queryRequest);
        Console.WriteLine(queryResponse.Count);
    }

    public async Task UseQueryFromDbContext()
    {
        var search = _dbContext.QueryAsync<DynamoTable>(HashKey);

        while (!search.IsDone)
        {
            var GetRemainingAsync = await search.GetNextSetAsync();
        }
    }
    
}