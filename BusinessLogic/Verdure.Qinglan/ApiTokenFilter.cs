using System;
using Microsoft.Extensions.Options;
using WebApiClientCore;

namespace Verdure.Qinglan
{
    public class ApiTokenFilter : IApiFilter
    {
        private readonly IOptions<QinglanAccountOptions> _options;

        private readonly IQinglanApi _qinglanApi;
        public ApiTokenFilter(IOptions<QinglanAccountOptions> options, IQinglanApi qinglanApi)
        {
            _options = options;
            _qinglanApi = qinglanApi;
        }
        public async Task OnRequestAsync(ApiRequestContext context)
        {
            var option = _options.Value;

            var result = await _qinglanApi.LoginAsync(new LoginInput { Username = option.UserName, Password = option.Password });
             
            if (result.Code == 200 && result.Data!=null)
            {

                context.HttpContext.RequestMessage.Headers.Add("Authorization", $"bearer {result.Data.access_token}");
            }
        }

        public Task OnResponseAsync(ApiResponseContext context)
        {
            return Task.CompletedTask;
        }
    }
}

