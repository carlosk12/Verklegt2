using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Service
{
    public class ProfileService
    {
		public Profile getOwnProfile(string id)
		{
			var db = new ApplicationDbContext();

			var value = (from p in db.profiles
						where p.userID == id
						select p).FirstOrDefault();

			return value;
		}
    }
}