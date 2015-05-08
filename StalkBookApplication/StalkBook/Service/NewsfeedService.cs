using StalkBook.Models;
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

        public IEnumerable<Status> getMyStatuses(string userId)
        {
			IEnumerable<Status> value = (	from p in db.userStatuses
											where p.userId == userId            
											select p).Take(20);

            return value;
        }

		public IEnumerable<Status> getAllAvailableStatuses(string userID)
		{
            List<string> result1 = (from s in db.stalking
                                    where s.userId == userID
                                    select s.stalkedId.ToString()).ToList();

            var result = (from us in db.userStatuses where result1.Contains(us.userId) orderby us.timeCreated descending select us).Take(25);

            // For debugging purposes
            foreach (var item in result)
            {
                System.Diagnostics.Debug.WriteLine(item.body);
            }

			return result;
		}
    }
}