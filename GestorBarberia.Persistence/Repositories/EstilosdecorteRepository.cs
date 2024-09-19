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
    public class EstilosdecorteRepository : BaseRepository<Estilosdecortes>, IEstilosdecorteRepository 
    {

        private readonly DbContextBarberia dbContextBarberia;
        private readonly ILogger<EstilosdecorteRepository> logger;

        public EstilosdecorteRepository(DbContextBarberia dbContextBarberia, ILogger<EstilosdecorteRepository> logger):base(dbContextBarberia) 
        { 
            this.dbContextBarberia = dbContextBarberia;
            this.logger = logger;   
        }

        public List<EstilosdecorteModel> GetEstilosdecorte()
        {
            List<EstilosdecorteModel> EstilosModels = new List<EstilosdecorteModel>();

            try
            {

                EstilosModels = base.GetEntities()
                    .Select(estilos => estilos.ConvertEstilosToModel())
                    .ToList();
                
            }
            catch (Exception ex)
            {

                this.logger.LogError($"Ha ocurrido un error listando los estilos de corte: {ex.ToString()}.");
            }

            return EstilosModels;

        }

        public override void Add(Estilosdecortes entity)
        {
            base.Add(entity);
            base.SaveChanged();
        }

        public override void Update(Estilosdecortes entity)
        {
            try
            {
                Estilosdecortes EstiloCorteId = this.GetById(entity.EstiloId);

                if (EstiloCorteId is null)
                {
                    throw new EstilosdecorteExceptions("Ha ocurrido un error obteniendo el Id del estilo de corte.");
                }

                EstiloCorteId.Nombre = entity.Nombre;
                EstiloCorteId.Descripcion = entity.Descripcion;
                EstiloCorteId.Precio = entity.Precio;
                EstiloCorteId.Imgestilo = entity.Imgestilo;
                EstiloCorteId.EstiloId = entity.EstiloId;
                EstiloCorteId.Cita = entity.Cita;

                base.Update(EstiloCorteId);
                base.SaveChanged();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error actualizando el estilo de corte: {ex.ToString()}.");
            }

        }

        public override void Remove(Estilosdecortes entity)
        {

            try
            {
                Estilosdecortes EstiloCorteId = this.GetById(entity.EstiloId);

                if(EstiloCorteId is null)
                {
                    throw new EstilosdecorteExceptions("Ha ocurrido un error obteniendo el Id del estilo de corte.");
                }

                base.Remove(EstiloCorteId);
                base.SaveChanged();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error elimando el estilo de corte: {ex.ToString()}.");
            }

        }
    }
}

