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
        private NewsfeedService service = new NewsfeedService();
        // GET: NewsFeed
        public ActionResult Index()
        {
            var statuses = service.getAllAvailableStatuses(User.Identity.GetUserId());
            var model = new StatusViewModel();
            model.availableStatuses = statuses;

            return View(model);
        }


        [HttpPost]
        public ActionResult Index(Status userStatus)
        {
            string theUserId = User.Identity.GetUserId();
            service.postStatus(theUserId, userStatus);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Search(string searchString)
        {
            string theUserId = User.Identity.GetUserId();
            var model = service.search(theUserId, searchString);
            
            return View(model);
        }
    }
}