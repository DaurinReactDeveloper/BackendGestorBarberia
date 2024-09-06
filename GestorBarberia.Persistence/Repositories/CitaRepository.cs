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
    public class CitaRepository : BaseRepository<Cita>, ICitaRepository
    {

        private readonly DbContextBarberia dbContextBarberia;
        private readonly ILogger<CitaRepository> logger;

        public CitaRepository(DbContextBarberia dbContextBarberia, ILogger<CitaRepository> logger) : base(dbContextBarberia)
        {
            this.dbContextBarberia = dbContextBarberia; 
            this.logger = logger;
        }

        //Metodo para el cliente
        public List<CitaModel> GetCitaByBarberoId(int barberoId)
        {
            try
            {
                var listCita = (from c in this.dbContextBarberia.Citas
                                join b in dbContextBarberia.Barberos on c.BarberoId equals b.BarberoId into bj
                                from b in bj.DefaultIfEmpty()
                                join e in dbContextBarberia.Estilosdecortes on c.EstiloId equals e.EstiloId into es
                                from e in es.DefaultIfEmpty()
                                join cl in dbContextBarberia.Clientes on c.ClienteId equals cl.ClienteId into clj
                                from cl in clj.DefaultIfEmpty()
                                where c.BarberoId.Equals(barberoId)
                                select new CitaModel()
                                {
                                    CitaId = c.CitaId,
                                    Fecha = c.Fecha,
                                    Hora = c.Hora,
                                    BarberoId = c.BarberoId,
                                    Estado = c.Estado,
                                    Barbero = b,
                                    Estilo = e,
                                    Cliente = cl,
                                }).OrderByDescending(c => c.Fecha).ToList();


                return listCita;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error listando las citas: {ex.ToString()}");
                throw new CitaExceptions("Ha ocurrido un error listando las citas");
            }
        }

        //Metodo para el barbero
        public List<CitaModel> GetCitaByClienteId(int clienteId)
        {
            try
            {
                var listCita = (from c in this.dbContextBarberia.Citas
                                join cl in dbContextBarberia.Clientes on c.ClienteId equals cl.ClienteId into cid
                                from cl in cid.DefaultIfEmpty()
                                join e in dbContextBarberia.Estilosdecortes on c.EstiloId equals e.EstiloId into es
                                from e in es.DefaultIfEmpty()
                                where c.ClienteId.Equals(clienteId)
                                select new CitaModel()
                                {
                                    CitaId = c.CitaId,
                                    Fecha = c.Fecha,
                                    Hora = c.Hora,
                                    BarberoId = c.BarberoId,
                                    Estado = c.Estado,
                                    Cliente = cl,
                                    Estilo = e
                                }).OrderByDescending(c => c.Fecha).ToList();

                return listCita;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error listando las citas: {ex.ToString()}");
                throw new CitaExceptions("Ha ocurrido un error listando las citas");
            }
        }

        public override void Add(Cita entity)
        {
            base.Add(entity);
            base.SaveChanged();
        }

        public override void Update(Cita entity)
        {

            try
            {
                Cita citaUpdate = this.GetById(entity.CitaId);

                if (citaUpdate is null)
                {

                    throw new CitaExceptions("Ha ocurrido un error obteniendo el Id de la Cita");

                }

                citaUpdate.Barbero = entity.Barbero;
                citaUpdate.Estilo = entity.Estilo;
                citaUpdate.Estado = entity.Estado;
                citaUpdate.Fecha = entity.Fecha;
                citaUpdate.BarberoId = entity.BarberoId;
                citaUpdate.EstiloId = entity.EstiloId;
                citaUpdate.CitaId = entity.CitaId;
                
                base.Update(citaUpdate);
                base.SaveChanged();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error actuaalizando las citas: {ex.ToString()}");
            }
        }

        //Actualizar el estado
        public void UpdateEstado(Cita citaUpdate)
        {
            try
            {
                Cita citaUpdateEstado = this.GetById(citaUpdate.CitaId);

                if (citaUpdateEstado is null)
                {
                    throw new CitaExceptions("Ha ocurrido un error obteniendo el Id de la Cita");
                }

                citaUpdateEstado.CitaId = citaUpdate.CitaId;
                citaUpdateEstado.Estado = citaUpdate.Estado;

                base.Update(citaUpdateEstado);
                base.SaveChanged();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error actuaalizando las citas: {ex.ToString()}");
            }
        }

        public override void Remove(Cita entity)
        {

            try
            {   
                
                Cita citaRemove = this.GetById(entity.CitaId);

                if (citaRemove is null)
                {

                    throw new CitaExceptions("Ha ocurrido un error obteniendo el Id de la Cita");

                }

                base.Remove(citaRemove);
                base.SaveChanged();

            }
            catch (Exception)
            {

                throw new CitaExceptions("Ha ocurrido un error removiendo la Cita");
            }


        }

    }
}
