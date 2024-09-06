using GestorBarberia.Application.Dtos.BarberoDto;
using GestorBarberia.Application.Dtos.CitaDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Validations
{
    public static class CitaValidations
    {

        public static bool ValidationsCita(CitaDto citaDto)
        {
            // Verificar si la fecha es nula
            if (citaDto.Fecha == null)
            {
                return false;
            }

            // Verificar si la fecha es anterior a la fecha actual
            if (citaDto.Fecha < DateTime.Now.Date)
            {
                return false;
            }

            // Verificar la hora
            var horaSeleccionada = citaDto.Hora;

            // Definir rango permitido de horas
            var horaMinima = new TimeSpan(9, 0, 0); // 09:00
            var horaMaxima = new TimeSpan(18, 0, 0); // 18:00

            // Verificar que la hora esté en el rango permitido
            if (horaSeleccionada < horaMinima || horaSeleccionada > horaMaxima)
            {
                return false;
            }

            // Verificar si CitaId, ClienteId o EstiloId son nulos
            if (citaDto.CitaId == null || citaDto.ClienteId == null || citaDto.EstiloId == null)
            {
                return false;
            }

            return true;
        }

    }
}
