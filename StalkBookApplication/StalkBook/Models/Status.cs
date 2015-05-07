using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StalkBook.Models
{
	public class Status
	{
		public int ID { get; set; }
		public int userId { get; set; }
		public string body { get; set; }
		public DateTime timeCreated { get; set; }
		public string urlToPic { get; set; }
		public int upvotes { get; set; }
		public int downvotes { get; set; }

	}
}