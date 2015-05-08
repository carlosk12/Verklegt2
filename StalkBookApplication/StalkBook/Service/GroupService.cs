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

        public IEnumerable<Group> getAllGroups()
        {
            IEnumerable<Group> value = (from p in db.groups
                select p);

            return value;
        }

    }
}