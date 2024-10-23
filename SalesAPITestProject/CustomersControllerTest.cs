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
         // Arrange
        var mockRepo = new Mock<ICustomerRepository>();
        int invalidCustomerId = -1; // Ugyldigt ID


         // Mock repository metoden GetById til at returnere null, når der gives et ugyldigt ID
        mockRepo.Setup(repo => repo.GetById(invalidCustomerId))
                .Returns((Customer)null);


        var controller = new CustomersController(mockRepo.Object);


         // Act
        var result = controller.Get(invalidCustomerId) as NotFoundResult;


         // Assert
        Assert.IsNotNull(result); // Vi forventer, at resultatet er en NotFoundResult


        }


        [TestMethod]
        public void GetCustomer_WithAValidId_ShouldReturnCustomer()
        {
            // Arrange
            var customerId = 1;
            var mockCustomer = new Customer { CustomerId = customerId, Name = "John Doe" };
            _mockRepository.Setup(repo => repo.GetById(customerId)).Returns(mockCustomer);

            // Act
            var result = _controller.Get(customerId); // Call the Get method instead of GetCustomer

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult); // Asserts that the result is not null
            Assert.AreEqual(200, okResult.StatusCode); // Asserts that the status code is 200 OK
            Assert.IsInstanceOfType(okResult.Value, typeof(Customer)); // Ensures the result is a single customer
            Assert.AreEqual(mockCustomer, okResult.Value); // Asserts that the returned customer is the expected one
        }
    }
}
