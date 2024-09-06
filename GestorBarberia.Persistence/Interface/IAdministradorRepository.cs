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
    public interface IAdministradorRepository : IBaseRepository<Administrador>
    {

        AdministradorModel GetAdministrador(string name,string password);
        AdministradorModel GetAdministradorName(string name);   

    }
}
