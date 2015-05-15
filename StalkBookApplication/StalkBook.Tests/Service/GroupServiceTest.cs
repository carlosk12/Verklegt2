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
    public class GroupServiceTest
    {
        private GroupService service;
		private MockDatabase mockDb = new MockDatabase();

        [TestInitialize]
        public void Initialize()
        {           
			var status1 = new Status { ID = 1, groupId = 1, body = "status1", userId = "User1" };
			var status2 = new Status { ID = 2, groupId = 1, body = "status2", userId = "User1" };
			var status3 = new Status { ID = 3, groupId = 2, body = "status3", userId = "User2" };
            List<Status> statuses = new List<Status>{status1, status2, status3};

			var group1 = new Group { ID = 1, name = "group1", ownerId = "User1" };
			var group2 = new Group { ID = 2, name = "group2", ownerId = "User2" };
			var group3 = new Group { ID = 2, name = "group2", ownerId = "User2" };

			var groupProfileFK1 = new GroupProfileFK { ID = 1, groupID = 1, profileID = "User1" };
			var groupProfileFK2 = new GroupProfileFK { ID = 2, groupID = 2, profileID = "User1" };
			var userRating = new UserStatusRating { ID = 1, rating = 2, statusId = 1, userId = "User1" };
 			
			foreach(var item in statuses)
			{
				mockDb.userStatuses.Add(item);
			}
			mockDb.groups.Add(group1);
			mockDb.groups.Add(group2);
			mockDb.groups.Add(group3);
			mockDb.groupProfileFks.Add(groupProfileFK1);
			mockDb.groupProfileFks.Add(groupProfileFK2);
			mockDb.userStatusRating.Add(userRating);

            service = new GroupService(mockDb);
        }

		[TestMethod]
		public void RemoveUserFromGroup()
		{
			// Arrange
			var numberOfGroupProfileFK = 1;

			// Act
			service.RemoveUserFromGroup("User1", 2);

			// Assert
			Assert.AreEqual(numberOfGroupProfileFK, mockDb.groupProfileFks.Count());
			Assert.IsTrue(mockDb.groupProfileFks.ElementAt(0).groupID == 1);
		}

		[TestMethod]
		public void AddUserToGroup()
		{
			// Arrange
			var numberOfGroupProfileFK = 3;
			var groupProfileFK3 = new GroupProfileFK {ID = 0, groupID = 1, profileID = "User3" };

			// Act
			service.AddUserToGroup("User3", 1);

			// Assert
			Assert.AreEqual(numberOfGroupProfileFK, mockDb.groupProfileFks.Count());
			Assert.AreEqual(mockDb.groupProfileFks.ElementAt(2).profileID, groupProfileFK3.profileID);
		}

		[TestMethod]
		public void GetAllGroups()
		{
			// Arrange
			var numberOfGroups = 3;

			// Act
			var result = service.GetAllGroups();

			// Assert
			Assert.AreEqual(numberOfGroups, result.Count());
		}

		[TestMethod]
		public void GetGroupsByUserId()
		{
			// Arrange
			var group1 = new Group { ID = 1, name = "group1", ownerId = "User1" };
			var group2 = new Group { ID = 2, name = "group2", ownerId = "User2" };

			// Act
			var result = service.GetGroupsByUserId("User1");

			// Assert
			Assert.AreEqual(group1.name, result.ElementAt(0).name);
			Assert.AreEqual(group2.name, result.ElementAt(1).name);
		}
		[TestMethod]
		public void GetGroupsByUserIdUserDontExist()
		{
			// Arrange
			var numberOfGroups = 0;

			// Act
			var result = service.GetGroupsByUserId("User3");

			// Assert
			Assert.AreEqual(numberOfGroups, result.Count());
		}

		[TestMethod]
		public void CreateGroup()
		{
			// Arrange
			var numberOfGroups = 4;
			var group3 = new Group { ID = 3, name = "group3", ownerId = "User3" };

			// Act
			service.CreateGroup("Gunnar",group3);

			// Assert
			Assert.AreEqual(numberOfGroups, mockDb.groups.Count());
		}

		[TestMethod]
		public void DeleteGroup()
		{
			// Arrange
			var numberOfGroups = 2;
			var numberOfGroupProfileFK = 1;
			var numberOfStatuses = 1;
			var numberOfStatusRatings = 0;

			// Act
			service.DeleteGroup(1);

			// Assert
			Assert.AreEqual(numberOfGroups, mockDb.groups.Count());
			Assert.AreEqual(numberOfGroupProfileFK, mockDb.groupProfileFks.Count());
			Assert.AreEqual(numberOfStatuses, mockDb.userStatuses.Count());
			Assert.AreEqual(numberOfStatusRatings, mockDb.userStatusRating.Count());
		}

        [TestMethod]
        public void GetGroupsByIdGroupInDatabase()
        {
            // Arrange
			var status1 = new Status { ID = 1, groupId = 1, body = "status1", userId = "User1" };
			var status2 = new Status { ID = 2, groupId = 1, body = "status2", userId = "User1" };
			List<Status> statuses = new List<Status> { status1, status2};
			var group1 = new Group { ID = 1, name = "group1", ownerId = "User1" };
			var expectedResult = new GroupViewModel { name = "group1", groupStatuses = statuses, groupId = 1 };

            // Act
			var result = service.GetGroupById(1, "User1");

            // Assert
			Assert.AreEqual(expectedResult.groupStatuses.ElementAt(0).ID, result.groupStatuses.ElementAt(0).ID);
			Assert.AreEqual(expectedResult.groupStatuses.ElementAt(1).ID, result.groupStatuses.ElementAt(1).ID);
			Assert.AreEqual(expectedResult.groupStatuses.ElementAt(0).groupId, result.groupStatuses.ElementAt(0).groupId);
			Assert.AreEqual(expectedResult.groupStatuses.ElementAt(1).groupId, result.groupStatuses.ElementAt(1).groupId);
			Assert.AreEqual(expectedResult.groupStatuses.ElementAt(0).body,result.groupStatuses.ElementAt(0).body);
			Assert.AreEqual(expectedResult.groupStatuses.ElementAt(1).body, result.groupStatuses.ElementAt(1).body);
			Assert.AreEqual(expectedResult.name, result.name);
			Assert.AreEqual(expectedResult.groupId, result.groupId);				
        }

		[TestMethod]
		public void GetGroupsByIGroupNotInDatabase()
		{
			// Arrange
			
			// Act
			var result = service.GetGroupById(3, "User3");

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void PostStatus()
		{
			// Arrange
			var numberOfStatus = 4;
			var status4 = new Status { ID = 4, groupId = 1, body = "status4", userId = "User4" };

			// Act
			service.PostStatus(status4.userId, status4);

			// Assert
			Assert.AreEqual(numberOfStatus, mockDb.userStatuses.Count());
		}
    }
}
