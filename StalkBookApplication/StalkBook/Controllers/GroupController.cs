using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StalkBook.Models;
using StalkBook.Service;
using StalkBook.Entity;

namespace StalkBook.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private GroupService service = new GroupService(null);
        private NewsfeedService newsFeedService = new NewsfeedService(null);
        private ProfileService profileService = new ProfileService(null);
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

        public ActionResult GetGroupById(int groupId)
        {
            string theUserId = User.Identity.GetUserId();
            var model = service.GetGroupById(groupId, theUserId);
            model.myId = theUserId;
            model.myRatings = newsFeedService.GetRatingByUserId(User.Identity.GetUserId());
           
        

            return View("Group", model);
        }

        public ActionResult Delete(int groupID)
        {
            string theUserId = User.Identity.GetUserId();
            service.DeleteGroup(groupID);

            return RedirectToAction("Index");
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

        [HttpPost]
        public ActionResult PostStatus(Status status)
        {
            string theUserId = User.Identity.GetUserId();
            service.PostStatus(theUserId, status);

            var model = service.GetGroupById((int)status.groupId, theUserId);
            model.myId = theUserId;
            model.myRatings = newsFeedService.GetRatingByUserId(User.Identity.GetUserId());
            return View("Group", model);
        }

        public ActionResult DeleteStatus(int Id, int groupId)
        {
            profileService.DeleteStatus(Id);      
            string theUserId = User.Identity.GetUserId();
            var model = service.GetGroupById(groupId, theUserId);
            model.myId = theUserId;
            model.myRatings = newsFeedService.GetRatingByUserId(User.Identity.GetUserId());

            return View("Group", model);
        }
    }
}