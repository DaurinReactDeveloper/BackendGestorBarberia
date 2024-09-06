using GestorBarberia.Domain.Entities;
using GestorBarberia.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Infrastructure.Extension
{
    public static class AdministradorExtension
    {

        public static AdministradorModel ConvertAdministradorToModel(this Administrador administradorEntity)
        {

          var administradorModel = new AdministradorModel()
            {

                AdministradorID = administradorEntity.AdministradorID,
                Email = administradorEntity.Email,
                Nombre = administradorEntity.Nombre,
                Password = administradorEntity.Password,
                Telefono = administradorEntity.Telefono,

            };
            
            return administradorModel;
        }


    }
}
