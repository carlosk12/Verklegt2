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
        public List<Status> userStatuses { get; set; }
    }
}