using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StalkBook.Models;

namespace StalkBook.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Group group)
        {
            var db = new ApplicationDbContext();
            group.name = group.name;
            group.timeCreated = DateTime.Now;
            db.groups.Add(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}