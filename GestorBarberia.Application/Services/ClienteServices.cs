using GestorBarberia.Application.Contract;
using GestorBarberia.Application.Core;
using GestorBarberia.Application.Dtos.ClienteDto;
using GestorBarberia.Application.Validations;
using GestorBarberia.Domain.Entities;
using GestorBarberia.Infrastructure.Exceptions;
using GestorBarberia.Persistence.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Services
{
    public class ClienteServices : IClienteServices
    {

        private readonly IClienteRepository clienteRepository;
        private readonly ILogger<ClienteServices> logger;

        public ClienteServices(IClienteRepository clienteRepository, ILogger<ClienteServices> logger)
        {
            this.clienteRepository = clienteRepository;
            this.logger = logger;
        }

        public ServiceResult GetCliente(string name, string password)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var getCliente = this.clienteRepository.GetClienteName(name);   

                if (getCliente is null)
                {

                    result.Success = false;
                    result.Message = "Nombre de Usuario Incorrecto";
                    return result;

                }

                bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(password, getCliente.Password);

                if (!isPasswordValid)
                {
                    result.Success = false;
                    result.Message = "Contraseña incorrecta";
                    return result;
                }

                result.Data = getCliente;
                result.Message = "Se ha encontrado correctamente el cliente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el cliente";
                this.logger.LogError($"Ha ocurrido un error obteniendo el cliente: {ex.Message}");
            }

            return result;


        }

        public ServiceResult GetClientes()
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var clientes = this.clienteRepository.GetClientes();
                result.Data = clientes;
                result.Message = "Clientes Obtenidos Correctamente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los clientes";
                this.logger.LogError($"Ha ocurrido un error obteniendo los clientes: {ex.Message}");
            }

            return result;

        }

        public ServiceResult GetClienteById(int id)
        {

            ServiceResult result = new ServiceResult();

            try
            {
                var clienteid = this.clienteRepository.GetById(id);

                if (clienteid is null)
                {
                    result.Success = false;
                    result.Message = "Ha ocurrido un error obteniendo el id del cliente";
                    return result;
                }

                result.Data = clienteid;
                result.Message = "Cliente Obtenido Correctamente";

            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el cliente";
                this.logger.LogError($"Ha ocurrido un error obteniendo el cliente: {ex.Message}");
            }

            return result;

        }

        public ServiceResult Add(ClienteAddDto modelDto)
        {

            ServiceResult result = new ServiceResult();


            if (!ClienteValidations.ValidationsCliente(modelDto))
            {
                result.Success = false;
                result.Message = "Los campos para agregar una cita NO cumplen con las validaciones establecidas";
                return result;
            }

            if (this.clienteRepository.VerifyNameCliente(modelDto.Nombre))
            {
                result.Success = false;
                result.Message = "Ya existe un cliente con ese nombre";
                return result;
            }

            string PasswordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(modelDto.Password);

            try
            {

                this.clienteRepository.Add(new Cliente()
                {
                    ClienteId = modelDto.ClienteId,
                    Email = modelDto.Email,
                    Nombre = modelDto.Nombre,
                    Telefono = modelDto.Telefono,
                    Password = PasswordHashed,
                    Imgcliente = modelDto.Imgcliente,
                    
                });

                this.clienteRepository.SaveChanged();
                result.Message = "Cliente Agregado Correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el cliente";
                this.logger.LogError($"Ha ocurrido un error guardando el cliente: {ex.Message}");
            }

            return result;

        }

        public ServiceResult Update(ClienteUpdateDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var clienteUpdate = this.clienteRepository.GetById(modelDto.ClienteId);

                if (!ClienteValidations.ValidationsCliente(modelDto))
                {
                    result.Success = false;
                    result.Message = "Los campos para actualizar un cliente NO cumplen con las validaciones establecidas";
                    return result;
                }

                clienteUpdate.ClienteId = modelDto.ClienteId;
                clienteUpdate.Nombre = modelDto.Nombre;
                clienteUpdate.Email = modelDto.Email;
                clienteUpdate.Telefono = modelDto.Telefono;
                clienteUpdate.Password = modelDto.Password;
                clienteUpdate.Imgcliente = modelDto.Imgcliente;

                this.clienteRepository.Update(clienteUpdate);
                this.clienteRepository.SaveChanged();
                result.Message = "Cliente Actualizado Correctamente";

            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el cliente";
                this.logger.LogError($"Ha ocurrido un error actualizando el cliente: {ex.Message}");
            }

            return result;
        }

        public ServiceResult Remove(ClienteRemoveDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var clienteid = this.clienteRepository.GetById(modelDto.ClienteId);

                if (clienteid is null)
                {
                    result.Success = false;
                    result.Message = "Ha ocurrido un error obteniendo el id del cliente";
                    return result;
                }

                this.clienteRepository.Remove(clienteid);
                this.clienteRepository.SaveChanged();
                result.Message = "Cliente Removido Correctamente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el cliente";
                this.logger.LogError($"Ha ocurrido un error eliminando el cliente: {ex.Message}");
            }

            return result;

        }

    }
}
