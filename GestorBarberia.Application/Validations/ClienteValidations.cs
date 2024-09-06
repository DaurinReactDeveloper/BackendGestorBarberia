using GestorBarberia.Application.Dtos.ClienteDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Validations
{
    public static class ClienteValidations
    {

        public static bool ValidationsCliente(ClienteDto clienteDto)
        {

            if (string.IsNullOrWhiteSpace(clienteDto.Nombre) || string.IsNullOrEmpty(clienteDto.Nombre)
             || string.IsNullOrWhiteSpace(clienteDto.Telefono) || string.IsNullOrEmpty(clienteDto.Telefono)
             || string.IsNullOrWhiteSpace(clienteDto.Email) || string.IsNullOrEmpty(clienteDto.Email)
             || string.IsNullOrWhiteSpace(clienteDto.Password) || string.IsNullOrEmpty(clienteDto.Password)
             || string.IsNullOrWhiteSpace(clienteDto.Imgcliente) || string.IsNullOrEmpty(clienteDto.Imgcliente)
             )

            {

                return false;

            }

            if (clienteDto.Nombre.Length >= 29 || clienteDto.Nombre.Length <= 5
             || clienteDto.Telefono.Length >= 20 || clienteDto.Telefono.Length <= 5
             || clienteDto.Email.Length >= 49 || clienteDto.Email.Length <= 5
             || clienteDto.Password.Length >= 99 || clienteDto.Password.Length <= 5
             || clienteDto.Imgcliente.Length >= 254 || clienteDto.Imgcliente.Length <= 5)
            {

                return false;

            }

            return true;

        }

    }
}
