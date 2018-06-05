using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using JFJT.GemStockpiles.Authorization.Roles;
using JFJT.GemStockpiles.Authorization.Users;
using JFJT.GemStockpiles.MultiTenancy;
using JFJT.GemStockpiles.Models.Points;
using JFJT.GemStockpiles.Models.Products;
using JFJT.GemStockpiles.Models.Customers;

namespace JFJT.GemStockpiles.EntityFrameworkCore
{
    public class GemStockpilesDbContext : AbpZeroDbContext<Tenant, Role, User, GemStockpilesDbContext>
    {
        public GemStockpilesDbContext(DbContextOptions<GemStockpilesDbContext> options)
            : base(options)
        {

        }

        /* Define a DbSet for each entity of the application */

        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerCompany> CustomerCompany { get; set; }
        public DbSet<CustomerCompanyLicense> CustomerCompanyLicense { get; set; }

        public DbSet<PointRule> PointRule { get; set; }
        public DbSet<PointRank> PointRank { get; set; }

        public DbSet<Categorys> Category { get; set; }
        public DbSet<CategoryAttribute> CategoryAttribute { get; set; }
        public DbSet<CategoryAttributeItem> CategoryAttributeItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductMedia> ProductMedia { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValue { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //设置该字段非必输项
            modelBuilder.Entity<User>().Property(a => a.Surname).IsRequired(false);
            modelBuilder.Entity<User>().Property(a => a.EmailAddress).IsRequired(false);
        }
    }
}
