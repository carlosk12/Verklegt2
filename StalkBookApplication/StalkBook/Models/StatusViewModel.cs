using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using StalkBook.Entity;

namespace StalkBook.Models
{
    public class StatusViewModel
    {
        public int ID { get; set; }
        public string userId { get; set; }
		[Required]
		[StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
		public string body { get; set; }
        public DateTime timeCreated { get; set; }
        public string urlToPic { get; set; }
        public int upvotes { get; set; }
        public int downvotes { get; set; }
        public string fullName { get; set; }
        public IEnumerable<Status> availableStatuses { get; set; }
        public IEnumerable<UserStatusRating> myRatings { get; set; }

    }
}