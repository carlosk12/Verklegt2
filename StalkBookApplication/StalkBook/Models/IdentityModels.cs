using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using StalkBook.Entity;

namespace StalkBook.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		public string fullName { get; set; }
		public DateTime dateCreated { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<Status> userStatuses{	get; set; }
        public DbSet<GroupStatus> groupStatuses { get; set; }
		public DbSet<Group> groups { get; set; }
		public DbSet<Profile> profiles { get; set; }
		public DbSet<GroupProfileFK> groupProfileFks { get; set; }
		public DbSet<Stalking> stalking { get; set; }
        public DbSet<UserStatusRating> userStatusRating { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<StalkBook.Models.GroupViewModel> GroupViewModels { get; set; }
    }
}