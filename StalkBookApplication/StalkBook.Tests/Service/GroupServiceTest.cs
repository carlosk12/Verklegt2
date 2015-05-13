using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StalkBook.Models;
using StalkBook.Entity;
using StalkBook.Service;
using Stalkbook.Tests;
using System.Collections.Generic;

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
            List<Status> statuses = new List<Status>();
            var status1 = new Status
            {
                ID = 1,
                groupId = 1,
                body = "Group1"
            };
            var status2 = new Status
            {
                ID = 2,
                groupId = 1,
                body = "Group1"
            };
            var status3 = new Status
            {
                ID = 3,
                groupId = 2,
                body = "Group2"
            };
            statuses.Add(status1);


            var group1 = new Group
            {
                ID = 1,
                name = "Group1",
                ownerId = "Gunnar"
            };
            var group2 = new Group
            {
                ID = 2,
                name = "Group2",
                ownerId = "Jon"
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
