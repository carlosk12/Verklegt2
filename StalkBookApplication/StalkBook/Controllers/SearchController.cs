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
    public class SearchController : Controller
    {
        private SearchService service = new SearchService();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchString)
        {
            string theUserId = User.Identity.GetUserId();
            var model = service.Search(theUserId, searchString);

            return View(model);
        }

        public ActionResult SearchGroups(string searchString)
        {
            string theUserId = User.Identity.GetUserId();
            var model = service.SearchGroups(theUserId, searchString);

            return View(model);
        }
    }
}