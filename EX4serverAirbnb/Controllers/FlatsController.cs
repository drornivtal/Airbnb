using HW4_EX3_Server.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW4_EX3_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {

        // GET: api/<FlatsController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Flat> tempFlatList = new List<Flat>();
            tempFlatList = Flat.Read();

            if (tempFlatList.Count == 0)
                return NotFound("Sorry, the list of flats is empty!");
            else
                return Ok(tempFlatList);
        }

        //[HttpGet("search")]
        //public IActionResult GetCityflats(string city, double price)
        //{
        //    List<Flat> tempFlatList = new List<Flat>();
        //    tempFlatList = Flat.ReadCityPrice(city, price);

        //    if (tempFlatList.Count == 0)
        //        return NotFound("Sorry, no flats in this city and below this price!");
        //    else
        //        return Ok(tempFlatList);
        //}


        // GET api/<FlatsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FlatsController>
        [HttpPost]
        public IActionResult Post([FromBody] Flat flat)
        {           
            int res= flat.Insert();
            if (res == 0)
            {
                return NotFound("The insert new flat failed, try again!");
            }
            else 
                return Ok(res);
        }

        // PUT api/<FlatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}