using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string name { get; set; }
        public DateTime timeCreated { get; set; }
    }
}