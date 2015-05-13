using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Service
{
    public class ProfileService
    {
		private readonly IAppDataContext db;

        public ProfileService(IAppDataContext context)
         {
             db = context ?? new ApplicationDbContext();
         }

		public ProfileViewModel GetProfile(string id)
		{
			var profileInfo = (from p in db.profiles
						where p.userID == id
						select p).FirstOrDefault();

            List<Status> myStatuses = (from s in db.userStatuses
                              where s.userId == id
                              where s.groupId == null
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

		public Profile GetProfileEntity(string Id)
		{
			var profile = (	from p in db.profiles
							where p.userID == Id
							select p).SingleOrDefault();

			return profile;
		}

		public ProfileViewModel GetProfileByID(int id)
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

		public void AddProfilePicUrl(string userId, string url)
		{
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

		public void ChangeProfilePicUrl(string userId, string url)
		{
			if (!String.IsNullOrEmpty(url))
			{
				var profile = (from p in db.profiles
							   where p.userID == userId
							   select p).SingleOrDefault();

				profile.profilePicUrl = url;


				db.SaveChanges();
			}
		}

		public void DeleteStatus(int statusId)
		{
			var deleteStatus = (from s in db.userStatuses
								where s.ID == statusId
								select s).SingleOrDefault();

			var deleteStatusRatings = (from s in db.userStatusRating
									   where s.statusId == statusId
									   select s).ToList();

			db.userStatuses.Remove(deleteStatus);
            foreach(var item in deleteStatusRatings)
            {
                db.userStatusRating.Remove(item);
            }

			db.SaveChanges();
		}
    }
}