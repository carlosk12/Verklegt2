using StalkBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StalkBook.Models;

namespace StalkBook.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
		public ActionResult Index()
        {
			var service = new ProfileService();
            var newsFeedService = new NewsfeedService();

			var model = service.getProfile(User.Identity.GetUserId());
            model.myRatings = newsFeedService.GetRatingByUserId(User.Identity.GetUserId());
            model.myId = User.Identity.GetUserId();

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(Status userStatus)
        {
            var newsFeedService = new NewsfeedService();
            string theUserId = User.Identity.GetUserId();
            newsFeedService.PostStatus(theUserId, userStatus);

            return RedirectToAction("Index");
        }

		public ActionResult GetProfile(string Id)
		{
			var service = new ProfileService();
			var newsFeedService = new NewsfeedService();

			var model = service.getProfile(Id);
			model.myRatings = newsFeedService.GetRatingByUserId(User.Identity.GetUserId());
			model.myId = User.Identity.GetUserId();

			return View("Index", model);
		}
    }
}