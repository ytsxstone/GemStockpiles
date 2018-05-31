using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace JFJT.GemStockpiles.Localization
{
    public static class GemStockpilesLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(GemStockpilesConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(GemStockpilesLocalizationConfigurer).GetAssembly(),
                        "JFJT.GemStockpiles.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
