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
        enum Ratings { Neutral = 0, Upvote = 1, Downvote = 2 };
        private NewsfeedService service;
        private MockDatabase mockDb = new MockDatabase();

        [TestInitialize]
        public void Initialize()
        {
            var status1 = new Status { ID = 1, groupId = null, body = "status1", userId = "User1" };
            var status2 = new Status { ID = 2, groupId = null, body = "status2", userId = "User1" };
            var status3 = new Status { ID = 3, groupId = 1, body = "status3", userId = "User2" };
            var status4 = new Status { ID = 4, groupId = null, body = "status4", userId = "User3" };
            List<Status> statuses = new List<Status> { status1, status2, status3, status4 };
            foreach (var item in statuses)
            {
                mockDb.userStatuses.Add(item);
            }
            var stalker1 = new Stalking { ID = 1, userId = "User1", stalkedId = "User1" };
            var stalker2 = new Stalking { ID = 2, userId = "User2", stalkedId = "User2" };
            var stalker3 = new Stalking { ID = 3, userId = "User3", stalkedId = "User3" };
            var stalker4 = new Stalking { ID = 4, userId = "User4", stalkedId = "User4" };
            var stalker5 = new Stalking { ID = 5, userId = "User1", stalkedId = "User3" };
            var stalker6 = new Stalking { ID = 6, userId = "User2", stalkedId = "User3" };
            var stalker7 = new Stalking { ID = 7, userId = "User2", stalkedId = "User1" };
            var stalker8 = new Stalking { ID = 8, userId = "User2", stalkedId = "User4" };
            var stalker9 = new Stalking { ID = 9, userId = "User3", stalkedId = "User4" };
            var stalker10 = new Stalking { ID = 10, userId = "User4", stalkedId = "User1" };
            List<Stalking> stalkings = new List<Stalking> { stalker1, stalker2, stalker3, stalker4, stalker5, stalker6, stalker7, stalker8, stalker9, stalker10 };
            foreach (var item in stalkings)
            {
                mockDb.stalking.Add(item);
            }
            var userRating1 = new UserStatusRating { ID = 1, rating = (int)Ratings.Downvote, statusId = 1, userId = "User1" };
            var userRating2 = new UserStatusRating { ID = 2, rating = (int)Ratings.Upvote, statusId = 2, userId = "User1" };
            var userRating3 = new UserStatusRating { ID = 3, rating = (int)Ratings.Neutral, statusId = 1, userId = "User2" };
            var userRating4 = new UserStatusRating { ID = 4, rating = (int)Ratings.Downvote, statusId = 1, userId = "User3" };
            List<UserStatusRating> ratings = new List<UserStatusRating> { userRating1, userRating2, userRating3, userRating4 };
            foreach (var item in ratings)
            {
                mockDb.userStatusRating.Add(item);
            }

            service = new NewsfeedService(mockDb);
        }

        [TestMethod]
        public void UpdateRatingUserHasNotRated()
        {
            // Arrange
            var userRating = new UserStatusRating { ID = 5, rating = (int)Ratings.Upvote, statusId = 3, userId = "User1" };
            var numberOfUpvotes = 1;
            var numberOfDownvotes = 0;
            var currentRating = (int)Ratings.Neutral;
            var numberOfRatings = 5;

            // Act
            service.UpdateRating("User1", currentRating, "up", 3);

            // Assert
            Assert.AreEqual(userRating.rating, mockDb.userStatusRating.ElementAt(4).rating);
            Assert.AreEqual(numberOfUpvotes, mockDb.userStatuses.ElementAt(2).upvotes);
            Assert.AreEqual(numberOfDownvotes, mockDb.userStatuses.ElementAt(2).downvotes);
            Assert.AreEqual(numberOfRatings, mockDb.userStatusRating.Count());
        }

        [TestMethod]
        public void UpdateRatingToDownvoteCurrentUpvote()
        {
            // Arrange
            var userRating = new UserStatusRating { ID = 1, rating = (int)Ratings.Downvote, statusId = 1, userId = "User1" };
            var numberOfUpvotes = -1;
            var numberOfDownvotes = 1;
            var currentRating = (int)Ratings.Upvote;

            // Act
            service.UpdateRating("User1", currentRating, "down", 1);

            // Assert
            Assert.AreEqual(userRating.rating, mockDb.userStatusRating.ElementAt(0).rating);
            Assert.AreEqual(numberOfUpvotes, mockDb.userStatuses.ElementAt(0).upvotes);
            Assert.AreEqual(numberOfDownvotes, mockDb.userStatuses.ElementAt(0).downvotes);
        }

        [TestMethod]
        public void UpdateRatingToDownvoteCurrentNeutral()
        {
            // Arrange
            var userRating = new UserStatusRating { ID = 1, rating = (int)Ratings.Downvote, statusId = 1, userId = "User1" };
            var numberOfUpvotes = 0;
            var numberOfDownvotes = 1;
            var currentRating = (int)Ratings.Neutral;

            // Act
            service.UpdateRating("User1", currentRating, "down", 1);

            // Assert
            Assert.AreEqual(userRating.rating, mockDb.userStatusRating.ElementAt(0).rating);
            Assert.AreEqual(numberOfUpvotes, mockDb.userStatuses.ElementAt(0).upvotes);
            Assert.AreEqual(numberOfDownvotes, mockDb.userStatuses.ElementAt(0).downvotes);
        }

        [TestMethod]
        public void UpdateRatingToNeutralCurrentDownvote()
        {
            // Arrange
            var userRating = new UserStatusRating { ID = 1, rating = (int)Ratings.Neutral, statusId = 1, userId = "User1" };
            var numberOfUpvotes = 0;
            var numberOfDownvotes = -1;
            var currentRating = (int)Ratings.Downvote;

            // Act
            service.UpdateRating("User1", currentRating, "down", 1);

            // Assert
            Assert.AreEqual(userRating.rating, mockDb.userStatusRating.ElementAt(0).rating);
            Assert.AreEqual(numberOfUpvotes, mockDb.userStatuses.ElementAt(0).upvotes);
            Assert.AreEqual(numberOfDownvotes, mockDb.userStatuses.ElementAt(0).downvotes);
        }

        [TestMethod]
        public void UpdateRatingToNeutralCurrentUpvote()
        {
            // Arrange
            var userRating = new UserStatusRating { ID = 1, rating = (int)Ratings.Neutral, statusId = 1, userId = "User1" };
            var numberOfUpvotes = -1;
            var numberOfDownvotes = 0;
            var currentRating = (int)Ratings.Upvote;

            // Act
            service.UpdateRating("User1", currentRating, "up", 1);

            // Assert
            Assert.AreEqual(userRating.rating, mockDb.userStatusRating.ElementAt(0).rating);
            Assert.AreEqual(numberOfUpvotes, mockDb.userStatuses.ElementAt(0).upvotes);
            Assert.AreEqual(numberOfDownvotes, mockDb.userStatuses.ElementAt(0).downvotes);
        }

        [TestMethod]
        public void UpdateRatingToUpvoteCurrentNeutral()
        {
            // Arrange
            var userRating = new UserStatusRating { ID = 1, rating = (int)Ratings.Upvote, statusId = 1, userId = "User1" };
            var numberOfUpvotes = 1;
            var numberOfDownvotes = 0;
            var currentRating = (int)Ratings.Neutral;

            // Act
            service.UpdateRating("User1", currentRating, "up", 1);

            // Assert
            Assert.AreEqual(userRating.rating, mockDb.userStatusRating.ElementAt(0).rating);
            Assert.AreEqual(numberOfUpvotes, mockDb.userStatuses.ElementAt(0).upvotes);
            Assert.AreEqual(numberOfDownvotes, mockDb.userStatuses.ElementAt(0).downvotes);
        }

        [TestMethod]
        public void UpdateRatingToUpvoteCurrentDownvote()
        {
            // Arrange
            var userRating = new UserStatusRating { ID = 1, rating = (int)Ratings.Upvote, statusId = 1, userId = "User1" };
            var numberOfUpvotes = 1;
            var numberOfDownvotes = -1;
            var currentRating = (int)Ratings.Downvote;

            // Act
            service.UpdateRating("User1", currentRating, "up", 1);

            // Assert
            Assert.AreEqual(userRating.rating, mockDb.userStatusRating.ElementAt(0).rating);
            Assert.AreEqual(numberOfUpvotes, mockDb.userStatuses.ElementAt(0).upvotes);
            Assert.AreEqual(numberOfDownvotes, mockDb.userStatuses.ElementAt(0).downvotes);
        }

        [TestMethod]
        public void GetRatingByUserId1()
        {
            // Arrange
            var numberOfRatings = 2;
            var rating1 = 2;
            var rating2 = 1;

            // Act
            var result = service.GetRatingByUserId("User1");

            // Assert
            Assert.AreEqual(numberOfRatings, result.Count());
            Assert.AreEqual(rating1, result.ElementAt(0).rating);
            Assert.AreEqual(rating2, result.ElementAt(1).rating);
        }

        [TestMethod]
        public void GetRatingByUserId4()
        {
            // Arrange
            var numberOfRatings = 0;

            // Act
            var result = service.GetRatingByUserId("User4");

            // Assert
            Assert.AreEqual(numberOfRatings, result.Count());
        }

        [TestMethod]
        public void PostStatus()
        {
            // Arrange
            var numberOfStatuses = 5;
            var status = new Status{ID = 5, body = "status5", groupId = 2, userId = "User4"};

            // Act
            service.PostStatus("User4", status);

            // Assert
            Assert.AreEqual(numberOfStatuses, mockDb.userStatuses.Count());
            Assert.AreEqual(status.body, mockDb.userStatuses.ElementAt(4).body);
        }

        [TestMethod]
        public void GetAllAvailableStatusesUser1()
        {
            // Arrange
            var numberOfStatuses = 3;

            // Act
            var result = service.GetAllAvailableStatuses("User1");

            // Assert
            Assert.AreEqual(numberOfStatuses, result.Count());
        }

        [TestMethod]
        public void GetAllAvailableStatusesUser3()
        {
            // Arrange
            var numberOfStatuses = 1;

            // Act
            var result = service.GetAllAvailableStatuses("User3");

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
        public void GetMyStatusesUser4()
        {
            // Arrange
            var numberOfStatuses = 0;

            // Act
            var result = service.GetMyStatuses("User4");

            // Assert
            Assert.AreEqual(numberOfStatuses, result.Count());
        }
	}
}
