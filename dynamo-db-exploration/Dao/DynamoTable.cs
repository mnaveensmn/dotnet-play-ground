using Amazon.DynamoDBv2.DataModel;
using dynamo_db_exploration.Shared;

namespace dynamo_db_exploration.Dao;

[DynamoDBTable(Constant.TableName)]
public class DynamoTable
{
    [DynamoDBHashKey(Constant.ColumnOne)]
    public string ColumnOne { get; set; }
    
    [DynamoDBRangeKey(Constant.ColumnTwo)] 
    public string ColumnTwo { get; set; }
}

