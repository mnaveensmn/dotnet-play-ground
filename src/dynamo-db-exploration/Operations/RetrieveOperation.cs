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
        var data = await _dbContext.LoadAsync<DynamoTable>(HashKey, RangeKey, CancellationToken.None);
        Console.WriteLine(data?.ColumnTwo);
    }

    public async Task UseGetItem()
    {
        var request = new GetItemRequest
        {
            TableName = Constant.TableName,
            Key = new Dictionary<string, AttributeValue>()
            {
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
        var result = response.Responses;
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
                { ":HashKey", new AttributeValue { S = HashKey } }
            },
            ScanIndexForward = false
        };
        var queryResponse = await _dbClient.QueryAsync(queryRequest);
        Console.WriteLine(queryResponse.Count);
    }

    public async Task UseQueryFromDbContext()
    {
        var search = _dbContext.QueryAsync<ShowConfig>("SHOW_DETAILS");
        List<ShowConfig> showConfigs = null;

        while (!search.IsDone)
        {
            showConfigs = await search.GetNextSetAsync();
        }

        showConfigs.ForEach(x => x.PrintEventEditionId());
    }

    public async Task DeleteShowConfigRecords()
    {
        var search = _dbContext.QueryAsync<ShowConfig>("SHOW_DETAILS");

        List<ShowConfig> showConfigs = null;

        while (!search.IsDone)
        {
            showConfigs = await search.GetNextSetAsync();
        }

        foreach (var showConfig in showConfigs)
        {
            await _dbContext.DeleteAsync(showConfig);
        }

        Console.WriteLine($"Successfully deleted");
    }
    
    public async Task DeleteQrProfileRecords(string gbsCode)
    {
        var search = _dbContext.QueryAsync<QrProfileDocument>(gbsCode);

        List<QrProfileDocument> qrProfileDocuments = null;

        while (!search.IsDone)
        {
            qrProfileDocuments = await search.GetNextSetAsync();
        }
        
        Console.WriteLine($"Fetched {qrProfileDocuments.Count} qr profile documents");

        foreach (var showConfig in qrProfileDocuments)
        {
            Console.WriteLine($"Deleting {showConfig.GroupId} - {showConfig.CustomSortKey}");
            await _dbContext.DeleteAsync(showConfig);
        }

        Console.WriteLine($"Successfully deleted");
    }
    
    public async Task DeleteShowRecords()
    {
        var search = _dbContext.QueryAsync<ShowItem>("shows-detail");

        List<ShowItem> qrProfileDocuments = null;

        while (!search.IsDone)
        {
            qrProfileDocuments = await search.GetNextSetAsync();
        }
        
        Console.WriteLine($"Fetched {qrProfileDocuments.Count} shows");

        foreach (var showConfig in qrProfileDocuments)
        {
            Console.WriteLine($"Deleting {showConfig.PartitionKey} - {showConfig.EventEditionId}");
            await _dbContext.DeleteAsync(showConfig);
        }

        Console.WriteLine($"Successfully deleted");
    }

    public async Task GetShowConfig()
    {
        var search = _dbContext.QueryAsync<ShowConfig>("SHOW_DETAILS");
        List<ShowConfig> showConfigs = null;

        while (!search.IsDone)
        {
            showConfigs = await search.GetNextSetAsync();
        }

        showConfigs.ForEach(x => x.PrintEventEditionId());
    }

    public async Task GetQrProfile(string gbsCode)
    {
        var search = _dbContext.QueryAsync<QrProfileDocument>(gbsCode);
        List<QrProfileDocument> qrProfileDocuments = null;

        while (!search.IsDone)
        {
            qrProfileDocuments = await search.GetNextSetAsync();
        }

        qrProfileDocuments[0].PrintGbsCodeAndShortCode();
    }
}