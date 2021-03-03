using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormulaOneDLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FormulaOneWebServices.Controllers
{
    [Route("api/listTeams")]
    [ApiController]
    public class listTeamsController : ControllerBase
    {
        // GET: api/listTeams
        [HttpGet]
        public List<TeamListDTO> Get()
        {
            DbTools db = new DbTools();
            return db.GetTeamsList();
        }

        //GET api/listTeams/1
        [HttpGet("{id}")]
        public TeamDetailsDTO Get(int id)
        {
            DbTools db = new DbTools();
            return db.GetTeamsDetails(id);
        }

        // POST api/<listTeamsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<listTeamsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<listTeamsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
