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

        // GET: api/team/id
        [HttpGet("id/{id}")]
        public Team Get(int id)
        {
            DbTools db = new DbTools();
            return db.GetTeam(id);
        }

        // GET: api/team/name/Mercedes-AMG Petronas F1 Team
        [HttpGet("name/{name}")]
        public Team Get(string name)
        {
            DbTools db = new DbTools();
            return db.GetTeamName(name);
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