using System.Runtime.Serialization;

namespace dynamo_db_load_testing.Shared;

public static class Constants
{
    public const string TableName = "some-table";
    public const string SomeIdentifier = "some";
}

public static class Attributes
{
    public const string PartitionKey = "partition_key";
    public const string SortKey = "sort_key";
    public const string SomeProperty = "some_property";
}