using GestorBarberia.Domain.Entities;
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

    public class BarberoRepositoryTests
    {
        private readonly Mock<DbContextBarberia> _mockDbContext;
        private readonly Mock<ILogger<BarberoRepository>> _mockLogger;
        private readonly BarberoRepository _repository;

        public BarberoRepositoryTests()
        {
            // Inicializa los mocks
            _mockDbContext = new Mock<DbContextBarberia>();
            _mockLogger = new Mock<ILogger<BarberoRepository>>();

            // Crea la instancia del repositorio
            _repository = new BarberoRepository(_mockDbContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetBarberoName_BarberoExistente_RetornaBarberoModel()
        {
            // Arrange
            var nombre = "barbero1";
            var barberos = new List<Barbero>
        {
            new Barbero { BarberoId = 1, Nombre = nombre, Password = "password" }
        }.AsQueryable();

            var mockSet = new Mock<DbSet<Barbero>>();
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Provider).Returns(barberos.Provider);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Expression).Returns(barberos.Expression);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.ElementType).Returns(barberos.ElementType);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.GetEnumerator()).Returns(barberos.GetEnumerator());

            _mockDbContext.Setup(c => c.Barberos).Returns(mockSet.Object);

            // Act
            var result = _repository.GetBarberoName(nombre);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nombre, result.Nombre);
        }

        [Fact]
        public void GetBarberoName_BarberoNoExistente_RetornaNull()
        {
            // Arrange
            var nombre = "barberoNoExistente";
            var barberos = new List<Barbero>().AsQueryable();

            var mockSet = new Mock<DbSet<Barbero>>();
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Provider).Returns(barberos.Provider);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Expression).Returns(barberos.Expression);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.ElementType).Returns(barberos.ElementType);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.GetEnumerator()).Returns(barberos.GetEnumerator());

            _mockDbContext.Setup(c => c.Barberos).Returns(mockSet.Object);

            // Act
            var result = _repository.GetBarberoName(nombre);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetBarbero_BarberoExistente_RetornaBarberoModel()
        {
            // Arrange
            var nombre = "barbero1";
            var password = "password";
            var barberos = new List<Barbero>
        {
            new Barbero { BarberoId = 1, Nombre = nombre, Password = password }
        }.AsQueryable();

            var mockSet = new Mock<DbSet<Barbero>>();
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Provider).Returns(barberos.Provider);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Expression).Returns(barberos.Expression);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.ElementType).Returns(barberos.ElementType);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.GetEnumerator()).Returns(barberos.GetEnumerator());

            _mockDbContext.Setup(c => c.Barberos).Returns(mockSet.Object);

            // Act
            var result = _repository.GetBarbero(nombre, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nombre, result.Nombre);
            Assert.Equal(password, result.Password);
        }

        [Fact]
        public void GetBarbero_BarberoNoExistente_RetornaNull()
        {
            // Arrange
            var nombre = "barbero1";
            var password = "wrongpassword";
            var barberos = new List<Barbero>().AsQueryable();

            var mockSet = new Mock<DbSet<Barbero>>();
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Provider).Returns(barberos.Provider);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Expression).Returns(barberos.Expression);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.ElementType).Returns(barberos.ElementType);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.GetEnumerator()).Returns(barberos.GetEnumerator());

            _mockDbContext.Setup(c => c.Barberos).Returns(mockSet.Object);

            // Act
            var result = _repository.GetBarbero(nombre, password);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetBarberos_RetornaListaDeBarberos()
        {
            // Arrange
            var barberos = new List<Barbero>
        {
            new Barbero { BarberoId = 1, Nombre = "barbero1", Password = "password" },
            new Barbero { BarberoId = 2, Nombre = "barbero2", Password = "password2" }
        };

            var mockSet = new Mock<DbSet<Barbero>>();
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Provider).Returns(barberos.AsQueryable().Provider);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Expression).Returns(barberos.AsQueryable().Expression);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.ElementType).Returns(barberos.AsQueryable().ElementType);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.GetEnumerator()).Returns(barberos.AsQueryable().GetEnumerator());

            _mockDbContext.Setup(c => c.Barberos).Returns(mockSet.Object);

            // Act
            var result = _repository.GetBarberos();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void VerifyNameBarbero_BarberoExistente_RetornaTrue()
        {
            // Arrange
            var nombreBarbero = "barbero1";
            var barberos = new List<Barbero>
        {
            new Barbero { BarberoId = 1, Nombre = nombreBarbero }
        }.AsQueryable();

            var mockSet = new Mock<DbSet<Barbero>>();
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Provider).Returns(barberos.Provider);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Expression).Returns(barberos.Expression);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.ElementType).Returns(barberos.ElementType);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.GetEnumerator()).Returns(barberos.GetEnumerator());

            _mockDbContext.Setup(c => c.Barberos).Returns(mockSet.Object);

            // Act
            var result = _repository.VerifyNameBarbero(nombreBarbero);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyNameBarbero_BarberoNoExistente_RetornaFalse()
        {
            // Arrange
            var nombreBarbero = "barberoNoExistente";
            var barberos = new List<Barbero>().AsQueryable();

            var mockSet = new Mock<DbSet<Barbero>>();
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Provider).Returns(barberos.Provider);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.Expression).Returns(barberos.Expression);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.ElementType).Returns(barberos.ElementType);
            mockSet.As<IQueryable<Barbero>>().Setup(m => m.GetEnumerator()).Returns(barberos.GetEnumerator());

            _mockDbContext.Setup(c => c.Barberos).Returns(mockSet.Object);

            // Act
            var result = _repository.VerifyNameBarbero(nombreBarbero);

            // Assert
            Assert.False(result);
        }
    }
}