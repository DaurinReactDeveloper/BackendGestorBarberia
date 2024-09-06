using GestorBarberia.Application.Contract;
using GestorBarberia.Application.Core;
using GestorBarberia.Persistence.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GestorBarberia.Application.Services
{
    public class AdministradorServices : IAdministradorServices
    {

        private readonly IAdministradorRepository administradorRepository;
        private readonly ILogger<AdministradorServices> logger;
       
        public AdministradorServices(IAdministradorRepository administradorRepository, ILogger<AdministradorServices> logger)
        {
            this.administradorRepository = administradorRepository;
            this.logger = logger;
        }

        public ServiceResult GetAdministrador(string nombre, string password)
        {

            ServiceResult result = new ServiceResult();

            try
            {

                //Conseguir la password del Administrador por nombre
                var getAdministradorName = this.administradorRepository.GetAdministradorName(nombre);

                if (getAdministradorName is null)
                {
                    result.Success = false;
                    result.Message = "Nombre del Administrador Incorrecto";
                    return result;
                }

                //Decodificar Token
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, getAdministradorName.Password);

                if (!isPasswordValid)
                {
                    result.Success = false;
                    result.Message = "Contraseña incorrecta";
                    return result;
                }

                result.Data = getAdministradorName;
                result.Message= "Se ha encontrado correctamente el administrador";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el administrador";
                this.logger.LogError($"Ha ocurrido un error obteniendo el administrador: {ex.Message}");
            }

            return result;

        }
    }
}
