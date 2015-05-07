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
        public IEnumerable<Status> getMyStatuses(string userId)
        {
            var db = new ApplicationDbContext();

            IEnumerable<Status> value = (from p in db.userStatuses
                         where p.userId == userId            
                         select p).Take(20);

            return value;
        }
    }
}