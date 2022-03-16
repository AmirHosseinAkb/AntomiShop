﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Data.Entities.Permission;
using Antomi.Data.Entities.Product;
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
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Product>()
                .HasOne<ProductGroup>(p => p.ProductGroup)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne<ProductGroup>(p => p.SubGroup)
                .WithMany(p => p.SubProducts)
                .HasForeignKey(p => p.SubId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
