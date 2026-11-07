using Microsoft.EntityFrameworkCore;

namespace Web_WorkerService.Models;

public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
    : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");
            entity.HasKey(employee => employee.Id);
            entity.Property(employee => employee.IsProcessed).HasDefaultValue(false);
            entity.Property(employee => employee.Name).HasMaxLength(100);
            entity.Property(employee => employee.Salary).HasColumnType("decimal(18, 2)");
        });
    }
}
