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
    public class StalkController : Controller
    {
		private StalkService service = new StalkService();
        public ActionResult Index()
        {
            return View();
        }
		[HttpPost]
		public ActionResult Stalk(string stalkId)
		{
			string theUserId = User.Identity.GetUserId();
			service.StalkUser(theUserId, stalkId);

			return new EmptyResult();
		}

		[HttpPost]
		public ActionResult StopStalkingUser(string stalkId)
		{
			string theUserId = User.Identity.GetUserId();
			service.StopStalkingUser(theUserId, stalkId);

			return new EmptyResult();
		}
    }
}