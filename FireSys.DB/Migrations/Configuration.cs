namespace FireSys.DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<FireSys.DB.FireSysModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FireSys.DB.FireSysModel context)
        {
            PasswordHasher hasher = new PasswordHasher();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.AspNetUsers.AddOrUpdate(
                new FireSys.Entities.AspNetUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin1",
                    PasswordHash = hasher.HashPassword("nimda1"),
                    Discriminator = "ApplicationUser",
                    SecurityStamp = ""
                });
        }
    }
}
