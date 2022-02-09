using Tulumba.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Tulumba.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(TulumbaEntityFrameworkCoreModule),
        typeof(TulumbaApplicationContractsModule)
    )]
    public class TulumbaDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}