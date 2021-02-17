using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FormulaOneDLL;

namespace FormulaOneWebServices
{
    [Route("api/country")]
    [ApiController]
    public class countryController : ControllerBase
    {
        // GET: api/country
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            DbTools db = new DbTools();
            return db.GetCountriesObj();
        }

        // GET: api/country/it
        [HttpGet("{isocode}")]
        public Country Get(string isoCode)
        {
            DbTools db = new DbTools();
            return db.GetCountry(isoCode);
        }

        // POST: api/country
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/country/5
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