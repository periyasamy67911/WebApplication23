using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication23
{
    public static class UnicornServiceCollectionSetup
    {
        public static void AddUnicornServices(IServiceCollection services)
        {
            WebHookMetadata.Register<UnicornMetadata>(services);
            services.AddSingleton<UnicornSignatureFilter>();
        }
    }
}
