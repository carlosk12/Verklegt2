using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StalkBook.Models;
using StalkBook.Entity;
using StalkBook.Service;
using Stalkbook.Tests;
using System.Collections.Generic;
using System.Linq;

namespace StalkBook.Tests.Service
{
	[TestClass]
	public class NewsfeedServiceTest
    {
        private NewsfeedService service;
        private MockDatabase mockDb = new MockDatabase();

        [TestInitialize]
        public void Initialize()
        {
            var status1 = new Status { ID = 1, groupId = 1, body = "status1", userId = "User1" };
            var status2 = new Status { ID = 2, groupId = 1, body = "status2", userId = "User1" };
            var status3 = new Status { ID = 3, groupId = 2, body = "status3", userId = "User2" };
            List<Status> statuses = new List<Status> { status1, status2, status3 };

            foreach (var item in statuses)
            {
                mockDb.userStatuses.Add(item);
            }

            service = new NewsfeedService(mockDb);
        }

        [TestMethod]
        public void GetAllAvailableStatuses()
        {
            // Arrange
            var numberOfStatuses = 2;

            // Act
            var result = service.GetMyStatuses("User1");

            // Assert
            Assert.AreEqual(numberOfStatuses, result.Count());
        }

        [TestMethod]
        public void GetMyStatusesUser1()
        {
            // Arrange
            var numberOfStatuses = 2;

            // Act
            var result = service.GetMyStatuses("User1");

            // Assert
            Assert.AreEqual(numberOfStatuses, result.Count());
        }

        [TestMethod]
        public void GetMyStatusesUser3()
        {
            // Arrange
            var numberOfStatuses = 0;

            // Act
            var result = service.GetMyStatuses("User3");

            // Assert
            Assert.AreEqual(numberOfStatuses, result.Count());
        }
	}
}
