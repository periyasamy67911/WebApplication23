using Microsoft.AspNetCore.WebHooks;

namespace WebApplication23
{
    public class UnicornWebHookAttribute : WebHookAttribute
    {
        public UnicornWebHookAttribute() : base(UnicornConstants.ReceiverName)
        {
        }
    }
}
