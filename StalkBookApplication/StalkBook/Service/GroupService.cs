using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc.Html;
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

        public IEnumerable<Group> GetGroupsByUserId(string userId)
        {
            List<string> result1 = (from s in db.groupProfileFks
                where s.profileID == userId
                select s.groupID.ToString()).ToList();

            var result = (from gr in db.groups where result1.Contains(gr.ID.ToString()) select gr).ToList();

            return result;

        }
    }
}