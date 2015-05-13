using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StalkBook.Service;
using StalkBook.Tests;

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
