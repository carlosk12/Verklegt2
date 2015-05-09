using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StalkBook.Service;

namespace StalkBook.Controllers
{
	[Authorize]
    public class NewsFeedController : Controller
    {
        // GET: NewsFeed
        public ActionResult Index()
        {
            var service = new NewsfeedService();

            var statuses = service.getAllAvailableStatuses(User.Identity.GetUserId());
            var model = new StatusViewModel();
            model.availableStatuses = statuses;

            return View(model);
        }


        [HttpPost]
        public ActionResult Index(Status userStatus)
        {
            var db = new ApplicationDbContext();
            string theUserId = User.Identity.GetUserId();
            userStatus.userId = User.Identity.GetUserId();
            userStatus.timeCreated = System.DateTime.Now;
            var fullName = (from p in db.profiles
                                  where p.userID == theUserId
                                  select p.name).SingleOrDefault();
            userStatus.fullName = fullName;
            db.userStatuses.Add(userStatus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var db = new ApplicationDbContext();
            var model = new SearchViewModel();
            string theUserId = User.Identity.GetUserId();
            var profiles = from u in db.profiles
                         select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                profiles = profiles.Where(s => s.name.Contains(searchString));
            }
            model.searchResult = profiles.ToList();
            model.stalking = (from s in db.stalking
                             where s.userId == theUserId
                             select s.stalkedId).ToList();
            model.userId = theUserId;

            return View(model);
        }
    }
}