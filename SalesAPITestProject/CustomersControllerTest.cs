using Microsoft.VisualStudio.TestTools.UnitTesting; 
using Moq;
using SalesWebAPI.Controllers;
using SalesWebAPI.Interfaces;
using SalesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SalesAPITestProject
{
    [TestClass]
    public class CustomersControllerTest
    {
        private Mock<ICustomerRepository> _mockRepository;
        private CustomersController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Initialize mock repository and controller
            _mockRepository = new Mock<ICustomerRepository>();
            _controller = new CustomersController(_mockRepository.Object);
        }

        [TestMethod]
        public void GetCustomers_ShouldReturnLists()
        {
            // Arrange
            var mockCustomers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "John Doe" },
                new Customer { CustomerId = 2, Name = "Jane Smith" }
            };
            _mockRepository.Setup(repo => repo.GetCustomers()).Returns(mockCustomers);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult); // Asserts that the result is not null
            Assert.AreEqual(200, okResult.StatusCode); // Asserts that the status code is 200 OK
            Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<Customer>)); // Ensures the result is a list of customers
        }

        [TestMethod]
        public void GetCustomers_When_Called_returnsNull()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetCustomers()).Returns((List<Customer>)null);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult)); // Ensures the result is an OkObjectResult
            var okResult = result as OkObjectResult;
            Assert.IsNull(okResult.Value); // Asserts that the returned customer list is null
        }

        [TestMethod]
        public void GetCustomer_WithAnInvalidId_ShouldReturnNull()
        {
            // TODO: Implement this test
        }

        [TestMethod]
        public void GetCustomer_WithAValidId_ShouldReturnCustomer()
        {
            // TODO: Implement this test
        }
    }
}
