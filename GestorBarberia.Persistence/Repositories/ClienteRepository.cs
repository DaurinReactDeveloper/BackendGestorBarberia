using GestorBarberia.Domain.Entities;
using GestorBarberia.Infrastructure.Exceptions;
using GestorBarberia.Infrastructure.Extension;
using GestorBarberia.Infrastructure.Models;
using GestorBarberia.Persistence.Context;
using GestorBarberia.Persistence.Core;
using GestorBarberia.Persistence.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Persistence.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {

        private readonly DbContextBarberia dbContextBarberia;
        private readonly ILogger<ClienteRepository> logger;

        public ClienteRepository(DbContextBarberia dbContextBarberia, ILogger<ClienteRepository> logger) : base(dbContextBarberia)
        {
            this.dbContextBarberia = dbContextBarberia;
            this.logger = logger;
        }

        public List<ClienteModel> GetClientes()
        {

            List<ClienteModel> clienteModels = new List<ClienteModel>();

            try
            {

                clienteModels = base.GetEntities()
                                .Select(cm => cm.ConvertClienteToModel())
                                .ToList();
            }

            catch (Exception ex)

            {
                this.logger.LogError($"Ha ocurrido un error listando los clientes: {ex.ToString()}");
            }

            return clienteModels;

        }

        public ClienteModel GetClienteName(string name)
        {
            try
            {
                var model = (from cl in this.dbContextBarberia.Clientes
                             where cl.Nombre.Equals(name)
                             select new ClienteModel()
                             {
                                 ClienteId = cl.ClienteId,
                                 Nombre = cl.Nombre,
                                 Password = cl.Password

                             }).FirstOrDefault();

                return model;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error obteniendo el cliente: {ex.Message}");
                throw new ClienteExceptions("Ha ocurrido un error obteniendo el cliente");
            }
        }

        public ClienteModel GetCliente(string name, string password)
        {

                try
                {
                var model = (from cli in this.dbContextBarberia.Clientes
                             where cli.Nombre.Equals(name) && cli.Password.Equals(password)
                             select new ClienteModel()
                             {
                                 ClienteId = cli.ClienteId,
                                 Nombre = name,
                                 Password = password

                             }).FirstOrDefault();

                return model;

            }

                catch (Exception ex)
                {
                    this.logger.LogError($"Ha ocurrido un error obteniendo el barbero: {ex.Message}");
                    throw new ClienteExceptions("Ha ocurrido un error obteniendo el cliente");
                }
        }

        public override void Add(Cliente entity)
        {
            base.Add(entity);
            base.SaveChanged();
        }

        public override void Update(Cliente entity)
        {
            try
            {

                Cliente clienteEntity = this.GetById(entity.ClienteId);

                if (clienteEntity is null)
                {
                    throw new ClienteExceptions("Ha ocurrido un error obteniendo el Id del cliente");
                }

                clienteEntity.ClienteId = entity.ClienteId;
                clienteEntity.Nombre = entity.Nombre;
                clienteEntity.Email = entity.Email;
                clienteEntity.Telefono = entity.Telefono;
                clienteEntity.Password = entity.Password;
                clienteEntity.Imgcliente = entity.Imgcliente;

                base.Update(clienteEntity);
                base.SaveChanged();
            }

            catch (Exception ex)
            {

                this.logger.LogError($"Ha ocurrido un error actualizando el cliente: {ex.ToString()}");

            }

        }

        public override void Remove(Cliente entity)
        {

            try
            {

                Cliente clienteRemove = this.GetById(entity.ClienteId);

                if(clienteRemove is null)
                {
                    throw new ClienteExceptions("Ha ocurrido un error obteniendo el Id del cliente");
                }

                base.Remove(clienteRemove);
                base.SaveChanged(); 

            }

            catch (Exception ex)
            {

                this.logger.LogError($"Ha ocurrido un error eliminando el cliente: {ex.ToString()}");

            }

        }

        public bool VerifyNameCliente(string nameCliente)
        {
            try
            {
                // Verificar si ya existe un cliente con el nombre proporcionado
                var nameData = (from n in this.dbContextBarberia.Clientes
                                where n.Nombre.Equals(nameCliente)
                                select n).FirstOrDefault();

                return nameData != null;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Ha ocurrido un error verificando el nombre del cliente: {ex.Message}");
                throw new ClienteExceptions("Ha ocurrido un error verificando el nombre del cliente");
            }
        }

    }

}

