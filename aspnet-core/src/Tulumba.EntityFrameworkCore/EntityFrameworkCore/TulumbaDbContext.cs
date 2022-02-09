using Microsoft.EntityFrameworkCore;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.DailyEarning;
using Tulumba.Entities.Employee;
using Tulumba.Entities.Expense;
using Tulumba.Entities.ExpenseType;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Entities.Shop;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Tulumba.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class TulumbaDbContext :
        AbpDbContext<TulumbaDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        public TulumbaDbContext(DbContextOptions<TulumbaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DailyEarning> DailyEarnings { get; set; }
        public DbSet<MonthlyCashFlow> MonthlyCashFlows { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<DailyCashFlow> DailyCashFlows { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */

            builder.Entity<Shop>(b =>
            {
                b.ToTable(TulumbaConsts.DbTablePrefix + "Shops",
                    TulumbaConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
            builder.Entity<Employee>(b =>
            {
                b.ToTable(TulumbaConsts.DbTablePrefix + "Employees",
                    TulumbaConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
            builder.Entity<DailyEarning>(b =>
            {
                b.ToTable(TulumbaConsts.DbTablePrefix + "DailyEarnings",
                    TulumbaConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ShopId).IsRequired();
            });
            builder.Entity<MonthlyCashFlow>(b =>
            {
                b.ToTable(TulumbaConsts.DbTablePrefix + "MonthlyCashFlows",
                    TulumbaConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ShopId).IsRequired();
            });
            builder.Entity<ExpenseType>(b =>
            {
                b.ToTable(TulumbaConsts.DbTablePrefix + "ExpenseTypes",
                    TulumbaConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired();
                b.Property(x => x.ExpenseCategory).IsRequired();
                b.Property(x => x.ExpensePeriod).IsRequired();
            });

            builder.Entity<Expense>(b =>
            {
                b.ToTable(TulumbaConsts.DbTablePrefix + "Expenses",
                    TulumbaConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Date).IsRequired();
                b.Property(x => x.ShopId).IsRequired();
                b.Property(x => x.ExpenseTypeId).IsRequired();
            });

                        builder.Entity<DailyCashFlow>(b =>
            {
                b.ToTable(TulumbaConsts.DbTablePrefix + "DailyCashFlows",
                    TulumbaConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Date).IsRequired();
                b.Property(x => x.ShopId).IsRequired();
            });
        }
        /* Add DbSet properties for your Aggregate Roots / Entities here. */

        #region Entities from the modules

        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        //Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion
    }
}