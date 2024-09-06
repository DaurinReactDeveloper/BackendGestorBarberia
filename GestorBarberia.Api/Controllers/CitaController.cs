using GestorBarberia.Application.Contract;
using GestorBarberia.Application.Dtos.BarberoDto;
using GestorBarberia.Application.Dtos.CitaDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorBarberia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {

        private readonly ICitaServices Citaservices;

        public CitaController(ICitaServices Citaservices)
        {
            this.Citaservices = Citaservices;
        }

        // GET: api/<CitaController>
        [Authorize(Roles = "cliente")]
        [HttpGet("GetCitasByCliente/{clienteId}")]
        public IActionResult GetCitasByCliente(int clienteId)
        {

            var result = this.Citaservices.GetCitaByClienteId(clienteId);

            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // GET api/<CitaController>/5
        [Authorize(Roles = "admin,barbero")]
        [HttpGet("GetCitasByBarbero/{barberoId}")]
        public IActionResult GetCitasByBarbero(int barberoId)
        {

            var result = this.Citaservices.GetCitaByBarberoId(barberoId);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        //POST api/<CitaController>
        [Authorize(Roles = "cliente")]
        [HttpPost("Save")]
        public IActionResult Post([FromBody] CitaAddDto modelDto)
        {

            var result = this.Citaservices.Add(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // PUT api/<CitaController>/5
        [Authorize(Roles = "admin")]
        [HttpPut("Update")]
        public IActionResult Put([FromBody] CitaUpdateDto modelDto)
        {

            var result = this.Citaservices.Update(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        [Authorize(Roles = "barbero,cliente")]
        [HttpPut("UpdateEstado")]
        public IActionResult PutEstado([FromBody] CitaUpdateDto modelDto)
        {

            var result = this.Citaservices.UpdateEstado(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // DELETE api/<CitaController>/5
        [Authorize(Roles = "admin,cliente,barbero")]
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] CitaRemoveDto modelDto)
        {

            var result = this.Citaservices.Remove(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }
    }
}
