using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Services.ViewModels;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindController : ControllerBase
    {
        // Binding Primitive type
        [HttpGet]
        [Route("{id:int}/{name:alpha}")]
        public IActionResult Get1([FromRoute]int id,[FromRoute]string name) {
            return Ok(new { identifier = id, Name = name });
        }
        [HttpPost]  
        public IActionResult Add([FromBody]EmployeeViewModel employee){
            return Ok(new { message = "Added Employee Successfully" });
        }
        [HttpGet]
        public IActionResult Get2([FromBody] EmployeeViewModel employee) {
            return Ok(employee);
        }
    }
}
