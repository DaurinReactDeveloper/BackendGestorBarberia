using GestorBarberia.Application.Contract;
using GestorBarberia.Application.Dtos.ClienteDto;
using GestorBarberia.Application.Dtos.EstilodecorteDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorBarberia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstilosdecorteController : ControllerBase
    {

        private readonly IEstilosdecorteServices estiloServices;

        public EstilosdecorteController(IEstilosdecorteServices estiloServices)
        {
            this.estiloServices = estiloServices;
        }

        // GET: api/<ClienteController>
        [HttpGet("GetEstilos")]
        public IActionResult Get()
        {

            var result = this.estiloServices.GetEstilos();


            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);
        }

        // GET api/<ClienteController>/5
        [Authorize(Roles = "admin, cliente")]
        [HttpGet("Estilos/{id}")]
        public IActionResult Get(int id)
        {

            var result = this.estiloServices.GetEstilosById(id);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);
        }

        // POST api/<ClienteController>
        [Authorize(Roles = "admin")]
        [HttpPost("Save")]
        public IActionResult Post([FromBody] EstilosdecorteAddDto modelDto)
        {

            var result = this.estiloServices.Add(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }
        
        [Authorize(Roles = "admin")]
        // PUT api/<ClienteController>/5
        [HttpPut("Update")]
        public IActionResult Put([FromBody] EstilosdecorteUpdateDto modelDto)
        {

            var result = this.estiloServices.Update(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // DELETE api/<ClienteController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("Remove")]
        public IActionResult Delete(EstilosdecorteRemoveDto modelDto)
        {

            var result = this.estiloServices.Remove(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);
        }
    }
}
