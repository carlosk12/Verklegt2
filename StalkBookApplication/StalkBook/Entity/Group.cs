using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StalkBook.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string name { get; set; }
        public DateTime timeCreated { get; set; }
        public string ownerId { get; set; }
    }
}