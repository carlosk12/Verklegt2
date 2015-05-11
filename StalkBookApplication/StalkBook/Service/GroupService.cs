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

        public void CreateGroup(string userId, Group groupName)
        {
            groupName.ownerId = userId;
            groupName.timeCreated = System.DateTime.Now;

            db.groups.Add(groupName);
            db.SaveChanges();
        }        

    }
}