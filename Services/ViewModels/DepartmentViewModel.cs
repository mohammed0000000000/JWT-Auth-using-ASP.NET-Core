using System.ComponentModel.DataAnnotations;
using WebAPIDemo.Models;

namespace WebAPIDemo.Services.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
