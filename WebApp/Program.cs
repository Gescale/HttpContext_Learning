using System.Text.Json;
using WebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    if (context.Request.Path.StartsWithSegments("/"))
    {
        //
        if (context.Request.Method == "GET")
        {
            // Return this response for the root path
            await context.Response.WriteAsync($"Method is: {context.Request.Method}\r\n");
            await context.Response.WriteAsync($"The url is: {context.Request.Path}\r\n");

            await context.Response.WriteAsync("Headers :\r\n");
            foreach (var key in context.Request.Headers.Keys)
            {
                await context.Response.WriteAsync($"{key} : {context.Request.Headers[key]}\r\n");
            }
        }
    }

    else if (context.Request.Path.StartsWithSegments("/employees"))
    {
        //
        if (context.Request.Method == "GET")
        {
            if (context.Request.Query.ContainsKey("id"))
            {
                var id = context.Request.Query["id"].ToString();
                var employee = EmployeesRepository.GetEmployeeById(id);

                if (employee != null)
                    await context.Response.WriteAsync($"Id: {employee.Id}, Name: {employee.Name}, Position: {employee.Position}, Salary: {employee.Salary}\r\n");
                else
                    await context.Response.WriteAsync($"Employee does not exist!");
            }

            else
            {
                // return this response for the /employees path
                //await context.Response.WriteAsync("Employees endpoint");
                var employees = EmployeesRepository.GetAllEmployees();

                foreach (var employee in employees)
                {
                    await context.Response.WriteAsync($"Id: {employee.Id}, Name: {employee.Name}, Position: {employee.Position}, Salary: {employee.Salary}\r\n");
                }
            }
        }

        else if (context.Request.Method == "POST")
        {
            // Handle POST request for /employees path
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var employee = JsonSerializer.Deserialize<Employee>(body);

            if (employee != null)
            {
                //Add the new employee to the repository
                EmployeesRepository.AddEmployee(employee);
            }

        }
    }
});

//app.Run(async (HttpContext context) =>
//{
//    // Testing the Quesry string access
//    // visit link (http://localhost:5118/employees?id=3&name=gescale&position=developer) to test
//    foreach (var key in context.Request.Query.Keys)
//    {
//        await context.Response.WriteAsync($"{key} : {context.Request.Query[key]}\r\n");
//    }
//});

app.Run();
