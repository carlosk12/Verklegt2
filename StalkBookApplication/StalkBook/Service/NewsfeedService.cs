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
        private readonly IAppDataContext db;

        public NewsfeedService(IAppDataContext context)
         {
             db = context ?? new ApplicationDbContext();
         }

        public IEnumerable<Status> GetMyStatuses(string userId)
        {
            try
            {
                IEnumerable<Status> value = (from p in db.userStatuses
                                             where p.userId == userId
                                             select p).ToList();
                return value;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
                return null;
            }
        }

		public StatusViewModel GetAllAvailableStatuses(string userId)
		{
            try
            {
                var model = new StatusViewModel();
                // Id of all users that the userId is stalking. Including himself.
                List<string> result1 = (from s in db.stalking
                                        where s.userId == userId
                                        select s.stalkedId.ToString()).ToList();
                // Take 50 most recent statuses from the users that the userId is stalking.
                var result = (from us in db.userStatuses where result1.Contains(us.userId) orderby us.timeCreated descending select us).Take(50);
                model.availableStatuses = result;
                // Profiles of all the users that the userId is stalking.
                model.profiles = (from p in db.profiles where result1.Contains(p.userID) select p).ToList();
                // GroupIds of all groups the userId has joined.
                List<int> groupsJoined = (from g in db.groupProfileFks where g.profileID == userId select g.groupID).ToList();
                model.groupsJoined = (from g in db.groups where groupsJoined.Contains(g.ID) select g).ToList();

                return model;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
                return null;
            }          
		}

        public void PostStatus(string userId, Status userStatus)
        {
            try
            {
                userStatus.userId = userId;
                userStatus.timeCreated = System.DateTime.Now;
                userStatus.fullName = (from p in db.profiles
                                       where p.userID == userId
                                       select p.name).SingleOrDefault();
                if (!String.IsNullOrEmpty(userStatus.urlToPic))
                {
                    userStatus.urlToPic = userStatus.urlToPic;
                }
                db.userStatuses.Add(userStatus);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
            }     
        }

        public IEnumerable<UserStatusRating> GetRatingByUserId(string userId)
        {
            try
            {
                var result = (from r in db.userStatusRating
                              where r.userId == userId
                              select r).ToList();

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
                return null;
            }          
        }

        public void UpdateRating(string userId, int currRating, string arrowDirection, int statusId)
        {
            try
            {
                // Get users current rating of the status. 
                // Return null if user hasn't rated the status.
                var myRating = (from r in db.userStatusRating
                                where r.userId == userId
                                where r.statusId == statusId
                                select r).SingleOrDefault();
                var result = new UserStatusRating();
                var status = (from s in db.userStatuses
                              where s.ID == statusId
                              select s).SingleOrDefault();

                if (arrowDirection == "up" && currRating != (int)Ratings.Upvote)
                {
                    result.rating = (int)Ratings.Upvote;
                    result.statusId = statusId;
                    result.userId = userId;
                    if (currRating == (int)Ratings.Downvote)
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
                    if (currRating == (int)Ratings.Upvote)
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


                // Update the rating if the user had already rated the status.
                // Else add the rating to userStatusRating.
                if (myRating != null)
                {
                    myRating.rating = result.rating;
                }
                else
                {
                    db.userStatusRating.Add(result);
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
            }             
        }
    }
}