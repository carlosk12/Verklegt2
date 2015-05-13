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
        private readonly IAppDataContext db;

        public GroupService(IAppDataContext context)
         {
             db = context ?? new ApplicationDbContext();
         }

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
            model.groupStatuses = (from gs in db.userStatuses
                                   where gs.groupId == groupId
								   orderby gs.timeCreated descending
                                   select gs).ToList();
            model.groupId = groupId;
            
            return model;
        }

        public void DeleteGroup(int groupId)
        {           
                var deleteFK = (from s in db.groupProfileFks
                                where s.groupID == groupId                            
                                select s).ToList();             
                var deleteGroupStatuses = (from s in db.userStatuses
                                           where s.groupId == groupId
                                           select s).ToList();
                List<int> statusIds = (from s in db.userStatuses
                                 where s.groupId == groupId
                                 select s.ID).ToList();                
                var deleteStatusRating = (from r in db.userStatusRating where statusIds.Contains(r.statusId)  select r).ToList();
                var deleteGroup = (from g in db.groups
                                   where g.ID == groupId
                                   select g).FirstOrDefault(); 
     
                foreach(var item in deleteFK)
                {
                    db.groupProfileFks.Remove(item); 
                }
                foreach (var item in deleteGroupStatuses)
                {
                    db.userStatuses.Remove(item);
                }
                foreach (var item in deleteStatusRating)
                {
                    db.userStatusRating.Remove(item);
                }
                         
                db.groups.Remove(deleteGroup);
          

                db.SaveChanges();
        }

        public void PostStatus(string userId, Status status)
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
            db.userStatuses.Add(status);       
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