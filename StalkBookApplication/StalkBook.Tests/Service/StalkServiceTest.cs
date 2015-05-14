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
	public class StalkServiceTest
	{
        private StalkService service;
        private MockDatabase mockDb = new MockDatabase();
		[TestInitialize]
		public void Initialize()
		{
		    var stalker1 = new Stalking {ID = 1, userId = "User1", stalkedId = "User1"};
            var stalker2 = new Stalking { ID = 2, userId = "User2", stalkedId = "User2" };
            var stalker3 = new Stalking { ID = 3, userId = "User3", stalkedId = "User3" };
            var stalker4 = new Stalking { ID = 4, userId = "User4", stalkedId = "User4" };
            var stalker5 = new Stalking { ID = 5, userId = "User1", stalkedId = "User3" };
            var stalker6 = new Stalking { ID = 6, userId = "User2", stalkedId = "User3" };
            var stalker7 = new Stalking { ID = 7, userId = "User2", stalkedId = "User1" };
            var stalker8 = new Stalking { ID = 8, userId = "User2", stalkedId = "User4" };
            var stalker9 = new Stalking { ID = 9, userId = "User3", stalkedId = "User4" };
            var stalker10 = new Stalking { ID = 10, userId = "User4", stalkedId = "User1" };

            List<Stalking> stalkings = new List<Stalking>{stalker1, stalker2, stalker3, stalker4, stalker5, stalker6, stalker7, stalker8, stalker9, stalker10};

		    foreach (var item in stalkings)
		    {
		        mockDb.stalking.Add(item);
		    }

		    service = new StalkService(mockDb);
		}

	    [TestMethod]
	    public void StalkUser()
	    {
            // Arrange
            var numberOfStalkingUsers = 11;

            // Act
            service.StalkUser("User4", "User2");

            // Assert
            Assert.AreEqual(numberOfStalkingUsers, mockDb.stalking.Count());
	    }

	    [TestMethod]
	    public void StopStalkingUser()
	    {
            // Arrange
            var numberOfStalkingUsers = 8;

            // Act
            service.StopStalkingUser("User4", "User2");
            service.StopStalkingUser("User3", "User2");
            service.StopStalkingUser("User2", "User3");
            service.StopStalkingUser("User3", "User4");

            // Assert
            Assert.AreEqual(numberOfStalkingUsers, mockDb.stalking.Count());
	    }
	}
}
