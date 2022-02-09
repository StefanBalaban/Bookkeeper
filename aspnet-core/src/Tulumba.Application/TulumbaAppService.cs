using Tulumba.Localization;
using Volo.Abp.Application.Services;

namespace Tulumba
{
    /* Inherit your application services from this class.
     */
    public abstract class TulumbaAppService : ApplicationService
    {
        protected TulumbaAppService()
        {
            LocalizationResource = typeof(TulumbaResource);
        }
    }
}