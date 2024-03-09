using Microsoft.AspNetCore.Mvc;
using HW4_EX3_Server.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW4_EX3_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            User user = new User();
            return user.Read();
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {         
            int numEffected = user.Insert();
            if (numEffected == 0)
            {
                return NotFound("EROR!");
            }
            else
            return Ok(numEffected);
        }

        [HttpPost]
        [Route ("LogIn")]
        public IActionResult LogIn([FromBody] User user)
        {
            User resUser= new User();
            resUser = user.LogIn();

            if (resUser.Email == null)
            {
                return NotFound("The user is not registered in the system,try again!");
            }
            else
                return Ok(resUser);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            int res = user.UpdateUser();
            if (res == 0)
            {
                return NotFound("Failed to update details,Try again!");
            }
            else 
                return Ok(res);
        }

    

        // PUT api/<UsersController>/5
        [HttpPut]
        [Route("UpdateActivityId")]
        public IActionResult UpdateActivity(string userId)
        {
            User user = new User();
            int res = user.UpdateUserActivity(userId);
            if (res == 0)
            {
                return NotFound("Failed to update details,Try again!");
            }
            else
                return Ok(res);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("month")]
        public Object Get(int month)
        {
            User user = new User();
            return user.GetAvregePerNightForCity(month);
        }
    }
}
