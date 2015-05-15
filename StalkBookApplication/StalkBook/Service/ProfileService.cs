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
            // Get profile of the user with id.
			var profileInfo = (from p in db.profiles
						where p.userID == id
						select p).FirstOrDefault();
            // Get all the statuses of the user that were not posted in a group.
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
            // Id of all users that the userId is stalking. Including himself.
			result.stalking = (	from s in db.stalking
								where s.userId == id
								select s.stalkedId).ToList();

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
            // Get status with statusId
			var deleteStatus = (from s in db.userStatuses
								where s.ID == statusId
								select s).SingleOrDefault();
            // Get all the ratings of that status.
			var deleteStatusRatings = (from s in db.userStatusRating
									   where s.statusId == statusId
									   select s).ToList();

            // Then remove it all from the database and save.
			db.userStatuses.Remove(deleteStatus);
            foreach(var item in deleteStatusRatings)
            {
                db.userStatusRating.Remove(item);
            }

			db.SaveChanges();
		}

		public IEnumerable<ProfileViewModel> ViewStalking(string Id, string theUserId)
		{
			List<ProfileViewModel> profiles = new List<ProfileViewModel>();
            // Get all the users that the user with Id is stalking.
			var stalkList = (from p in db.stalking
						  where p.userId == Id
						  select p).ToList();
            // Get the profile of the user with Id.
			ProfileViewModel myProfile = this.GetProfile(Id);

            // Add all the profiles that the user with Id is stalking.
			foreach (var item in stalkList)
			{
				profiles.Add( GetProfile(item.stalkedId) );
			}

            // Order the profiles in alphabetic order.
			profiles = profiles.OrderBy(x => x.name).ToList();
            // Get the id of all the users that the user that is logged in is stalking.
            myProfile.myStalkings = (from p in db.stalking
                                     where p.userId == theUserId
                                     select p.stalkedId).ToList();
            // Insert the profile of the user with id at the front of the list.
			profiles.Insert(0, myProfile);

			return profiles;
		}

		public IEnumerable<ProfileViewModel> ViewStalkers(string Id, string theUserId)
		{
			List<ProfileViewModel> profiles = new List<ProfileViewModel>();
            // Get all the users that are stalking the user with id.
			var stalkList = (from p in db.stalking
							 where p.stalkedId == Id
							 select p).ToList();
            // Get the profile of the user with Id.
			ProfileViewModel myProfile = this.GetProfile(Id);
            // Add all the profiles that are stalking the user with id.
			foreach (var item in stalkList)
			{
				profiles.Add(GetProfile(item.userId));
			}
            // Order the profiles in alphabetic order.
			profiles = profiles.OrderBy(x => x.name).ToList();
            // Get the id of all the users that the user that is logged in is stalking.
            myProfile.myStalkings = (from p in db.stalking
                                     where p.userId == theUserId
                                     select p.stalkedId).ToList();
            // Insert the profile of the user with id at the front of the list.
			profiles.Insert(0, myProfile);

			return profiles;
		}
    }
}