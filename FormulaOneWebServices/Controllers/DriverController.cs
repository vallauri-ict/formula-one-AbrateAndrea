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
        

        //GET: api/driver/id/1
        [HttpGet("id/{id}")]
        public Driver Get(int id)
        {
            DbTools db = new DbTools();
            return db.GetDriver(id);
        }

        //GET: api/driver/number/44
        //[HttpGet("number/{number:int}")]
        //public Driver Get(int number)
        //{
        //    DbTools db = new DbTools();
        //    return db.GetDriverNumber(number);
        //}

        //GET: api/driver/name/Lewis Hamilton
        [HttpGet("name/{name}")]
        public Driver Get(string name)
        {
            DbTools db = new DbTools();
            return db.GetDriverName(name);
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