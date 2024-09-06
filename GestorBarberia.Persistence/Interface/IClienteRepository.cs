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
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        List<ClienteModel> GetClientes();

        ClienteModel GetCliente(string name, string password);
        ClienteModel GetClienteName(string name);

        // Verificar el Barbero
        bool VerifyNameCliente(string nameCliente);
    }
}
