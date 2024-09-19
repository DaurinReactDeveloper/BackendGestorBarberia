using GestorBarberia.Domain.Entities;
using GestorBarberia.Domain.Repository;
using GestorBarberia.Infrastructure.Exceptions;
using GestorBarberia.Infrastructure.Extension;
using GestorBarberia.Infrastructure.Models;
using GestorBarberia.Persistence.Context;
using GestorBarberia.Persistence.Core;
using GestorBarberia.Persistence.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Persistence.Repositories
{
    public class AdministradorRepository : BaseRepository<Administradores>, IAdministradorRepository
    {

        private readonly DbContextBarberia dbContextBarberia;
        private readonly ILogger<AdministradorRepository> logger;

        public AdministradorRepository(DbContextBarberia dbContextBarberia, ILogger<AdministradorRepository> logger): base(dbContextBarberia)
        {

            this.dbContextBarberia = dbContextBarberia;
            this.logger = logger;

        }

        public AdministradorModel GetAdministrador(string name, string password)
        {

            try
            {
                var AdmModel = (from ad in this.dbContextBarberia.Administradores
                             where ad.Nombre == name && ad.Password == password
                             select new AdministradorModel()
                             {
                                 Nombre = name,
                                 Password = password

                             }).FirstOrDefault();

                return AdmModel;

            }

            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error obteniendo el barbero: {ex.ToString()}.");
                throw new AdministradorExceptions("Ha ocurrido un error obteniendo el Administrador.");
            }
        }

        public AdministradorModel GetAdministradorName(string name)
        {
            try
            {
                var model = (from ad in this.dbContextBarberia.Administradores
                             where ad.Nombre.Equals(name)
                             select new AdministradorModel()
                             {
                                 Nombre = ad.Nombre,
                                 Password = ad.Password
                             }).FirstOrDefault();

                return model;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error obteniendo el administrador: {ex.ToString()}.");
                throw new BarberoExceptions("Ha ocurrido un error obteniendo el administrador.");
            }
        }
    }
}
