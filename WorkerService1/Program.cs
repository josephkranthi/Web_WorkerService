using Microsoft.EntityFrameworkCore;
using WorkerService1;
using WorkerService1.Models;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString("EmployeeDB")));

var host = builder.Build();
host.Run();
