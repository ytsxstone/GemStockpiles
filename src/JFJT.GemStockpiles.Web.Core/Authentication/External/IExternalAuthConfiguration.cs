using System.Collections.Generic;

namespace JFJT.GemStockpiles.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
