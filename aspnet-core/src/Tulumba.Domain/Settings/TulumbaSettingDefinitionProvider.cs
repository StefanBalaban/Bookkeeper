using Volo.Abp.Settings;

namespace Tulumba.Settings
{
    public class TulumbaSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(TulumbaSettings.MySetting1));
        }
    }
}