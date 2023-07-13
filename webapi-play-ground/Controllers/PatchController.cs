using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using webapi_play_ground.Exceptions;
using webapi_play_ground.Models;

namespace webapi_play_ground.Controllers;

[ApiController]
[Route("[controller]")]
public class PatchController
{
    [HttpPatch(Name = "Patch Demo")]
    public Employee Get(JsonPatchDocument employeePatch)
    {
        var employee = createEmployee();
        employeePatch.ApplyTo(employee);
        return employee;
    }

    private Employee createEmployee()
    {
        if (true)
        {
            throw new DynamoDBException("Error while saving data");
        }
        return new Employee()
        {
            name = "Stark",
            age = 25
        };
    }
}

/*
Request Body:

[
    {
        "path": "/name",
        "op": "add",
        "value": "Tony"
    },
    {
        "path": "/empId",
        "op": "add",
        "value": "emp1001"
    },
    {
        "path": "/age",
        "op": "add",
        "value": "30"
    }
]
*/