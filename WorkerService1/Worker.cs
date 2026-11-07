using Microsoft.EntityFrameworkCore;
using WorkerService1.Models;

public class Worker(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope =_serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

            var employee = await db.Employees
                .Where(x => x.IsProcessed == false)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(stoppingToken);

            if (employee != null)
            {
                employee.IsProcessed = true;

                await db.SaveChangesAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(20),stoppingToken);
        }
    }
}