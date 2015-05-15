using StalkBook.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StalkBook.Models
{
    public class GroupViewModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }
        public IEnumerable<Group> groups { get; set; }
        public IEnumerable<int> groupsJoined { get; set; }
        public string searchString { get; set; }
        public string myId { get; set; }
        public int groupId { get; set; }
        public IEnumerable<Status> groupStatuses { get; set; }
        public string body { get; set; }
        public string urlToPic { get; set; }
        public IEnumerable<UserStatusRating> myRatings { get; set; }
        public bool userIsInGroup { get; set; }
        
    }
}