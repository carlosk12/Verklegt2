using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StalkBook.Models
{
    public class Stalking
    {
        public int userId { get; set; }
        public int stalkedId { get; set; }
    }
}