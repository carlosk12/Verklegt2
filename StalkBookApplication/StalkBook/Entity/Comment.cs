using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Entity
{
	public class Comment
	{
		public int ID { get; set; }
		public string body { get; set; }
		public DateTime timeCreated { get; set; }
		public int statusID { get; set; }
	}
}