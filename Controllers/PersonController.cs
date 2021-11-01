using dapper.DAL.Interfaces;
using dapper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonDAL _personDAL;

        public PersonController(IPersonDAL personDAL)
        {
            _personDAL = personDAL;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _personDAL.GetAllPerson();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            var result = _personDAL.NewPerson(person);
            if (!result)
                return StatusCode(500);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            var result = _personDAL.EditPerson(person);
            if (!result)
                return StatusCode(500);

            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var result = _personDAL.Delete(id);
            if (!result)
                return StatusCode(500);

            return Ok();
        }

    }
}
