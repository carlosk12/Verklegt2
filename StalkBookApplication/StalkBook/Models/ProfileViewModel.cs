using StalkBook.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Models
{
    public class ProfileViewModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }
        public string userID { get; set; }
		public string profilePicUrl { get; set; }
        public List<Status> userStatuses { get; set; }
        public IEnumerable<UserStatusRating> myRatings { get; set; }
        public string  body { get; set; }
        public string urlToPic { get; set; }
        public string myId { get; set; }
		public List<string> stalking { get; set; }
    }
}