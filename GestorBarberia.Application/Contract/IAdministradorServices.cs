using GestorBarberia.Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Contract
{
    public interface IAdministradorServices
    {

        ServiceResult GetAdministrador(string nombre, string password);

    }
}
