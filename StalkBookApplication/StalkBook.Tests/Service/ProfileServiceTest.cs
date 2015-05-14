using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StalkBook.Service;
using Stalkbook.Tests;
using StalkBook.Models;
using System.Collections.Generic;
using StalkBook.Entity;
using System.Linq;

namespace StalkBook.Tests.Service
{
	[TestClass]
	public class ProfileServiceTest
	{
		private ProfileService service;
		private MockDatabase mockDb = new MockDatabase();

		[TestInitialize]
		public void Initialize()
		{
			var profileEntity = new Profile();

			profileEntity.name = "Carlos Ragnar Kárason";
			profileEntity.userID = "CarlosUserID";
			profileEntity.profilePicUrl = "picture.jpg";

			mockDb.profiles.Add(profileEntity);

			
			var status1 = new Status { ID = 1, groupId = null, body = "Status 1", userId = "CarlosUserID"};
			var status2 = new Status { ID = 2, groupId = null, body = "Status 2", userId = "CarlosUserID" };
			var status3 = new Status { ID = 3, groupId = null, body = "Status 3", userId = "CarlosUserID" };
			List<Status> statuses = new List<Status> {status1, status2, status3 };

			foreach (var item in statuses)
			{
				mockDb.userStatuses.Add(item);
			}

			UserStatusRating rating1 = new UserStatusRating { ID = 1, userId = "CarlosUserID", statusId = 1, rating = 1 };
			UserStatusRating rating2 = new UserStatusRating { ID = 2, userId = "CarlosUserID", statusId = 2, rating = 2 };

			List<UserStatusRating> myRatings = new List<UserStatusRating> { rating1, rating2 };

			foreach (var item in myRatings)
			{
				mockDb.userStatusRating.Add(item);
			}

			service = new ProfileService(mockDb);
		}

		[TestMethod]
		public void GetProfileForCarlos()
		{
			//Arrange
			List<Status> statuses = new List<Status>();
			var status1 = new Status { ID = 1, groupId = null, body = "Status 1", userId = "CarlosUserID" };
			var status2 = new Status { ID = 2, groupId = null, body = "Status 2", userId = "CarlosUserID" };
			var status3 = new Status { ID = 3, groupId = null, body = "Status 3", userId = "CarlosUserID" };
			statuses.Add(status1);
			statuses.Add(status2);
			statuses.Add(status3);

			var profile = new ProfileViewModel {name = "Carlos Ragnar Kárason", 
												userID = "CarlosUserID", 
												profilePicUrl = "picture.jpg", 
												userStatuses = statuses, 
												body = "Status 4",
												urlToPic = "picture2.jpg",
												myId = "CarlosUserID"};

			//Act
			var result = service.GetProfile("CarlosUserID");

			//Assert
			Assert.AreEqual(profile.name, result.name);
			Assert.AreEqual(profile.creationDate, result.creationDate);
			Assert.AreEqual(profile.userID , result.userID);
			Assert.AreEqual(profile.profilePicUrl , result.profilePicUrl);
			Assert.AreEqual(profile.userStatuses.Count, result.userStatuses.Count);
			Assert.AreEqual(profile.userStatuses.ElementAt(0).body, result.userStatuses.ElementAt(0).body);
		}

		[TestMethod]
		public void GetProfileEntityForCarlos()
		{
			//Arrange
			var profile = new Profile { name = "Carlos Ragnar Kárason", 
										profilePicUrl = "picture.jpg", 
										userID = "CarlosUserID" };

			//Act
			var result = service.GetProfileEntity("CarlosUserID");

			//Assert
			Assert.AreEqual(profile.name, result.name);
			Assert.AreEqual(profile.creationDate, result.creationDate);
			Assert.AreEqual(profile.profilePicUrl, result.profilePicUrl);
			Assert.AreEqual(profile.userID, result.userID);
		}

		[TestMethod]
		public void ChangeProfilePicUrlForCarlosWithUrl()
		{
			//Arrange
			var expectedResult = "newpicture.jpg";

			//Act
			service.ChangeProfilePicUrl("CarlosUserID", expectedResult);
			var result = (from p in mockDb.profiles
							where p.userID == "CarlosUserID"
							  select p).SingleOrDefault();
			
			//Assert
			Assert.AreEqual("newpicture.jpg",  result.profilePicUrl);
		}

		[TestMethod]
		public void ChangeProfilePicUrlForCarlosWithoutUrl()
		{
			//Arrange
			
			//Act
			service.ChangeProfilePicUrl("CarlosUserID", null);
			var result = (from p in mockDb.profiles
						  where p.userID == "CarlosUserID"
						  select p).SingleOrDefault();

			//Assert
			Assert.AreEqual("picture.jpg", result.profilePicUrl);
		}

		[TestMethod]
		public void DeleteStatusForCarlos()
		{
			//Arrange
			var numberOfStatuses = mockDb.userStatuses.Count();

			//Act
			service.DeleteStatus(3);
			var newNumberOfStatuses = mockDb.userStatuses.Count();

			//Assert
			Assert.AreNotEqual(numberOfStatuses, newNumberOfStatuses);
		}
	}
}
