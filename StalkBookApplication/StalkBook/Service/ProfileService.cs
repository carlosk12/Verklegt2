using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Service
{
    public class ProfileService
    {
		private ApplicationDbContext db = new ApplicationDbContext();

		public ProfileViewModel getProfile(string id)
		{
			var profileInfo = (from p in db.profiles
						where p.userID == id
						select p).FirstOrDefault();

            List<Status> myStatuses = (from s in db.userStatuses
                              where s.userId == id
                              orderby s.timeCreated descending
							  select s).ToList();

            var result = new ProfileViewModel();
            result.creationDate = profileInfo.creationDate;
            result.name = profileInfo.name;
            result.userStatuses = myStatuses;
            result.userID = profileInfo.userID;

			return result;
		}

		public ProfileViewModel getProfileByID(int id)
		{
			var profileInfo = (from p in db.profiles
							   where p.ID == id
							   select p).FirstOrDefault();

			System.Diagnostics.Debug.Write(profileInfo.userID);

			List<Status> myStatuses = (from s in db.userStatuses
									   where s.userId == profileInfo.userID
									   orderby s.timeCreated descending
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