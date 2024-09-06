using GestorBarberia.Application.Contract;
using GestorBarberia.Application.Dtos.BarberoDto;
using GestorBarberia.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorBarberia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberoController : ControllerBase
    {

        private readonly IBarberoServices barberoServices;
        private readonly IConfiguration configuration;

        public BarberoController(IBarberoServices barberoServices, IConfiguration configuration)
        {
            this.barberoServices = barberoServices;
            this.configuration = configuration;
        }

        // GET: api/<BarberoController>
        [HttpGet("GetBarbero/{name}/{password}")]
        public IActionResult GetBarbero(string name, string password)
        {
            var result = this.barberoServices.GetBarbero(name, password);
            var token = TokenServices.GenerateToken(name, "barbero", configuration);

            if (result.Success == false)
            {

                return BadRequest(result);

            }

            return Ok(new
            {
                Data = result,
                Token = token
            });
        }

        // GET: api/<BarberoController>
        //[Authorize(Roles = "cliente")]
        [HttpGet("GetBarberos")]
        public IActionResult GetBarberos()
        {

            var result = this.barberoServices.GetBarberos();

            if (result is null) {

                return BadRequest(result);

            }

            return Ok(result);
        }

        // GET api/<BarberoController>/5
        [Authorize(Roles = "cliente")]
        [HttpGet("BarberoById/{id}")]
        public IActionResult BarberoGetbyid(int id)
        {

            var result = this.barberoServices.GetBarberobyId(id);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // POST api/<BarberoController>
        //[Authorize(Roles = "admin")]
        [HttpPost("Save")]
        public IActionResult Post([FromBody] BarberoAddDto modelDto)
        {

            var result = this.barberoServices.Add(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // PUT api/<BarberoController>/5
        [Authorize(Roles = "admin")]
        [HttpPut("Update")]
        public IActionResult Put([FromBody] BarberoUpdateDto modelDto)
        {

            var result = this.barberoServices.Update(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        [Authorize(Roles = "admin")]
        // DELETE api/<BarberoController>/5
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] BarberoRemoveDto modelDto)
        {

            var result = this.barberoServices.Remove(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }
    }
}
