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
            var statuses = service.GetAllAvailableStatuses(User.Identity.GetUserId());
            var model = new StatusViewModel();
            model.availableStatuses = statuses;
            model.myRatings = service.GetRatingByUserId(User.Identity.GetUserId());

            return View(model);
        }


        [HttpPost]
        public ActionResult Index(Status userStatus)
        {
            string theUserId = User.Identity.GetUserId();
            service.PostStatus(theUserId, userStatus);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Search(string searchString)
        {
            string theUserId = User.Identity.GetUserId();
            var model = service.Search(theUserId, searchString);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult RateStatus(int currRating, string arrowDirection)
        {
            string theUserId = User.Identity.GetUserId();
            var model = service.Search(theUserId, arrowDirection);

            return View(model);
        }
    }
}