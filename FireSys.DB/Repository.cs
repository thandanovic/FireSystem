using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSys.Entities;

namespace FireSys.DB
{
    public class Repository : DbContext
    {
        public Repository() : base("CaWeDb")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
