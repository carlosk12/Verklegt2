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
			result.profilePicUrl = profileInfo.profilePicUrl;

			return result;
		}

		public Profile getProfileEntity(string Id)
		{
			var profile = (	from p in db.profiles
							where p.userID == Id
							select p).SingleOrDefault();

			return profile;
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
			result.profilePicUrl = profileInfo.profilePicUrl;

			return result;
		}

		public void addProfilePicUrl(string userId, string url)
		{
			var db = new ApplicationDbContext();

			var profile = (from p in db.profiles
						   where p.userID == userId
						   select p).SingleOrDefault();

			if (String.IsNullOrEmpty(url))
			{
				profile.profilePicUrl = "https://a7b146f211da20455eacff07e84c48e5e1ba36e0.googledrive.com/host/0B6sTYcGVKmPpbGtNQzB2U05XbDA/Chrome%20Incognito%20Windows%20Icon.ico";
			}
			else
			{
				profile.profilePicUrl = url;
			}

			db.SaveChanges();
		}

		public void changeProfilePicUrl(string userId, string url)
		{
			if (!String.IsNullOrEmpty(url))
			{
				var db = new ApplicationDbContext();

				var profile = (from p in db.profiles
							   where p.userID == userId
							   select p).SingleOrDefault();

				profile.profilePicUrl = url;


				db.SaveChanges();
			}
		}
    }
}