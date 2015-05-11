using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Service
{
	public class ManageService
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		public void ChangeEmail(string Id, string newEmail)
		{
			var result = (from u in db.Users
						  where u.Id == Id
						  select u).SingleOrDefault();

			if(result != null)
			{
				result.UserName = result.Email = newEmail;
				db.SaveChanges();
			}
		}
	}
}