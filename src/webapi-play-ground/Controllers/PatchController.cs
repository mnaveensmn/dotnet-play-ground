using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using webapi_play_ground.Exceptions;
using webapi_play_ground.Models;

namespace webapi_play_ground.Controllers;

[ApiController]
[Route("[controller]")]
public class PatchController : ControllerBase
{
    [HttpPatch(Name = "Patch Demo")]
    public Employee Get(JsonPatchDocument employeePatch)
    {
        var employee = createEmployee();
        employeePatch.ApplyTo(employee);
        return employee;
    }

    [HttpPost(Name = "Json Ignore Demo")]
    public Employee Post([FromBody] Employee employee)
    {
        Console.WriteLine($"Employee Salary {employee.salary}");
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
 Request Body for Post
 {
  "id": "ss",
  "name":"ss",
  "age": 10,
  "salary": "100"
}

 */
/*
Request Body for Patch
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