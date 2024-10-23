using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesWebAPI.Controllers;
using SalesWebAPI.Interfaces;
using SalesWebAPI.Models;
using System.Numerics;

namespace SalesAPITestProject
{
    [TestClass]
    public class CustomersControllerTest
    {
        [TestMethod]
        public void GetCustomers_ShouldReturnLists()
        {
            //
        }
        [TestMethod]
        public void GetCustomers_When_Called_returnsNull()
        {
            //

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
           //
        }

    }
}