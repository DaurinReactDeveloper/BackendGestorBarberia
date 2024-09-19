using GestorBarberia.Domain.Entities;
using GestorBarberia.Domain.Repository;
using GestorBarberia.Infrastructure.Models;
using GestorBarberia.Persistence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Persistence.Interface
{
    public interface IBarberoRepository : IBaseRepository<Barberos>
    {

        List<BarberoModel> GetBarberos();

        //Login Barbero
        BarberoModel GetBarbero(string name,string password);
        BarberoModel GetBarberoName(string name);

        // Verificar el Barbero
        bool VerifyNameBarbero(string nameBarbero);

    }
}
