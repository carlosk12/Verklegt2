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
            return View();
        }


        [HttpPost]
        public ActionResult Index(Status userStatus)
        {
            var db = new ApplicationDbContext();

            string theUserId = User.Identity.GetUserId();
            userStatus.userId = User.Identity.GetUserId();
            userStatus.timeCreated = System.DateTime.Now;
            db.userStatuses.Add(userStatus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}