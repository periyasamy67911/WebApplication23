using Microsoft.AspNetCore.WebHooks;
using Microsoft.AspNetCore.WebHooks.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication23
{
    public interface IWebHookFilterMetadata : IWebHookMetadata, IWebHookReceiver
    {
        /// <summary>
        /// Add <see cref="IFilterMetadata"/> instances to <see cref="WebHookFilterMetadataContext.Results"/> of
        /// <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The <see cref="WebHookFilterMetadataContext"/> for the action.</param>
        /// <remarks>
        /// Added filters should not check applicability before executing e.g. no need to get the receiver name from
        /// <see cref="RouteData"/> or to call the filter's own <see cref="IWebHookReceiver.IsApplicable"/> method
        /// within <see cref="IResourceFilter.OnResourceExecuting"/>.
        /// </remarks>
        void AddFilters(WebHookFilterMetadataContext context);
    }
}
