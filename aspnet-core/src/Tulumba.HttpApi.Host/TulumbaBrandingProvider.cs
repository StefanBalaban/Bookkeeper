using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Tulumba
{
    [Dependency(ReplaceServices = true)]
    public class TulumbaBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Tulumba";
    }
}