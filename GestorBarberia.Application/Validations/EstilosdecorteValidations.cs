using GestorBarberia.Application.Dtos.EstilodecorteDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Validations
{
    public static class EstilosdecorteValidations
    {

        public static bool ValidationsEstilosdecorte(EstilosdecorteDto estilosdecorteDto)
        {

            if (string.IsNullOrEmpty(estilosdecorteDto.Nombre) || string.IsNullOrWhiteSpace(estilosdecorteDto.Nombre)
             || string.IsNullOrEmpty(estilosdecorteDto.Descripcion) || string.IsNullOrWhiteSpace(estilosdecorteDto.Descripcion)
             || string.IsNullOrEmpty(estilosdecorteDto.Imgestilo) || string.IsNullOrWhiteSpace(estilosdecorteDto.Imgestilo))

            {

                return false;

            }


            if (estilosdecorteDto.Nombre.Length >= 30 || estilosdecorteDto.Nombre.Length <= 5
             || estilosdecorteDto.Imgestilo.Length >= 255 || estilosdecorteDto.Imgestilo.Length <= 5 || estilosdecorteDto.Precio >= 1000
             || estilosdecorteDto.Descripcion.Length >= 54 || estilosdecorteDto.Descripcion.Length <= 5)
            {

                return false;

            }


            if (estilosdecorteDto.EstiloId == null || estilosdecorteDto.EstiloId <= 0
                || estilosdecorteDto.Precio == null || estilosdecorteDto.Precio <= 0)
            {

                return false;

            }

            return true;

        }


    }
}
