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
        private NewsfeedService service = new NewsfeedService(null);
        private ProfileService newsFeedService = new ProfileService(null);
        // GET: NewsFeed
        public ActionResult Index()
        {
            var model = service.GetAllAvailableStatuses(User.Identity.GetUserId());
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
        public ActionResult RateStatus(int currRating, string arrowDirection, int statusId)
        {
            string theUserId = User.Identity.GetUserId();
            service.UpdateRating(theUserId, currRating, arrowDirection, statusId);

            return new EmptyResult();
        }
    }
}