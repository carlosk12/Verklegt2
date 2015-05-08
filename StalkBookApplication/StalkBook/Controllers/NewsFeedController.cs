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
        // GET: NewsFeed
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(Status userStatus)
        {
            var db = new ApplicationDbContext();

            string theUserId = User.Identity.GetUserId();

            //db.stalking.Add(new Stalking { userId = theUserId, stalkedId = "845a9a85-ea60-42d1-a33a-9b487f7d786e" });

            List<string> result1 = (from s in db.stalking
                           where s.userId == theUserId
                           select s.stalkedId.ToString()).ToList();

            var result = from us in db.userStatuses where result1.Contains(us.userId) select us;                           
         
            foreach (var item in result)
            {
                System.Diagnostics.Debug.WriteLine(item.body);
            }

            //someValues.ToList().ForEach(x => list.Add(x + 1));                              
                                         /*on p.userId equals s.userId
                                         where p.userId == s.stalkedId
                                         select p;*/

            foreach (var item in result)
            {
                //System.Diagnostics.Debug.WriteLine(item.body);
            }

            userStatus.userId = User.Identity.GetUserId();
            userStatus.timeCreated = System.DateTime.Now;
            db.userStatuses.Add(userStatus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}