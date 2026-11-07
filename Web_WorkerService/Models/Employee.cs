namespace Web_WorkerService.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Salary { get; set; }
        public bool? IsProcessed { get; set; }
    }
}