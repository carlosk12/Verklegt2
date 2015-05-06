using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace StalkBook.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		//public string email { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Status> userStatuses { get; set; }
		public DbSet<User> userProfile { get; set; }
		public DbSet<Group> groups { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}