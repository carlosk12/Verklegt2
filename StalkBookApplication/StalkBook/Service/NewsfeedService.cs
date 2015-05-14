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

		public IEnumerable<Status> GetAllAvailableStatuses(string userId)
		{
            try
            {
                List<string> result1 = (from s in db.stalking
                                        where s.userId == userId
                                        select s.stalkedId.ToString()).ToList();

                var result = (from us in db.userStatuses where result1.Contains(us.userId) where us.groupId == null orderby us.timeCreated descending select us).Take(50);

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