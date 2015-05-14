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

		[TestInitialize]
		public void Initialize()
		{
			var mockDb = new MockDatabase();
			var profileView = new ProfileViewModel();
			var profileEntity = new Profile();

			profileView.ID = profileEntity.ID = 1;
			profileView.name = profileEntity.name = "Carlos Ragnar Kárason";
			profileView.creationDate = profileEntity.creationDate = new DateTime(2015, 5, 13, 11, 07, 41);
			profileView.userID = profileEntity.userID = "CarlosUserID";
			profileView.profilePicUrl = profileEntity.profilePicUrl = "picture.jpg";

			List<Status> statuses = new List<Status>();
			var status1 = new Status { ID = 1, groupId = null, body = "Status 1"};
			var status2 = new Status { ID = 2, groupId = null, body = "Status 2"};
			var status3 = new Status { ID = 3, groupId = null, body = "Status 3"};
			statuses.Add(status1);
			statuses.Add(status2); 
			statuses.Add(status3);

			profileView.userStatuses = statuses;

			List<UserStatusRating> myRatings = new List<UserStatusRating>();

			UserStatusRating rating1 = new UserStatusRating { ID = 1, userId = "CarlosUserID", statusId = 1, rating = 1 };
			UserStatusRating rating2 = new UserStatusRating { ID = 2, userId = "CarlosUserID", statusId = 2, rating = -1 };

			myRatings.ToList().Add(rating1);

			profileView.body = "Status 4";
			profileView.urlToPic = "picture2.jpg";
			profileView.myId = "CarlosUserID";
		}

		[TestMethod]
		public void GetProfileForCarlos()
		{
			//Arrange
			service = new ProfileService(null);

			//Act

			//Assert
		}
	}
}
