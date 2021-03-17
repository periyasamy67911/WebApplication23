using Microsoft.Extensions.DependencyInjection;

namespace WebApplication23
{
    public static class UnicornMvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddUnicornWebHooks(this IMvcCoreBuilder builder)
        {
            UnicornServiceCollectionSetup.AddUnicornServices(builder.Services);
            return builder.AddWebHooks();
        }
    }
}
