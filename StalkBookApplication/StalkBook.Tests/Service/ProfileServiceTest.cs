using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StalkBook.Service;
using Stalkbook.Tests;
using StalkBook.Models;

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
			var profile = new ProfileViewModel();


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
