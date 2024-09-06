using GestorBarberia.Application.Core;
using GestorBarberia.Application.Dtos.BarberoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Contract
{
    public interface IBarberoServices : IBaseServices<BarberoAddDto, BarberoRemoveDto, BarberoUpdateDto> 
    {

        ServiceResult GetBarberos();
        ServiceResult GetBarberobyId(int id);
        ServiceResult GetBarbero(string name, string password);
    }
}