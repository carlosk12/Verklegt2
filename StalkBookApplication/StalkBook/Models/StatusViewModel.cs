using StalkBook.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Models
{
	public class StatusViewModel
	{
		public Status status { get; set; }
		public IEnumerable<Comment> comments { get; set; }
	}
}