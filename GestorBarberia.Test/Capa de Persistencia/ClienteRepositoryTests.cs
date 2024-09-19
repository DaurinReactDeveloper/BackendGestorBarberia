using GestorBarberia.Domain.Entities;
using GestorBarberia.Infrastructure.Exceptions;
using GestorBarberia.Infrastructure.Models;
using GestorBarberia.Persistence.Context;
using GestorBarberia.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GestorBarberia.Test.Capa_de_Persistencia
{
    public class ClienteRepositoryTests
    {
        private readonly Mock<DbContextBarberia> _mockDbContext;
        private readonly Mock<ILogger<ClienteRepository>> _mockLogger;
        private readonly ClienteRepository _repository;

        public ClienteRepositoryTests()
        {
            // Inicializa los mocks
            _mockDbContext = new Mock<DbContextBarberia>();
            _mockLogger = new Mock<ILogger<ClienteRepository>>();

            // Crea la instancia del repositorio
            _repository = new ClienteRepository(_mockDbContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetClienteName_ClienteExistente_RetornaClienteModel()
        {
            // Arrange
            var nombre = "Cliente1";
            var cliente = new Clientes { ClienteId = 1, Nombre = nombre, Password = "password" };
            var clientes = new List<Clientes> { cliente }.AsQueryable();

            var mockSet = new Mock<DbSet<Clientes>>();
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Provider).Returns(clientes.Provider);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Expression).Returns(clientes.Expression);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.ElementType).Returns(clientes.ElementType);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.GetEnumerator()).Returns(clientes.GetEnumerator());

            _mockDbContext.Setup(c => c.Clientes).Returns(mockSet.Object);

            // Act
            var result = _repository.GetClienteName(nombre);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nombre, result.Nombre);
        }

        [Fact]
        public void GetClienteName_ClienteNoExistente_RetornaNull()
        {
            // Arrange
            var nombre = "ClienteNoExistente";
            var clientes = new List<Clientes>().AsQueryable();

            var mockSet = new Mock<DbSet<Clientes>>();
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Provider).Returns(clientes.Provider);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Expression).Returns(clientes.Expression);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.ElementType).Returns(clientes.ElementType);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.GetEnumerator()).Returns(clientes.GetEnumerator());

            _mockDbContext.Setup(c => c.Clientes).Returns(mockSet.Object);

            // Act
            var result = _repository.GetClienteName(nombre);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCliente_ClienteExistente_RetornaClienteModel()
        {
            // Arrange
            var nombre = "Cliente1";
            var password = "password";
            var cliente = new Clientes { ClienteId = 1, Nombre = nombre, Password = password };
            var clientes = new List<Clientes> { cliente }.AsQueryable();

            var mockSet = new Mock<DbSet<Clientes>>();
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Provider).Returns(clientes.Provider);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Expression).Returns(clientes.Expression);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.ElementType).Returns(clientes.ElementType);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.GetEnumerator()).Returns(clientes.GetEnumerator());

            _mockDbContext.Setup(c => c.Clientes).Returns(mockSet.Object);

            // Act
            var result = _repository.GetCliente(nombre, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nombre, result.Nombre);
            Assert.Equal(password, result.Password);
        }

        [Fact]
        public void GetCliente_ClienteNoExistente_RetornaNull()
        {
            // Arrange
            var nombre = "Cliente1";
            var password = "wrongpassword";
            var clientes = new List<Clientes>().AsQueryable();

            var mockSet = new Mock<DbSet<Clientes>>();
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Provider).Returns(clientes.Provider);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Expression).Returns(clientes.Expression);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.ElementType).Returns(clientes.ElementType);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.GetEnumerator()).Returns(clientes.GetEnumerator());

            _mockDbContext.Setup(c => c.Clientes).Returns(mockSet.Object);

            // Act
            var result = _repository.GetCliente(nombre, password);

            // Assert}
            Assert.Null(result);
        }

        [Fact]
        public void VerifyNameCliente_ClienteExistente_RetornaTrue()
        {
            // Arrange
            var nombre = "Cliente1";
            var cliente = new Clientes { ClienteId = 1, Nombre = nombre };
            var clientes = new List<Clientes> { cliente }.AsQueryable();

            var mockSet = new Mock<DbSet<Clientes>>();
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Provider).Returns(clientes.Provider);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Expression).Returns(clientes.Expression);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.ElementType).Returns(clientes.ElementType);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.GetEnumerator()).Returns(clientes.GetEnumerator());

            _mockDbContext.Setup(c => c.Clientes).Returns(mockSet.Object);

            // Act
            var result = _repository.VerifyNameCliente(nombre);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyNameCliente_ClienteNoExistente_RetornaFalse()
        {
            // Arrange
            var nombre = "ClienteNoExistente";
            var clientes = new List<Clientes>().AsQueryable();

            var mockSet = new Mock<DbSet<Clientes>>();
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Provider).Returns(clientes.Provider);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.Expression).Returns(clientes.Expression);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.ElementType).Returns(clientes.ElementType);
            mockSet.As<IQueryable<Clientes>>().Setup(m => m.GetEnumerator()).Returns(clientes.GetEnumerator());

            _mockDbContext.Setup(c => c.Clientes).Returns(mockSet.Object);

            // Act
            var result = _repository.VerifyNameCliente(nombre);

            // Assert
            Assert.False(result);
        }
    }
}
