using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Data.Entities.Permission;
using Antomi.Data.Entities.User;
using Antomi.Data.Entities.Wallet;
using Microsoft.EntityFrameworkCore;

namespace Antomi.Data.Context
{
    public class AntomiContext:DbContext
    {
        public AntomiContext(DbContextOptions<AntomiContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
