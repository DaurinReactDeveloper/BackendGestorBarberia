using GestorBarberia.Domain.Entities;
using GestorBarberia.Persistence.Context;
using GestorBarberia.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Test.Capa_de_Persistencia
{
    public class AdministradorRepositoryTests
    {
        private readonly Mock<DbContextBarberia> _mockDbContext;
        private readonly Mock<ILogger<AdministradorRepository>> _mockLogger;
        private readonly AdministradorRepository _repository;

        public AdministradorRepositoryTests()
        {
            // Inicializa los mocks
            _mockDbContext = new Mock<DbContextBarberia>();
            _mockLogger = new Mock<ILogger<AdministradorRepository>>();

            // Crea la instancia del repositorio
            _repository = new AdministradorRepository(_mockDbContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetAdministrador_AdministradorExistente_RetornaAdministradorModel()
        {
            // Arrange
            var nombre = "admin";
            var password = "password";
            var administradores = new List<Administradores>
        {
            new Administradores { Nombre = nombre, Password = password }
        }.AsQueryable();

            var mockSet = new Mock<DbSet<Administradores>>();
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.Provider).Returns(administradores.Provider);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.Expression).Returns(administradores.Expression);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.ElementType).Returns(administradores.ElementType);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.GetEnumerator()).Returns(administradores.GetEnumerator());

            _mockDbContext.Setup(c => c.Administradores).Returns(mockSet.Object);

            // Act
            var result = _repository.GetAdministrador(nombre, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nombre, result.Nombre);
            Assert.Equal(password, result.Password);
        }

        [Fact]
        public void GetAdministrador_AdministradorNoExistente_RetornaNull()
        {
            // Arrange
            var nombre = "admin";
            var password = "wrongpassword";
            var administradores = new List<Administradores>().AsQueryable();

            var mockSet = new Mock<DbSet<Administradores>>();
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.Provider).Returns(administradores.Provider);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.Expression).Returns(administradores.Expression);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.ElementType).Returns(administradores.ElementType);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.GetEnumerator()).Returns(administradores.GetEnumerator());

            _mockDbContext.Setup(c => c.Administradores).Returns(mockSet.Object);

            // Act
            var result = _repository.GetAdministrador(nombre, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAdministradorName_AdministradorExistente_RetornaAdministradorModel()
        {
            // Arrange
            var nombre = "admin";
            var administradores = new List<Administradores>
        {
            new Administradores { Nombre = nombre, Password = "password" }
        }.AsQueryable();

            var mockSet = new Mock<DbSet<Administradores>>();
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.Provider).Returns(administradores.Provider);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.Expression).Returns(administradores.Expression);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.ElementType).Returns(administradores.ElementType);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.GetEnumerator()).Returns(administradores.GetEnumerator());

            _mockDbContext.Setup(c => c.Administradores).Returns(mockSet.Object);

            // Act
            var result = _repository.GetAdministradorName(nombre);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nombre, result.Nombre);
        }

        [Fact]
        public void GetAdministradorName_AdministradorNoExistente_RetornaNull()
        {
            // Arrange
            var nombre = "nonexistent";
            var administradores = new List<Administradores>().AsQueryable();

            var mockSet = new Mock<DbSet<Administradores>>();
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.Provider).Returns(administradores.Provider);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.Expression).Returns(administradores.Expression);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.ElementType).Returns(administradores.ElementType);
            mockSet.As<IQueryable<Administradores>>().Setup(m => m.GetEnumerator()).Returns(administradores.GetEnumerator());

            _mockDbContext.Setup(c => c.Administradores).Returns(mockSet.Object);

            // Act
            var result = _repository.GetAdministradorName(nombre);

            // Assert
            Assert.Null(result);
        }
    }
}
