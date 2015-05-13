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

    public interface IAppDataContext
    {
        IDbSet<Status> userStatuses { get; set; }
        IDbSet<Group> groups { get; set; }
        IDbSet<Profile> profiles { get; set; }
        IDbSet<GroupProfileFK> groupProfileFks { get; set; }
        IDbSet<Stalking> stalking { get; set; }
        IDbSet<UserStatusRating> userStatusRating { get; set; }
        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
		public IDbSet<Status> userStatuses{	get; set; }
		public IDbSet<Group> groups { get; set; }
		public IDbSet<Profile> profiles { get; set; }
		public IDbSet<GroupProfileFK> groupProfileFks { get; set; }
		public IDbSet<Stalking> stalking { get; set; }
        public IDbSet<UserStatusRating> userStatusRating { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}