using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo.Data;
using WebAPIDemo.DTO;
using WebAPIDemo.Services.Contracts;
using WebAPIDemo.Services.ViewModels;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]//api/Employee/action
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService services;

        public EmployeeController(IEmployeeService services) {
            this.services = services;
        }


        [HttpGet("{id:int}", Name = "EmployeeDetailRoute")]
        //[Route("api/Employee/{id}")] equavalent
        //[Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id) {
            var res = await services.GetById(id); 
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetEmployee() {
            //var context = new AppDbContext();
            var res = await services.GetAll();
            //var res = context.Employees.ToList();
            return Ok(res);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]EmployeeViewModel employee) {
            if(ModelState.IsValid){
                employee.Id = id;
                var res = await services.UpdateEmp(employee);
                if(res){
                    return Ok("model update successfully");
                }
                return BadRequest("Error when updata");
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeViewModel employee) {
            if (ModelState.IsValid) {
                var res = await services.Create(employee);
                if (res != null) {
                    var url = Url.Link("EmployeeDetailRoute", new { id = res.Id });
                    return Created($"{url}", res);
                }
                return BadRequest("Error when create");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id) {
            try {
                var res = await services.DeleteById(id);
                if (res) { 
                    return StatusCode(StatusCodes.Status200OK);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet("Dept/{id:int}")]
        public IActionResult GetEmpDept(int id) {
            var context = new AppDbContext();
            //var emp = context.Employees.Where(emp => emp.Id == id).Select(x => new { Id = x.Id, Name = x.Name, Address = x.Address, Salary = x.Salary, Age = x.Age, DepartmentName = x.Department.Name }).Take(1);
            var emp = context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
            var res = new EmployeeDeptDTO(){ Id = emp.Id, Name = emp.Name, Salary = emp.Salary, DeptName = emp.Department.Name };
            return Ok(res);
        }

    }
}
