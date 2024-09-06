using GestorBarberia.Domain.Entities;
using GestorBarberia.Domain.Repository;
using GestorBarberia.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Persistence.Interface
{
    public interface ICitaRepository : IBaseRepository<Cita>
    {
        List<CitaModel> GetCitaByBarberoId(int barberoId);
        List<CitaModel> GetCitaByClienteId(int clienteId);
        void UpdateEstado(Cita citaUpdate); 
    }
}
