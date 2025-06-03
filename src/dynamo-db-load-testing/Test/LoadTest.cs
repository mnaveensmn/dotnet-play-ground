using System.Diagnostics;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using dynamo_db_load_testing.Entities;
using dynamo_db_load_testing.Shared;
using dynamo_db_load_testing.Utilities;

namespace dynamo_db_load_testing.Test;

public class LoadTest(string threadNo) : BaseLoadTest
{
    private IDynamoDBContext _dynamoDbContext = new DynamoDBContext(
        new AmazonDynamoDBClient(new AmazonDynamoDBConfig()),
        new DynamoDBContextConfig
        {
            TableNamePrefix = $"perf-",
        });

    private CircularList<SomeFeed> exhibitors = GetExhibitorsFeed();

    public override async Task TestCallBack()
    {
        var error = "";
        var exhibitorId = exhibitors.GetNext().someId;
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        try
        {
            await QueryAsync<SomeItem>(exhibitorId, QueryOperator.BeginsWith,
                new List<string> { Constants.SomeIdentifier });
        }
        catch (Exception e)
        {
            error = e.Message;
            Console.WriteLine(e);
        }

        stopWatch.Stop();
        var logMsg =
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff},{threadNo},{exhibitorId},{stopWatch.ElapsedMilliseconds},{error}";
        Console.WriteLine(logMsg);
    }

    private static CircularList<SomeFeed> GetExhibitorsFeed()
    {
        var filePath = "Feeds/SomeFeeds.json";

        var exhibitors = new FeedsReader<SomeFeed>().Read(filePath);

        return new CircularList<SomeFeed>(exhibitors);
    }

    public async Task TestQueryAsyncMethod()
    {
        var exhibitorId = exhibitors.GetNext().someId;

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var resultFromQueryAsync = await QueryAsync<SomeItem>(exhibitorId, QueryOperator.BeginsWith,
            new List<string> { Constants.SomeIdentifier });
        stopWatch.Stop();
        var QueryAsyncElapsed = stopWatch.ElapsedMilliseconds;
        
        stopWatch.Restart();
        var resultFromQueryAsyncWithLoop = await QueryAsyncWithLoop<SomeItem>(exhibitorId, QueryOperator.BeginsWith,
            new List<string> { Constants.SomeIdentifier });
        stopWatch.Stop();
        Console.WriteLine($"{QueryAsyncElapsed},{stopWatch.ElapsedMilliseconds}");
        //Console.WriteLine($"QueryAsync: {QueryAsyncElapsed}, Items Fetched: {resultFromQueryAsync.Count}\tQueryAsyncWithLoop: {stopWatch.ElapsedMilliseconds}, Items Fetched: {resultFromQueryAsyncWithLoop.Count}");
    }

    public async Task<List<T>> QueryAsync<T>(object hashKeyValue, QueryOperator queryOperator,
        IEnumerable<object> values)
    {
        var asyncSearch = _dynamoDbContext.QueryAsync<T>(hashKeyValue, queryOperator, values);
        return await asyncSearch.GetRemainingAsync();
    }

    public async Task<List<T>> QueryAsyncWithLoop<T>(object hashKeyValue, QueryOperator queryOperator,
        IEnumerable<object> values)
    {
        var asyncSearch = _dynamoDbContext.QueryAsync<T>(hashKeyValue, queryOperator, values);
        var list = new List<T>();
        while (!asyncSearch.IsDone)
        {
            list.AddRange(await asyncSearch.GetNextSetAsync());
        }

        return list;
    }
}