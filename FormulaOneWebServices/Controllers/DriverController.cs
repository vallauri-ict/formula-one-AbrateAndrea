using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FormulaOneDLL;

namespace FormulaOneWebServices
{
    [Route("api/driver")]
    [ApiController]
    public class driverController : ControllerBase
    {
        // GET: api/driver
        [HttpGet]
        public IEnumerable<Driver> Get()
        {
            DbTools db = new DbTools();
            return db.GetDriversObj();
        }

        //GET: api/driver/1
        [HttpGet("{id}")]
        public Driver Get(string id)
        {
            DbTools db = new DbTools();
            return db.GetDriver(id);
        }

        //GET: api/driver/number/44
        [HttpGet("number/{number}")]
        public Driver Get(int number)
        {
            DbTools db = new DbTools();
            return db.GetDriverNumber(number);
        }

        // POST: api/driver
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/driver/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}