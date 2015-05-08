using StalkBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StalkBook.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
		public ActionResult MyProfile()
        {
			var service = new ProfileService();

			var model = service.getProfile(User.Identity.GetUserId());

            return View("Index", model);
        }

		public ActionResult GetProfile(int Id)
		{
			var service = new ProfileService();

			var model = service.getProfile(Id);

			return View("Index", model);
		}
    }
}