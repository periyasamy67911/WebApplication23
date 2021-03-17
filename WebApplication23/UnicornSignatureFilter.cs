using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebHooks.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23
{
    public class UnicornSignatureFilter : WebHookVerifySignatureFilter,
                                       IAsyncResourceFilter
    {
        private readonly byte[] _secret;
        public UnicornSignatureFilter(//IOptions<UnicornConfig> options,
                                      IConfiguration configuration,
                                      IHostingEnvironment hostingEnvironment,
                                      ILoggerFactory loggerFactory)
             : base(configuration, hostingEnvironment, loggerFactory)
        {
            //_secret = Encoding.UTF8.GetBytes(options.Value.SharedSecret);
            _secret = Encoding.UTF8.GetBytes("secret");
        }

        public override string ReceiverName => UnicornConstants.ReceiverName;

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context,
                                                   ResourceExecutionDelegate next)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (next == null) throw new ArgumentNullException(nameof(next));

            var request = context.HttpContext.Request;
            if (!HttpMethods.IsPost(request.Method))
            {
                await next();
                return;
            }

            var errorResult = EnsureSecureConnection(ReceiverName, request);
            if (errorResult != null)
            {
                context.Result = errorResult;
                return;
            }

            var header = GetRequestHeader(request,
                                          UnicornConstants.SignatureHeaderName,
                                          out errorResult);
            if (errorResult != null)
            {
                context.Result = errorResult;
                return;
            }

            byte[] payload;
            using (var ms = new MemoryStream())
            {
                HttpRequestRewindExtensions.EnableBuffering(request);
                await request.Body.CopyToAsync(ms);
                payload = ms.ToArray();
                request.Body.Position = 0;
            }

            if (payload == null || payload.Length == 0)
            {
                context.Result = new BadRequestObjectResult("No payload");
                return;
            }

            var digest = FromBase64(header, UnicornConstants.SignatureHeaderName);
            var secretPlusJson = _secret.Concat(payload).ToArray();

            using (var sha512 = new SHA512Managed())
            {
                if (!SecretEqual(sha512.ComputeHash(secretPlusJson), digest))
                {
                    context.Result =
                        new BadRequestObjectResult("Signature verification failed");
                    return;
                }
            }

            await next();
        }
    }
}
