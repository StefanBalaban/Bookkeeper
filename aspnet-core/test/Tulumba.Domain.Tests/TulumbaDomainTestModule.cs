using Tulumba.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tulumba
{
    [DependsOn(
        typeof(TulumbaEntityFrameworkCoreTestModule)
    )]
    public class TulumbaDomainTestModule : AbpModule
    {
    }
}