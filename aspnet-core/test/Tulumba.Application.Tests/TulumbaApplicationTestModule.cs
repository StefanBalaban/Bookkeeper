using Volo.Abp.Modularity;

namespace Tulumba
{
    [DependsOn(
        typeof(TulumbaApplicationModule),
        typeof(TulumbaDomainTestModule)
    )]
    public class TulumbaApplicationTestModule : AbpModule
    {
    }
}