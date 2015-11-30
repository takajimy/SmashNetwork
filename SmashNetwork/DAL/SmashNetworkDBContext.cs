using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using SmashNetwork.Areas.Users.Models;

namespace SmashNetwork.DAL
{
    public class SmashNetworkDBContext : DbContext
    {
        public SmashNetworkDBContext()
        {
            Database.SetInitializer<SmashNetworkDBContext>(null);
        }

        // User Authentication tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
