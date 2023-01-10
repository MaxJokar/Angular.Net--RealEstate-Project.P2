using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;// were able to save & get records from Db



        //to comminucatie with Db , inject DB context class 
        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext; // first endpoint
        }
        [HttpPost("authenticate")] // first Endpoint :helps to Login

        public async Task<IActionResult> Authenticate([FromBody] User userObj) // user will be sending a body a login Obj:userobject
        {
            if (userObj == null) // were checking the object 
                return BadRequest();// if the user send blank 
            // check if User exist or Not & match with name  & password or Not 
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username && x.Password == userObj.Password);
            if (user == null)
                return NotFound(new { Message = "User Not Found" });
            //if there is a match
            return Ok(new
            {
                message = "Login Sucess!"
            });
        }


        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody] User userObj) // user will be sending a body a login Obj
        {
            if (userObj == null)
                return BadRequest();// if the user send blank 

            await _appDbContext.Users.AddAsync(userObj);
            await _appDbContext.SaveChangesAsync();


            return Ok(new
            {
                message = "Login Sucess!"
            });




        }
    }
}
