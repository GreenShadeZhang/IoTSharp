using System;
using WebApiClientCore;

namespace Verdure.Qinglan
{
    public class ApiTokenFilter : IApiFilter
    {
        public Task OnRequestAsync(ApiRequestContext context)
        {
            context.HttpContext.RequestMessage.Headers.Add("Authorization", "bearer c64b1dd6-d725-4de8-8f3f-e7ee887facc7");

            return Task.CompletedTask;
        }

        public Task OnResponseAsync(ApiResponseContext context)
        {
            return Task.CompletedTask;
        }
    }
}

