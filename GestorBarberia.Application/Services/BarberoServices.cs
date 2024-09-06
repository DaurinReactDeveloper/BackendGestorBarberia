using GestorBarberia.Application.Contract;
using GestorBarberia.Application.Core;
using GestorBarberia.Application.Dtos.BarberoDto;
using GestorBarberia.Application.Validations;
using GestorBarberia.Domain.Entities;
using GestorBarberia.Persistence.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Services
{
    public class BarberoServices : IBarberoServices
    {

        private readonly IBarberoRepository BarberoRepository;
        private readonly ILogger<BarberoServices> logger;

        public BarberoServices(IBarberoRepository BarberoRepository, ILogger<BarberoServices> logger)
        {
            this.BarberoRepository = BarberoRepository;
            this.logger = logger;
        }

        public ServiceResult GetBarbero(string name, string password)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var getBarbero = this.BarberoRepository.GetBarberoName(name);

                if (getBarbero is null)
                {
                    result.Success = false;
                    result.Message = "Nombre de Barbero Incorrecto";
                    return result;
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(password, getBarbero.Password);

                if (!isPasswordValid)
                {
                    result.Success = false;
                    result.Message = "Contraseña incorrecta";
                    return result;
                }

                result.Data = getBarbero;
                result.Message = "Se ha encontrado correctamente el barbero";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el barbero";
                this.logger.LogError($"Ha ocurrido un error obteniendo el barbero: {ex.Message}");
            }

            return result;
        }

        public ServiceResult GetBarberos()
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var barberos = this.BarberoRepository.GetBarberos();
                result.Data = barberos;
                result.Message = "Barberos Obtenidos Correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo los barberos";
                this.logger.LogError($"Ha ocurrido un EError obteniendo los barberos: {ex.Message}");
            }

            return result;
        }

        public ServiceResult GetBarberobyId(int id)
        {

            ServiceResult result = new ServiceResult();

            try
            {

                var BarberoId = this.BarberoRepository.GetById(id);

                if (BarberoId is null)
                {

                    result.Success = false;
                    result.Message = "No se pudo obtener el id del barbero";
                    return result;

                }

                result.Data = BarberoId;
                result.Message = "Barbero Obtenido Correctamente";
            }

            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el barbero";
                this.logger.LogError($"Ha ocurrido un error actualizando el barbero: {ex.Message}");

            }

            return result;
        }

        public ServiceResult Add(BarberoAddDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                if (!BarberiaValidations.ValidationsBarberia(modelDto))
                {
                    result.Success = false;
                    result.Message = "Los campos para agregar un barbero NO cumplen con las validaciones establecidas";
                    return result;
                }

                if (this.BarberoRepository.VerifyNameBarbero(modelDto.Nombre))
                {
                    result.Success = false;
                    result.Message = "Ya existe un barbero con ese nombre";
                    return result;
                }

                //string imagePath = ImageServices.SaveImage(modelDto.Imgbarbero, modelDto.Nombre,"barberos");

                string PasswordHashed = BCrypt.Net.BCrypt.EnhancedHashPassword(modelDto.Password);

                this.BarberoRepository.Add(new Barbero()
                {
                    BarberoId = modelDto.BarberoId,
                    Nombre = modelDto.Nombre,
                    Telefono = modelDto.Telefono,
                    Email = modelDto.Email,
                    Password = PasswordHashed,
                    Imgbarbero = modelDto.Imgbarbero, 
                });

                result.Message = "Se ha Agregado Correctamente el Barbero";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el barbero";
                this.logger.LogError($"Ha ocurrido un error guardando el barbero en barberoService + {ex.Message}");
            }

            return result;
        }

        public ServiceResult Update(BarberoUpdateDto modelDto)
        {

            ServiceResult result = new ServiceResult();

            try
            {

                var BarberoExistente = this.BarberoRepository.GetById(modelDto.BarberoId);

                if (!BarberiaValidations.ValidationsBarberia(modelDto))
                {
                    result.Success = false;
                    result.Message = "Los campos para actualizar un barbero NO cumplen con las validaciones establecidas";
                    return result;
                }

                BarberoExistente.BarberoId = modelDto.BarberoId;
                BarberoExistente.Nombre = modelDto.Nombre;
                BarberoExistente.Telefono = modelDto.Telefono;
                BarberoExistente.Email = modelDto.Email;
                BarberoExistente.Password = modelDto.Password;
                BarberoExistente.Imgbarbero = modelDto.Imgbarbero;

                this.BarberoRepository.Update(BarberoExistente);
                this.BarberoRepository.SaveChanged();
                result.Message = "Barbero Actualizado Correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el Barbero";
                this.logger.LogError($"Ha ocurrido un error actualizando el Barbero: {ex.Message}");
            }

            return result;
        }

        public ServiceResult Remove(BarberoRemoveDto modelDto)
        {

            ServiceResult result = new ServiceResult();

            try
            {

                var BarberoId = this.BarberoRepository.GetById(modelDto.BarberoId); 


                if (BarberoId is null)
                {
                    result.Success = false;
                    result.Message = "Ha ocurrido un error obteniendo el id del barbero";
                    return result;
                }

                this.BarberoRepository.Remove(BarberoId);
                this.BarberoRepository.SaveChanged();
                result.Message = "Barbero Removido Correctamente";
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el barbero";
                this.logger.LogError($"Ha ocurrido un error eliminando el barbero: {ex.Message}");

            }

            return result;
        }

    }
}
