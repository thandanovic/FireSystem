namespace FireSys.DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<FireSys.DB.Repository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FireSys.DB.Repository context)
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

            context.Users.AddOrUpdate(
                new Entities.User() { Email = "admin@gmail.com", Pass = hasher.HashPassword("nimda"), CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now }
                );

            context.Roles.AddOrUpdate(
                 new Entities.Role() { Name = "admin", Description = "zestoki momak" }
                );
            context.UserRoles.AddOrUpdate(
                new Entities.UserRole() { UserId = 1, RoleId = 1 });

        }
    }
}
