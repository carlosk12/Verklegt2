using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StalkBook.Entity;

namespace StalkBook.Service
{
    enum Ratings { Neutral = 0, Upvote = 1, Downvote = 2};
    public class NewsfeedService
    {
		private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Status> GetMyStatuses(string userId)
        {
			IEnumerable<Status> value = (	from p in db.userStatuses
											where p.userId == userId            
											select p).Take(20);
          
            return value;
        }

		public IEnumerable<Status> GetAllAvailableStatuses(string userId)
		{
            List<string> result1 = (from s in db.stalking
                                    where s.userId == userId
                                    select s.stalkedId.ToString()).ToList();

            var result = (from us in db.userStatuses where result1.Contains(us.userId) orderby us.timeCreated descending select us).Take(25);

			return result;
		}

        public SearchViewModel Search(string userId, string searchString)
        {
            var model = new SearchViewModel();
            var profiles = from u in db.profiles
                           select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                profiles = profiles.Where(s => s.name.Contains(searchString));
            }
            model.searchResult = profiles.ToList();
            model.stalking = (from s in db.stalking
                              where s.userId == userId
                              select s.stalkedId).ToList();
            model.userId = userId;

            return model;
        }

        public void PostStatus(string userId, Status userStatus)
        {
            userStatus.userId = userId;
            userStatus.timeCreated = System.DateTime.Now;
			userStatus.fullName = (from p in db.profiles
                            where p.userID == userId
                            select p.name).SingleOrDefault();
			if(!String.IsNullOrEmpty(userStatus.urlToPic))
			{
				userStatus.urlToPic = userStatus.urlToPic;
			}			
            db.userStatuses.Add(userStatus);
            db.SaveChanges();
        }

        public IEnumerable<UserStatusRating> GetRatingByUserId(string userId)
        {
            var result = (from r in db.userStatusRating
                         where r.userId == userId
                         select r).ToList();

            return result;
        }

        public void UpdateRating(string userId, int currRating, string arrowDirection, int statusId)
        {
            var myRating = (from r in db.userStatusRating
                            where r.userId == userId
                            where r.statusId == statusId
                            select r).SingleOrDefault();
            var result = new UserStatusRating();
            var status = (from s in db.userStatuses
                          where s.ID == statusId
                          select s).SingleOrDefault();

            if(arrowDirection == "up" && currRating != (int)Ratings.Upvote)
            {
                result.rating = (int)Ratings.Upvote;
                result.statusId = statusId;
                result.userId = userId;
                if(currRating == (int)Ratings.Downvote)
                {
                    status.downvotes--;
                }
                status.upvotes++;         
            }
            else if (arrowDirection == "down" && currRating != (int)Ratings.Downvote)
            {
                result.rating = (int)Ratings.Downvote;
                result.statusId = statusId;
                result.userId = userId;
                if(currRating == (int)Ratings.Upvote)
                {
                    status.upvotes--;
                }
                status.downvotes++;
            }
            else
            {
                result.rating = (int)Ratings.Neutral;
                result.statusId = statusId;
                result.userId = userId;
                if (arrowDirection == "up")
                {
                    status.upvotes--;
                }
                else
                {
                    status.downvotes--;
                }
            }

            

            if(myRating != null)
            {
                myRating.rating = result.rating;
            }
            else
            {
                db.userStatusRating.Add(result);
            }

            db.SaveChanges();
        }
    }
}