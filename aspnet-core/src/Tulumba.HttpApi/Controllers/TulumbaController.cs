using Tulumba.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tulumba.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TulumbaController : AbpController
    {
        protected TulumbaController()
        {
            LocalizationResource = typeof(TulumbaResource);
        }
    }
}