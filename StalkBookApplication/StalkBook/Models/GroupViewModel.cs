using System;
using System.Collections.Generic;

namespace StalkBook.Models
{
    public class GroupViewModel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }
        public IEnumerable<Group> groups { get; set; }
        
    }
}