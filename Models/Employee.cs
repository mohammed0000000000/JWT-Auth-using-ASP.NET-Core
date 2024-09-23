using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public string? Address { get; set; }
        public int Age{ get; set; } 
        public int? DeptId { get; set; } 
        public Department? Department{ get; set; } 
    }
}
