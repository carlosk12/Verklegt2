using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StalkBook.Entity;

namespace StalkBook.Service
{
    public class SearchService
    {
        private readonly IAppDataContext db;

        public SearchService(IAppDataContext context)
        {
            db = context ?? new ApplicationDbContext();
        }
        public SearchViewModel Search(string userId, string searchString)
        {
            try
            {
                var model = new SearchViewModel();
                // Get all profiles.
                var profiles = from u in db.profiles
                               select u;

                // If the searchString is not empty.
                // Find all the profiles with name that contain the searchString.
                // Else profiles contains all profiles.
                if (!String.IsNullOrEmpty(searchString))
                {
                    profiles = profiles.Where(s => s.name.Contains(searchString));
                }
                model.searchResult = profiles.ToList();
                // Sort the searchResult in alphabetic order.
                model.searchResult.Sort((x, y) => string.Compare(x.name, y.name));
                // Id of all users that the user is stalking.
                model.stalking = (from s in db.stalking
                                  where s.userId == userId
                                  select s.stalkedId).ToList();
                model.userId = userId;
                model.searchString = searchString;

                return model;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
                return null;
            }
        }

        public GroupViewModel SearchGroups(string userId, string searchString)
        {
            try
            {
                var model = new GroupViewModel();
                // Get all groups.
                var groups = (from g in db.groups
                              select g).ToList();

                // If the searchString is not empty.
                // Find all the groups with name that contain the searchString.
                // Else groups contains all groups.
                if (!String.IsNullOrEmpty(searchString))
                {
                    groups = groups.Where(s => s.name.Contains(searchString)).ToList();
                }

                // Sort the groups in alphabetic order.
                groups.Sort((x, y) => string.Compare(x.name, y.name));
                model.groups = groups;
                // Id of all groups that user has joined.
                model.groupsJoined = (from s in db.groupProfileFks
                                      where s.profileID == userId
                                      select s.groupID).ToList();
                model.searchString = searchString;
                model.myId = userId;

                return model;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
                return null;
            }        
        }
    }
}