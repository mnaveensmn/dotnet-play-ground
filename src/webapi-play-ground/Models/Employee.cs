using Newtonsoft.Json;

namespace webapi_play_ground.Models;

public class Employee
{
    public string id;
    public string name;
    public int age;
    [JsonIgnore]
    public string salary;
}