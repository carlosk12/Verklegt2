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
            var model = new GroupViewModel();
            model.groups = service.GetGroupsByUserId(User.Identity.GetUserId());

            return View(model);
        }

        [HttpPost]
        public ActionResult Join(int groupId)
        {
            string theUserId = User.Identity.GetUserId();
            service.AddUserToGroup(theUserId, groupId);

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Leave(int groupId)
        {
            string theUserId = User.Identity.GetUserId();
            service.RemoveUserFromGroup(theUserId, groupId);

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Delete(Group group)
        {
            string theUserId = User.Identity.GetUserId();
            service.DeleteGroup(theUserId, group);

            return RedirectToAction("Join");
        }

        [HttpPost]
        public ActionResult Index(Group groupName)
        {
            if (string.IsNullOrEmpty(groupName.name))
            {
                RedirectToAction("Index");
            }
            else
            {
                string theUserId = User.Identity.GetUserId();
                service.CreateGroup(theUserId, groupName);
            }
            

            return RedirectToAction("Index");
        }

        /*[HttpPost]
        public ActionResult AddUser(Group groupId)
        {
            string userId = User.Identity.GetUserId();
            service.AddUserToGroup(userId, groupId);

            return RedirectToAction("Index");
        }*/
    }
}