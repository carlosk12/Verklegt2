﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StalkBook.Entity
{
	public class UserStatusRating
	{
		// StatusViewModel með List af UserStatusRating þar sem userId == User.Identity.GetId.
		public int ID { get; set; }
		public string userId { get; set; }
		public int statusId { get; set; }
		public int rating { get; set; }
	}
}