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
        public int userId { get; set; }
        public int stalkersId { get; set; }
    }
}