﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StalkBook.Models
{
	public class User
	{
		public int ID { get; set; }
		public string name { get; set; }
		public DateTime creationDate { get; set; }
		/*public List<int> stalkersIDs{ get; set; }
		public List<int> stalkingIDs{ get; set; }
		public List<int> groupsIDs{ get; set; }*/
	}
}