using System.ComponentModel.DataAnnotations;

namespace MyCrudApp.MvcApp
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Salary { get; set; } = string.Empty;
    }
}
