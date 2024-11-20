namespace webapi_play_ground.Exceptions;

public class DynamoDBException : Exception
{
    public DynamoDBException(string message): base(message)
    {
        
    }
}