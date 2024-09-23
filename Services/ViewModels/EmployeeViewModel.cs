using System.ComponentModel.DataAnnotations;
using WebAPIDemo.Models;

namespace WebAPIDemo.Services.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1000,50000)]
        public int Salary { get; set; }
        public string? Address { get; set; }
        [Range(18, 50)]
        public int Age { get; set; }

        public int? DeptId{  get; set; } 
        public Department? Department { get; set; }  
    }
}
