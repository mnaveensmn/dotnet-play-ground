using webapi_play_ground.Exceptions;

namespace webapi_play_ground.Services;

public class Service : IService
{
    public void show()
    {
        throw new ResourceNotFoundException("Resource Not Found Exception");
    }
}