using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FormulaOneDLL;

namespace FormulaOneWebServices
{
    [Route("api/team")]
    [ApiController]
    public class teamController : ControllerBase
    {
        // GET: api/team
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            DbTools db = new DbTools();
            return db.GetTeamsObj();
        }

        // GET: api/team/1
        [HttpGet("{id}")]
        public Team Get(string id)
        {
            DbTools db = new DbTools();
            return db.GetTeam(id);
        }

        // POST: api/team
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/team/5
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