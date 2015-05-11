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

        public void DeleteGroup(string userId, Group groupName)
        {
            if (userId == groupName.ownerId)
            {
                var deleteFK = (from s in db.groupProfileFks
                                where s.profileID == userId
                                where s.groupID == groupName.ID
                                select s).FirstOrDefault();

                db.groupProfileFks.Remove(deleteFK);

                var deleteGroup = (from s in db.groups
                                   where s.ownerId == userId
                                   where s.ID == groupName.ID
                                   select s).FirstOrDefault();

                db.groups.Remove(deleteGroup);

                db.SaveChanges();
            }
        }

        public IEnumerable<Group> GetGroupsByUserId(string userId)
        {
            List<string> result1 = (from s in db.groupProfileFks
                where s.profileID == userId
                select s.groupID.ToString()).ToList();

            var result = (from gr in db.groups where result1.Contains(gr.ID.ToString()) select gr).ToList();

            return result;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            List<Group> result = (from s in db.groups
                select s).ToList();

            return result;
        }

        public void AddUserToGroup(string userId, Group group)
        {
            var groupFK = new GroupProfileFK();
            group.ID = groupFK.groupID;
            userId = groupFK.profileID;
            db.groupProfileFks.Add(groupFK);
            db.SaveChanges();
        }

        public void RemoveUserFromGroup(string userId, Group groups)
        {
            var removeuser = (from s in db.groupProfileFks
                            where s.profileID == userId
                            where s.groupID == groups.ID
                            select s).FirstOrDefault();

            db.groupProfileFks.Remove(removeuser);
            db.SaveChanges();
        }
    }
}