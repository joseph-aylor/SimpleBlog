using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class BlogContext : DbContext
    {
        // Use the default connection, as app grows we will need
        // to have more connections
        public BlogContext() : base("DefaultConnection")
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}