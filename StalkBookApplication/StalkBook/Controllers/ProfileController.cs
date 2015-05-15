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
		private ProfileService service = new ProfileService(null);
		private NewsfeedService newsFeedService = new NewsfeedService(null);

		public ActionResult Index()
        {
			var model = service.GetProfile(User.Identity.GetUserId());
            model.myRatings = newsFeedService.GetRatingByUserId(User.Identity.GetUserId());
            model.myId = User.Identity.GetUserId();

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(Status userStatus)
        {
            string theUserId = User.Identity.GetUserId();
            newsFeedService.PostStatus(theUserId, userStatus);

            return RedirectToAction("Index");
        }

		public ActionResult GetProfile(string Id)
		{
			var model = service.GetProfile(Id);
			model.myRatings = newsFeedService.GetRatingByUserId(User.Identity.GetUserId());
			model.myId = User.Identity.GetUserId();

			return View("Index", model);
		}

		public ActionResult DeleteStatus(int Id)
		{
			service.DeleteStatus(Id);

			return RedirectToAction("Index");
		}

		public ActionResult Stalking(string Id)
		{
            var theUserId = User.Identity.GetUserId();
			var model = service.ViewStalking(Id, theUserId);
            model.ElementAt(0).myId = theUserId;
            
			return View("Stalking", model);
		}

		public ActionResult Stalkers(string Id)
		{
            var theUserId = User.Identity.GetUserId();
			var model = service.ViewStalkers(Id, theUserId);
            model.ElementAt(0).myId = theUserId;
            
			return View("Stalking", model);
		}
    }
}