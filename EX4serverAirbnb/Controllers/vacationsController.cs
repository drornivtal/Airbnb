using HW4_EX3_Server.BL;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW4_EX3_Server.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class vacationsController : ControllerBase
    {
        // GET: api/<vacationsController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Vacation> tempVacList = Vacation.Read();
            if (tempVacList.Count == 0)
                return NotFound("Sorry, the list of vacations is empty!");
            else
                return Ok(tempVacList);
        }

        //[HttpGet("GetByDates/startDate/{startDate}/endDate/{endDate}")]

        //public IActionResult GetByDates(DateTime startDate, DateTime endDate)
        //{
        //    List<Vacation> tempVacList = Vacation.ReadByDates(startDate, endDate);
        //    if (tempVacList.Count == 0)
        //        return NotFound("Sorry, no vacations found between the dates, try other dates!");
        //    else
        //        return Ok(tempVacList);
        //}

        // GET api/<vacationsController>/5

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<vacationsController>
        [HttpPost]
        public IActionResult Post([FromBody] Vacation vacation)
        {
             int res= vacation.Insert();
            if(res == 0)
            {
                return NotFound("The insert new flat failed, try again!");
            }
            else return Ok(res);
        }

        // PUT api/<vacationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<vacationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}