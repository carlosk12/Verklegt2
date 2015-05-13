using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StalkBook.Service;
using Stalkbook.Tests;

namespace StalkBook.Tests.Service
{
    [TestClass]
    public class GroupServiceTest
    {
        private GroupService service;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDatabase();
            service = new GroupService(mockDb);
        }

        [TestMethod]
        public void GetGroupsByUserId()
        {
            // Arrange
            //HomeController controller = new HomeController();

            // Act
            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
        }
    }
}
