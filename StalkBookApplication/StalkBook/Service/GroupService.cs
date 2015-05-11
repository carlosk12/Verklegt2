using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StalkBook.Models;

namespace StalkBook.Service
{
    public class GroupService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
/*
        public IEnumerable<Group> GetMyGroups(string userId)
        {
            

            IEnumerable<Group> value = (from p in db.groupProfileFks
                                         where p.profileID == 
                                         select p).Take(20);

            return value;
        }
*/        

    }
}