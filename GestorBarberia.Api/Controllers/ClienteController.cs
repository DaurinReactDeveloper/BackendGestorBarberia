using GestorBarberia.Application.Contract;
using GestorBarberia.Application.Dtos.ClienteDto;
using GestorBarberia.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorBarberia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServices clienteServices;
        private readonly IConfiguration configuration;

        public ClienteController(IClienteServices clienteServices, IConfiguration configuration)
        {
            this.clienteServices = clienteServices;
            this.configuration = configuration;
        }

        // GET: api/<BarberoController>
        [HttpGet("GetCliente/{name}/{password}")]
        public IActionResult GetCliente(string name, string password)
        {

            var result = this.clienteServices.GetCliente(name, password);
            var token = TokenServices.GenerateToken(name, "cliente", configuration);

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

        // GET: api/<ClienteController>
        [Authorize(Roles = "admin,barbero")]
        [HttpGet("GetClientes")]
        public IActionResult GetClientes()
        {

            var result = this.clienteServices.GetClientes();


            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);
        }

        // GET api/<ClienteController>/5
        [Authorize(Roles = "barbero,cliente,admin")]
        [HttpGet("ClienteById/{id}")]
        public IActionResult ClienteById(int id)
        {

            var result = this.clienteServices. GetClienteById(id);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);
        }

        // POST api/<ClienteController>
        [HttpPost("Save")]
        public IActionResult Post([FromBody] ClienteAddDto modelDto)
        {

            var result = this.clienteServices.Add(modelDto);

            if (result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // PUT api/<ClienteController>/5
        [Authorize(Roles = "admin")]
        [HttpPut("Update")]
        public IActionResult Put([FromBody] ClienteUpdateDto modelDto)
        {

            var result = this.clienteServices.Update(modelDto);

            if(result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);

        }

        // DELETE api/<ClienteController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("Remove")]
        public IActionResult Delete(ClienteRemoveDto modelDto)
        {

            var result = this.clienteServices.Remove(modelDto);

            if(result is null)
            {

                return BadRequest(result);

            }

            return Ok(result);
        }
    }
}
