using GestorBarberia.Application.Contract;
using GestorBarberia.Application.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorBarberia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {

        private readonly IAdministradorServices administradorServices;
        private readonly IConfiguration configuration;

        public AdministradorController(IAdministradorServices administradorServices, IConfiguration configuration)
        {
            this.administradorServices = administradorServices;
            this.configuration = configuration;
        }

        // GET: api/<AdministradorController>
        [HttpGet("GetAdministrador/{name}/{password}")]
        public IActionResult GetAdministrador(string name, string password)
        {
            var result = this.administradorServices.GetAdministrador(name, password);

            if (result.Success == false)
            {
                return BadRequest(result);
            }

            var token = TokenServices.GenerateToken(name, "admin", configuration);

            return Ok(new
            {
                Data = result,
                Token = token
            });
        }
    }
}
