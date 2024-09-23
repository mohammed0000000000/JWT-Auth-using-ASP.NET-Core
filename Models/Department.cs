using System.Text.Json.Serialization;

namespace WebAPIDemo.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Employee> Employees { get; set; }
    }
}
