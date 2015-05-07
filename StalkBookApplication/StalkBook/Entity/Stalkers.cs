using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StalkBook.Models
{
    public class Stalkers
    {
		public int ID { get; set; }
        public string userId { get; set; }
		public string stalkersId { get; set; }
    }
}