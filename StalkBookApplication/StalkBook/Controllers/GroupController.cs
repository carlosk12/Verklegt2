using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StalkBook.Models;
using StalkBook.Service;

namespace StalkBook.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private GroupService service = new GroupService();
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Group groupName)
        {
            string theUserId = User.Identity.GetUserId();
            service.CreateGroup(theUserId, groupName);

            return RedirectToAction("Index");
        }
    }
}