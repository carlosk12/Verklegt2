using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeDbSet;
using System.Data.Entity;
using StalkBook.Models;
using StalkBook.Entity;

namespace Stalkbook.Tests
{
    /// <summary>
    /// This is an example of how we'd create a fake database by implementing the 
    /// same interface that the BookeStoreEntities class implements.
    /// </summary>
    public class MockDatabase : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDatabase()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            this.userStatuses = new InMemoryDbSet<Status>();
            this.groups = new InMemoryDbSet<Group>();
            this.profiles = new InMemoryDbSet<Profile>();
            this.groupProfileFks = new InMemoryDbSet<GroupProfileFK>();
            this.stalking = new InMemoryDbSet<Stalking>();
            this.userStatusRating = new InMemoryDbSet<UserStatusRating>();
        }

        public IDbSet<Status> userStatuses { get; set; }
        public IDbSet<Group> groups { get; set; }
        public IDbSet<Profile> profiles { get; set; }
        public IDbSet<GroupProfileFK> groupProfileFks { get; set; }
        public IDbSet<Stalking> stalking { get; set; }
        public IDbSet<UserStatusRating> userStatusRating { get; set; }

        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;
            //changes += DbSetHelper.IncrementPrimaryKey<Author>(x => x.AuthorId, this.Authors);
            //changes += DbSetHelper.IncrementPrimaryKey<Book>(x => x.BookId, this.Books);

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}