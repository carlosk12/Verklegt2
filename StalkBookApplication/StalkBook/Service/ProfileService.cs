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

		public IEnumerable<ProfileViewModel> ViewStalking(string Id)
		{
			List<ProfileViewModel> profiles = new List<ProfileViewModel>();

			var stalkList = (from p in db.stalking
						  where p.userId == Id
						  select p).ToList();

			ProfileViewModel myProfile = this.GetProfile(Id);

			foreach (var item in stalkList)
			{
				profiles.Add( GetProfile(item.stalkedId) );
			}

			profiles.Sort((x,y) => string.Compare(x.name, y.name));

			profiles.Insert(0, myProfile);

			return profiles;
		}

		public IEnumerable<ProfileViewModel> ViewStalkers(string Id)
		{
			List<ProfileViewModel> profiles = new List<ProfileViewModel>();

			var stalkList = (from p in db.stalking
							 where p.stalkedId == Id
							 select p).ToList();

			ProfileViewModel myProfile = this.GetProfile(Id);

			foreach (var item in stalkList)
			{
				profiles.Add(GetProfile(item.userId));
			}

			profiles.Sort((x, y) => string.Compare(x.name, y.name));

			return profiles;
		}
    }
}