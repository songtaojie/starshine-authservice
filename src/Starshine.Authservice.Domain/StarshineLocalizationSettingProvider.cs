using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization.Resources.AbpLocalization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Starshine.Authservice.Domain
{

    public class StarshineLocalizationSettingProvider : LocalizationSettingProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(LocalizationSettingNames.DefaultLanguage,
                    "zh-Hans",
                    L("DisplayName:Abp.Localization.DefaultLanguage"),
                    L("Description:Abp.Localization.DefaultLanguage"),
                    isVisibleToClients: true)
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpLocalizationResource>(name);
        }
    }

}
