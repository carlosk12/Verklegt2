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

		public IEnumerable<Status> getAllStatuses(string userID)
		{

			IEnumerable<Status> result = from p in db.userStatuses
                                         join s in db.profiles on p.userId equals s.userID
                                         select p;

            foreach (var item in result)
            {
                System.Diagnostics.Debug.WriteLine(item.body);
            }

			return result;
		}
    }
}