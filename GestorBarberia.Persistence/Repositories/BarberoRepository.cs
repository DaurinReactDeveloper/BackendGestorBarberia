using GestorBarberia.Domain.Entities;
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
    public class BarberoRepository : BaseRepository<Barberos>, IBarberoRepository
    {
        private readonly DbContextBarberia dbContextBarberia;
        private readonly ILogger<BarberoRepository> logger;

        public BarberoRepository(DbContextBarberia dbContextBarberia, ILogger<BarberoRepository> logger) : base(dbContextBarberia)
        {
            this.logger = logger;
            this.dbContextBarberia = dbContextBarberia;
        }

        public BarberoModel GetBarberoName(string name)
        {
            try
            {
                var model = (from ba in this.dbContextBarberia.Barberos
                             where ba.Nombre.Equals(name)
                             select new BarberoModel()
                             {
                                 BarberoId = ba.BarberoId,
                                 Nombre = ba.Nombre,
                                 Password = ba.Password
                             }).FirstOrDefault();

                return model;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error obteniendo el barbero: {ex.ToString()}.");
                throw new BarberoExceptions("Ha ocurrido un error obteniendo el barbero.");
            }
        }

        public BarberoModel GetBarbero(string name, string password)
        {

            try
            {

                 var barberoModel = (from ba in this.dbContextBarberia.Barberos
                             where ba.Nombre.Equals(name) && ba.Password.Equals(password)
                             select new BarberoModel()
                             {
                                 BarberoId = ba.BarberoId,
                                 Nombre = name,
                                 Password = password

                             }).FirstOrDefault();

                return barberoModel;

            }

            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error obteniendo el barbero: {ex.ToString()}.");
                throw new BarberoExceptions("Ha ocurrido un error obteniendo el barbero.");
            }

        }

        public List<BarberoModel> GetBarberos()
        {

            List<BarberoModel> barberoModels = new List<BarberoModel>();

            try
            {

                barberoModels = base.GetEntities()
                                .Select(br => br.ConverteBarberoToModel())
                                .ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error obteniendo los barberos : {ex.ToString()}.");
                throw new BarberoExceptions("Ha ocurrido un error obteniendo los barbero.");
            }

            return barberoModels;

        }

        public bool VerifyNameBarbero(string nameBarbero)
        {
            try
            {
                var nameData = (from n in this.dbContextBarberia.Barberos
                                where n.Nombre.Equals(nameBarbero)
                                select n).FirstOrDefault();

                return nameData != null;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error verificando el nombre del barbero: {ex.ToString()}.");
                throw new BarberoExceptions("Ha ocurrido un error verificando el nombre del barbero.");
            }
        }

        public override void Add(Barberos entity)
        {
            base.Add(entity);
            base.SaveChanged();
        }

        public override void Update(Barberos entity)
        {

            try
            {

                Barberos barberoUpdate = base.GetById(entity.BarberoId);

                if (barberoUpdate is null)
                {
                    throw new BarberoExceptions("Ha ocurrido un error obteniendo el Id del Barbero.");
                }

                barberoUpdate.Nombre = entity.Nombre;
                barberoUpdate.Telefono = entity.Telefono;
                barberoUpdate.Email = entity.Email;
                barberoUpdate.Password = entity.Password;
                barberoUpdate.Imgbarbero = entity.Imgbarbero;

                base.Update(barberoUpdate);
                base.SaveChanged();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error actualizando el barbero: {ex.ToString()}.");
            }

        }

        public override void Remove(Barberos entity)
        {

            try
            {

                Barberos barberoRemove = this.GetById(entity.BarberoId);

                if (barberoRemove is null)
                {

                    throw new BarberoExceptions("Ha ocurrido un error obteniendo el Id del Barbero.");
                }

                base.Remove(barberoRemove);
                base.SaveChanged();

            }

            catch (Exception ex)
            {

                this.logger.LogError($"Ha ocurrido un error elimiando el barbero: {ex.ToString()}.");

            }
        }

    }
}
