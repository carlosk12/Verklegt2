using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
        public ActionResult Index(Status s)
        {
            var db = new ApplicationDbContext();
            var temp = User.Identity.GetUserId();

            UpdateModel(s);
            //db.userStatuses.Add(status);
            return RedirectToAction("Index");
        }

    }
}