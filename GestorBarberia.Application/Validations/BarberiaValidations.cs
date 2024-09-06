using GestorBarberia.Application.Dtos.BarberoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Validations
{
    public static class BarberiaValidations
    {

        public static bool ValidationsBarberia(BarberoDto barberoDto)
        {

            //El nombre debe ser mayor a 5 y menor a 49
            //El Telefono debe ser mayor a 5 y menor a 20
            //El Email debe ser mayor a 5 y menor a 99
            //La Contraseña debe ser mayor a 5 y menor a 69
            //La imagen debe ser mayor a 5 y menor a 254

            if (barberoDto.Nombre.Length >= 29 || barberoDto.Nombre.Length <= 5
                || barberoDto.Telefono.Length >= 20 || barberoDto.Telefono.Length <= 5
                || barberoDto.Email.Length >= 49 || barberoDto.Email.Length <= 5
                || barberoDto.Password.Length >= 99 || barberoDto.Password.Length <= 5 /*hashing*/ 
                || barberoDto.Imgbarbero.Length >= 254 || barberoDto.Imgbarbero.Length <= 5
                )
            {

                return false;

            }

            if (string.IsNullOrEmpty(barberoDto.Nombre)  || string.IsNullOrWhiteSpace(barberoDto.Nombre)
             || string.IsNullOrEmpty(barberoDto.Telefono) || string.IsNullOrWhiteSpace(barberoDto.Telefono)
             || string.IsNullOrEmpty(barberoDto.Email) || string.IsNullOrWhiteSpace(barberoDto.Email)
             || string.IsNullOrEmpty(barberoDto.Password) || string.IsNullOrWhiteSpace(barberoDto.Password)
             || string.IsNullOrEmpty(barberoDto.Imgbarbero) || string.IsNullOrWhiteSpace(barberoDto.Imgbarbero)) 
            {

                return false;

            }

            return true;

        }

    }
}
