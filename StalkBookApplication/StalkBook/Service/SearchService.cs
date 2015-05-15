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
            var model = new SearchViewModel();
            var profiles = from u in db.profiles
                           select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                profiles = profiles.Where(s => s.name.Contains(searchString));
            }
            model.searchResult = profiles.ToList();
            model.searchResult.Sort((x, y) => string.Compare(x.name, y.name));
            model.stalking = (from s in db.stalking
                              where s.userId == userId
                              select s.stalkedId).ToList();
            model.userId = userId;
            model.searchString = searchString;

            return model;
        }

        public GroupViewModel SearchGroups(string userId, string searchString)
        {
            var model = new GroupViewModel();
            var groups = (from g in db.groups
                           select g).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(s => s.name.Contains(searchString)).ToList();
            }
            

            groups.Sort((x, y) => string.Compare(x.name, y.name));
            model.groups = groups;
            model.groupsJoined = (from s in db.groupProfileFks
                              where s.profileID == userId
                              select s.groupID).ToList();
            model.searchString = searchString;
            model.myId = userId;

            return model;
        }
    }
}