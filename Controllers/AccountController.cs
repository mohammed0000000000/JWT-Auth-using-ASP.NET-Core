using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIDemo.Data;
using WebAPIDemo.DTO;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration configuration ){
            this.userManager = userManager; 
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUser registerUser) {

            if(ModelState.IsValid){
                var res = await userManager.CreateAsync(new ApplicationUser() {
                    UserName = registerUser.UserName,
                    PasswordHash = registerUser.Password,
                    Email = registerUser.Email,
                }, registerUser.Password);
                if(res.Succeeded){
                    return Ok("Created");
                }
                foreach(var error in res.Errors){
                    ModelState.AddModelError("error",error.Description);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUser loginUser){
            if(ModelState.IsValid){
                var res = await userManager.FindByNameAsync(loginUser.UserName);
                if(res is null)return BadRequest("User Name is Invalid");
                var checkPassword = await userManager.CheckPasswordAsync(res, loginUser.Password);
                if (checkPassword is true){
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, res.Id));
                    userClaims.Add(new Claim(ClaimTypes.Name, res.UserName));

                    var userRoles = await userManager.GetRolesAsync(res);

                    if(userRoles is not null){
                        foreach(var role in userRoles){
                            userClaims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }
                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrityKey"]));
                    var signInCredentialList = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);
                    var userToken = new JwtSecurityToken(
                    issuer: configuration["JWT:IssuerIp"],
                    audience: configuration["JWT:AudienceIP"],
                    expires: DateTime.Now.AddHours(1),
                    claims: userClaims,
                    signingCredentials: signInCredentialList
                    );
                    return Ok(new { 
                        token = new JwtSecurityTokenHandler().WriteToken(userToken),
                        ExpireIn = DateTime.Now.AddHours(1)
                    });
                }
                ModelState.AddModelError("credentional", "UserName or Password is not valid");
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout() {
            return BadRequest();
        }
    }
}
