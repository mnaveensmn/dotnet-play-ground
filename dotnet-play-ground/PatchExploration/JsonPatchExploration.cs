using Microsoft.AspNetCore.JsonPatch;
namespace dotnet_play_ground.PatchExploration;

public class JsonPatchExploration
{
    public void patch()
    {
        Model model = new()
        {
            empId = "emp1001",
            name = "Tony",
            age = 30
        };

        JsonPatchDocument jsonPatchDocument = new();
        
        
        Model model2 = model;

    }
}