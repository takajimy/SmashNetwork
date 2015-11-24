namespace SmashNetworkPolymer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SmashNetworkPolymer.Areas.Users.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<SmashNetworkPolymer.DAL.SmashNetworkDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(SmashNetworkPolymer.DAL.SmashNetworkDBContext context)
        {
            PasswordManager pm = new PasswordManager("Takaji", "password");
            context.Users.AddOrUpdate(
                u => u.Username,
                new User() { Username = pm.username, Hash = pm.hash.getHashString(), Salt = pm.salt.getSaltString() }
            );
            context.SaveChanges();
        }
    }
}