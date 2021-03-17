using Microsoft.AspNetCore.WebHooks.Metadata;

namespace WebApplication23
{
    public class UnicornMetadata : WebHookMetadata, IWebHookFilterMetadata
    {
        private readonly UnicornSignatureFilter _verifySignatureFilter;

        public UnicornMetadata(UnicornSignatureFilter verifySignatureFilter)
            : base(UnicornConstants.ReceiverName)
        {
            _verifySignatureFilter = verifySignatureFilter;
        }
        public override WebHookBodyType BodyType => WebHookBodyType.Json;

        public void AddFilters(WebHookFilterMetadataContext context)
        {
            context.Results.Add(_verifySignatureFilter);
        }
    }
}
