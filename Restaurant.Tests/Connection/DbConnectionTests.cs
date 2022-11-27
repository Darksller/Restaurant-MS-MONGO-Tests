using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Restaurant.DAL.Interfaces;
using Restaurant.DAL.Repositories;
using Restaurant.DAL.Repositories.MongoDBRepository;
using Restaurant.DAL.Repositories.MsServerRepository;
using Restaurant.Tests;
using static MongoDB.Libmongocrypt.CryptContext;

namespace Restaurant.Tests.Connection
{
    public class DbConnectionTests
    {
        [Fact]
        public void MsServer_IncorrectPathTest_Exception()
        {
            // Arrange
            string connectionString = "WrongWay";
            IOrderRepository repository = new OrderRepositoryMS(connectionString);

            // Act
            var exception = Record.Exception(() => repository.Get(-1));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void MsServer_CorrectPathTest_Exception()
        {
            // Arrange
            string connectionString = @"Data Source=DARKSLLER\SQLEXPRESS;Initial Catalog=rest;Integrated Security=True";
            IOrderRepository repository = new OrderRepositoryMS(connectionString);

            // Act
            var exception = Record.Exception(() => repository.Get(-1));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void MongoDb_IncorrectPathTest_Exception()
        {
            // Arrange
            string connectionString = "WrongWay";
            IOrderRepository repository;

            // Act
            var exception = Record.Exception(() => repository = new OrderRepositoryMO(connectionString));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<MongoConfigurationException>(exception);
        }

        [Fact]
        public void MongoDb_CorrectPathTest_Exception()
        {
            // Arrange
            string connectionString = "mongodb://localhost:27017";
            IOrderRepository repository;

            // Act
            var exception = Record.Exception(() => repository = new OrderRepositoryMO(connectionString));

            // Assert
            Assert.Null(exception);
        }
    }
}
