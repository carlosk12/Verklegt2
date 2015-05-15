using StalkBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace StalkBook.Service
{
    public class StalkService
    {
        private readonly IAppDataContext db;

        public StalkService(IAppDataContext context)
        {
            db = context ?? new ApplicationDbContext();
        }

        public void StalkUser(string userId, string stalkId)
        {
            try
            {
                if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(stalkId))
                {
                    var stalking = new Stalking();
                    stalking.userId = userId;
                    stalking.stalkedId = stalkId;
                    db.stalking.Add(stalking);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
            }
        }

        public void StopStalkingUser(string userId, string stalkId)
        {
            try
            {
                if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(stalkId))
                {
                    var stalking = (from s in db.stalking
                                    where s.userId == userId
                                    where s.stalkedId == stalkId
                                    select s).FirstOrDefault();
                    db.stalking.Remove(stalking);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message + Environment.NewLine + "Location of error: " + ex.Source +
                    Environment.NewLine + "StackTrace: " + Environment.NewLine + ex.StackTrace +
                    Environment.NewLine + "Time : " + DateTime.Now + Environment.NewLine);
            }
        }
    }
}