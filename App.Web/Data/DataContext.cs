using App.Web.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
       
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<ItemTypeEntity> ItemTypes { get; set; }
        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<PurchaseEntity> Purchases { get; set; }
       
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
             
        }
    }
}
