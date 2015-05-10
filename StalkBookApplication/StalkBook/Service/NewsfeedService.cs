﻿using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StalkBook.Service
{
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

            // For debugging purposes
            foreach (var item in result)
            {
                System.Diagnostics.Debug.WriteLine(item.body);
            }

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
    }
}