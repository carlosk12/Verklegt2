using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Service
{
    public class ProfileService
    {
		public ProfileViewModel getOwnProfile(string id)
		{
			var db = new ApplicationDbContext();

			var profileInfo = (from p in db.profiles
						where p.userID == id
						select p).FirstOrDefault();

            List<Status> myStatuses = (from s in db.userStatuses
                              where s.userId == id
                              select s).ToList();

            var result = new ProfileViewModel();
            result.creationDate = profileInfo.creationDate;
            result.name = profileInfo.name;
            result.userStatuses = myStatuses;
            result.userID = profileInfo.userID;

			return result;
		}
    }
}