using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Models
{
	public class GroupProfileFK
	{
		public int ID { get; set; }
		public int groupID { get; set; }
		public int profileID { get; set; }
	}
}