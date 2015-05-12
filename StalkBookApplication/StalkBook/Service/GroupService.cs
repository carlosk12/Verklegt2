using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc.Html;
using StalkBook.Models;
using StalkBook.Entity;

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

        public GroupViewModel GetGroupById(int groupId)
        {
            var model = new GroupViewModel();
            var group = (from g in db.groups
                         where g.ID == groupId
                         select g).SingleOrDefault();
            model.creationDate = group.timeCreated;
            model.name = group.name;
            model.groupStatuses = (from gs in db.groupStatuses
                                   where gs.groupId == groupId
								   orderby gs.timeCreated descending
                                   select gs).ToList();
            model.groupId = groupId;
            
            return model;
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

        public void PostStatus(string userId, GroupStatus status)
        {
            status.userId = userId;
            status.timeCreated = System.DateTime.Now;
            status.fullName = (from p in db.profiles
                                   where p.userID == userId
                                   select p.name).SingleOrDefault();
            if (!String.IsNullOrEmpty(status.urlToPic))
            {
                status.urlToPic = status.urlToPic;
            }
            db.groupStatuses.Add(status);       
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

        public IEnumerable<Group> GetAllGroups()
        {
            List<Group> result = (from s in db.groups
                select s).ToList();

            return result;
        }

        public void AddUserToGroup(string userId, int groupId)
        {
            var group = new GroupProfileFK();
            group.groupID = groupId;
            group.profileID = userId;
            db.groupProfileFks.Add(group);
            db.SaveChanges();
        }

        public void RemoveUserFromGroup(string userId, int groupId)
        {
            var removeuser = (from s in db.groupProfileFks
                            where s.profileID == userId
                            where s.groupID == groupId
                            select s).FirstOrDefault();

            if (!String.IsNullOrEmpty(userId) && removeuser != null)
            {
                db.groupProfileFks.Remove(removeuser);
                db.SaveChanges();
            }
        }
    }
}