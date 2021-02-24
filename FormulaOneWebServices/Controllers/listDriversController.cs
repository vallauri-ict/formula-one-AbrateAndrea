using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormulaOneDLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FormulaOneWebServices.Controllers
{
    [Route("api/listDrivers")]
    [ApiController]
    public class listDriversController : ControllerBase
    {
        // GET: api/listDrivers
        [HttpGet]
        public List<DriverListDTO> Get()
        {
            DbTools db = new DbTools();
            return db.GetDriversList();
        }

        // GET api/listDrivers/44
        [HttpGet("{number}")]
        public DriverDetailsDTO Get(int number)
        {
            DbTools db = new DbTools();
            return db.getDriverDetails(number);
        }

        // POST api/<listDrivers>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<listDrivers>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<listDrivers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
