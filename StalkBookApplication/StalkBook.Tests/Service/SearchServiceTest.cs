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
	public class SearchServiceTest
	{
        private SearchService service;
        private MockDatabase mockDb = new MockDatabase();

	    [TestInitialize]
	    public void Initialize()
	    {
	        var profile1 = new Profile {ID = 1, name = "Gunni", userID = "User1"};
            var profile2 = new Profile { ID = 2, name = "Gummi", userID = "User2" };
            var profile3 = new Profile { ID = 3, name = "Gunna", userID = "User3" };

            var stalker1 = new Stalking{ID = 1, userId = "User1", stalkedId = "User1"};
            var stalker2 = new Stalking { ID = 2, userId = "User2", stalkedId = "User2" };
            var stalker3 = new Stalking { ID = 3, userId = "User3", stalkedId = "User3" };
            var stalker4 = new Stalking { ID = 4, userId = "User1", stalkedId = "User3" };

            var group1 = new Group { ID = 1, name = "Group1", ownerId = "User1"};
            var group2 = new Group { ID = 2, name = "Group2", ownerId = "User2"};
            var group3 = new Group { ID = 3, name = "Group3", ownerId = "User2"};

            var groupProfileFK1 = new GroupProfileFK { ID = 1, groupID = 1, profileID = "User1" };
            var groupProfileFK2 = new GroupProfileFK { ID = 2, groupID = 2, profileID = "User1" };

	        List<Stalking> stalkings = new List<Stalking>{stalker1, stalker2, stalker3, stalker4};

            List<Profile> profiles = new List<Profile>{profile1, profile2, profile3};

            List<Group> groups = new List<Group>{group1, group2, group3};

            List<GroupProfileFK> groupProfileFks = new List<GroupProfileFK>{groupProfileFK1, groupProfileFK1};

	        foreach (var item in stalkings)
	        {
	            mockDb.stalking.Add(item);
	        }

	        foreach (var item in profiles)
	        {
	            mockDb.profiles.Add(item);
	        }

	        foreach (var item in groups)
	        {
	            mockDb.groups.Add(item);
	        }

	        foreach (var item in groupProfileFks)
	        {
	            mockDb.groupProfileFks.Add(item);
	        }


            service = new SearchService(mockDb);

	    }

		[TestMethod]
		public void SearchUserProfileSearchStringContainsAll()
		{
            // Arrange
            var profile1 = new Profile { ID = 1, name = "Gunni", userID = "User1" };
            var profile2 = new Profile { ID = 2, name = "Gummi", userID = "User2" };
            var profile3 = new Profile { ID = 3, name = "Gunna", userID = "User3" };
            List<string> stalkings = new List<string> {"User1", "User2", "User3"};
            List<Profile> profiles = new List<Profile>{profile1, profile2, profile3 };

		    var expectedResult = new SearchViewModel {searchString = "G", userId = "User3", stalking = stalkings, searchResult = profiles};

            // Act
            var result = service.Search("User1", "G");

            // Assert
            Assert.AreEqual(expectedResult.searchResult.ElementAt(0).ID, result.searchResult.ElementAt(0).ID);
            Assert.AreEqual(expectedResult.searchResult.ElementAt(1).ID, result.searchResult.ElementAt(1).ID);
            Assert.AreEqual(expectedResult.searchResult.ElementAt(2).ID, result.searchResult.ElementAt(2).ID);
		}

        [TestMethod]
        public void SearchUserProfileSearchStringEmpty()
        {
            // Arrange
            var profile1 = new Profile { ID = 1, name = "Gunni", userID = "User1" };
            var profile2 = new Profile { ID = 2, name = "Gummi", userID = "User2" };
            var profile3 = new Profile { ID = 3, name = "Gunna", userID = "User3" };
            List<string> stalkings = new List<string> { "User1", "User2", "User3" };
            List<Profile> profiles = new List<Profile> { profile1, profile2, profile3 };

            var expectedResult = new SearchViewModel { searchString = "", userId = "User3", stalking = stalkings, searchResult = profiles };

            // Act
            var result = service.Search("User3", "");

            // Assert
            Assert.AreEqual(expectedResult.searchResult.ElementAt(0).ID, result.searchResult.ElementAt(0).ID);
            Assert.AreEqual(expectedResult.searchResult.ElementAt(1).ID, result.searchResult.ElementAt(1).ID);
            Assert.AreEqual(expectedResult.searchResult.ElementAt(2).ID, result.searchResult.ElementAt(2).ID);
        }

        [TestMethod]
        public void SearchUserProfileSearchStringContainsOneProfile()
        {
            // Arrange
            var profile2 = new Profile { ID = 2, name = "Gummi", userID = "User2" };
            List<string> stalkings = new List<string> { "User2" };
            List<Profile> profiles = new List<Profile> { profile2 };

            var expectedResult = new SearchViewModel { searchString = "Gum", userId = "User3", stalking = stalkings, searchResult = profiles };
            var numberOfResults = 1;

            // Act
            var result = service.Search("User3", "Gum");

            // Assert
            Assert.AreEqual(expectedResult.searchResult.ElementAt(0).ID, result.searchResult.ElementAt(0).ID);
            Assert.AreEqual(result.searchResult.Count(), numberOfResults);
        }

        [TestMethod]
        public void SearchUserProfileSearchStringNoProfiles()
        {
            // Arrange
            var profile2 = new Profile { ID = 2, name = "Gummi", userID = "User2" };
            List<string> stalkings = new List<string> { };
            List<Profile> profiles = new List<Profile> { };

            var expectedResult = new SearchViewModel { searchString = "Gunnar", userId = "User3", stalking = stalkings, searchResult = profiles };
            var numberOfResults = 0;

            // Act
            var result = service.Search("User3", "Gunnar");

            // Assert
            Assert.AreEqual(result.searchResult.Count(), numberOfResults);
        }

        [TestMethod]
        public void SearchGroupSearchStringContainsAll()
        {
            // Arrange
            var profile1 = new Profile { ID = 1, name = "Gunni", userID = "User1" };
            var profile2 = new Profile { ID = 2, name = "Gummi", userID = "User2" };
            var profile3 = new Profile { ID = 3, name = "Gunna", userID = "User3" };
            List<string> stalkings = new List<string> { "User1", "User2", "User3" };
            List<Profile> profiles = new List<Profile> { profile1, profile2, profile3 };

            var expectedResult = new SearchViewModel { searchString = "G", userId = "User3", stalking = stalkings, searchResult = profiles };

            // Act
            var result = service.Search("User1", "G");

            // Assert
            Assert.AreEqual(expectedResult.searchResult.ElementAt(0).ID, result.searchResult.ElementAt(0).ID);
            Assert.AreEqual(expectedResult.searchResult.ElementAt(1).ID, result.searchResult.ElementAt(1).ID);
            Assert.AreEqual(expectedResult.searchResult.ElementAt(2).ID, result.searchResult.ElementAt(2).ID);
        }

        [TestMethod]
        public void SearchGroupSearchStringEmpty()
        {
            // Arrange
            var profile1 = new Profile { ID = 1, name = "Gunni", userID = "User1" };
            var profile2 = new Profile { ID = 2, name = "Gummi", userID = "User2" };
            var profile3 = new Profile { ID = 3, name = "Gunna", userID = "User3" };
            List<string> stalkings = new List<string> { "User1", "User2", "User3" };
            List<Profile> profiles = new List<Profile> { profile1, profile2, profile3 };

            var expectedResult = new SearchViewModel { searchString = "", userId = "User3", stalking = stalkings, searchResult = profiles };

            // Act
            var result = service.Search("User3", "");

            // Assert
            Assert.AreEqual(expectedResult.searchResult.ElementAt(0).ID, result.searchResult.ElementAt(0).ID);
            Assert.AreEqual(expectedResult.searchResult.ElementAt(1).ID, result.searchResult.ElementAt(1).ID);
            Assert.AreEqual(expectedResult.searchResult.ElementAt(2).ID, result.searchResult.ElementAt(2).ID);
        }

        [TestMethod]
        public void SearchGroupSearchStringContainsOneProfile()
        {
            // Arrange
            var profile2 = new Profile { ID = 2, name = "Gummi", userID = "User2" };
            List<string> stalkings = new List<string> { "User2" };
            List<Profile> profiles = new List<Profile> { profile2 };

            var expectedResult = new SearchViewModel { searchString = "Gum", userId = "User3", stalking = stalkings, searchResult = profiles };
            var numberOfResults = 1;

            // Act
            var result = service.Search("User3", "Gum");

            // Assert
            Assert.AreEqual(expectedResult.searchResult.ElementAt(0).ID, result.searchResult.ElementAt(0).ID);
            Assert.AreEqual(result.searchResult.Count(), numberOfResults);
        }

        [TestMethod]
        public void SearcGroupSearchStringNoProfiles()
        {
            // Arrange
            var group1 = new Group { ID = 1, name = "Group1", ownerId = "User1"};
            var groupFK = new GroupProfileFK{ID = 1, groupID = 1, profileID = "User1"};

            List<string> groupProfileList = new List<string>{"User1"};

            var expectedResult = new SearchViewModel { searchString = "Group", userId = "User3", searchResult = profiles };
            var numberOfResults = 0;

            // Act
            var result = service.Search("User3", "Gunnar");

            // Assert
            Assert.AreEqual(result.searchResult.Count(), numberOfResults);
        }
	}
}
