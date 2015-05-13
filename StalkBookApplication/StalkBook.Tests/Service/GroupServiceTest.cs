using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StalkBook.Models;
using StalkBook.Entity;
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
            var group1 = new Group
            {
                ID = 1,
                name = "Group1",
                ownerId = "Ingimar"
            };
            var group1ViewModel = new GroupViewModel
            {
                ID = 1,
                name = "Group1",
                groupId = 1,


            };
            mockDb.groups.Add(group1);


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
            Assert.IsNotNull(null);
        }
    }
}
