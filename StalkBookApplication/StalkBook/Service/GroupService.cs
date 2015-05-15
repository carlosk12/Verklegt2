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
			try
			{
				groupName.ownerId = userId;
				groupName.timeCreated = System.DateTime.Now;

				db.groups.Add(groupName);
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
					Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
					Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
			}       
        }

        public GroupViewModel GetGroupById(int groupId, string theUserId)
        {
			try
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
                var getGroup = (from g in db.groupProfileFks
                                where g.groupID == groupId
                                where g.profileID == theUserId
                                select g).FirstOrDefault();
                if(getGroup == null)
                {
                    model.userIsInGroup = false;
                }
                else
                {
                    model.userIsInGroup = true;
                }

				return model;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
					Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
					Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
				return null;
			}          
        }

        public void DeleteGroup(int groupId)
        {
           	try
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
				var deleteStatusRating = (from r in db.userStatusRating where statusIds.Contains(r.statusId) select r).ToList();
				var deleteGroup = (from g in db.groups
								   where g.ID == groupId
								   select g).FirstOrDefault();

				foreach (var item in deleteFK)
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
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
					Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
					Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);			
			}        
        }

        public void PostStatus(string userId, Status status)
        {
			try
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
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
					Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
					Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
			}     
        }

        public IEnumerable<Group> GetGroupsByUserId(string userId)
        {
			try
			{
				List<string> result1 = (from s in db.groupProfileFks
										where s.profileID == userId
										select s.groupID.ToString()).ToList();
				var result = (from gr in db.groups where result1.Contains(gr.ID.ToString()) select gr).ToList();
				return result;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
					Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
					Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
				return null;
			}                     
        }

        public IEnumerable<Group> GetAllGroups()
        {
			try
			{
				List<Group> result = (from s in db.groups
									  select s).ToList();

				return result;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
					Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
					Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
				return null;
			}        
        }

        public void AddUserToGroup(string userId, int groupId)
        {
			try
			{
				var group = new GroupProfileFK();
				group.groupID = groupId;
				group.profileID = userId;
				db.groupProfileFks.Add(group);
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
					Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
					Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
			}            
        }

        public void RemoveUserFromGroup(string userId, int groupId)
        {
			try
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
			catch (Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
					Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
					Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
			}            
        }
    }
}