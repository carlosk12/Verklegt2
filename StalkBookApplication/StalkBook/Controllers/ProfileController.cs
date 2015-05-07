using StalkBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StalkBook.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
			var service = new ProfileService();

			var model = service.getOwnProfile(User.Identity.GetUserId());

            return View(model);
        }
    }
}