using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StalkBook.Models
{
    public class StatusViewModel
    {
        public int ID { get; set; }
        public string userId { get; set; }
        public string body { get; set; }
        public DateTime timeCreated { get; set; }
        public string urlToPic { get; set; }
        public int upvotes { get; set; }
        public int downvotes { get; set; }
        public string fullName { get; set; }
        public IEnumerable<Status> availableStatuses { get; set; }

    }
}