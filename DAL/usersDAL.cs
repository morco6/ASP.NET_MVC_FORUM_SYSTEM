using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using mvc_project1.Models;

//class for database connection
namespace mvc_project1.DAL
{
    //connection to users database
    public class usersDAL : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users");
        }

        public DbSet<User> Users { get; set; }
    }

    //connection to subjects database
    public class subsDAL : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<subject>().ToTable("SUBS");
        }

        public DbSet<subject> subject { get; set; }
    }

    //connection to global message database
    public class gblmDAL : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<gblm>().ToTable("GBLM");
        }

        public DbSet<gblm> gblm { get; set; }
    }

    //connection to comments database
    public class commDAL : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CommentsModel>().ToTable("COMMENTSS");
        }

        public DbSet<CommentsModel> comm { get; set; }
    }
}