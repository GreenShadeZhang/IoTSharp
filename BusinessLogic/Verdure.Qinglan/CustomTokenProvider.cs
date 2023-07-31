using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApiClientCore.Extensions.OAuths.TokenProviders;
using WebApiClientCore.Extensions.OAuths;

namespace Verdure.Qinglan
{
    public class CustomTokenProvider : TokenProvider
    {
        public CustomTokenProvider(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override async Task<TokenResult?> RequestTokenAsync(IServiceProvider serviceProvider)
        {
            var tokenApi = serviceProvider.GetService<IQinglanTokenApi>();

            var options = serviceProvider.GetService<IOptions<QinglanAccountOptions>>();

            var input = new LoginInput
            {
                Username = options.Value.UserName,
                Password = options.Value.Password
            };

            var token = await tokenApi.LoginAsync(input);

            if (token.Code == 200 && token.Data != null)
            {
                return new TokenResult
                {
                    Access_token = token.Data.access_token,
                    Refresh_token = token.Data.refresh_token,
                    Expires_in = token.Data.expires_in
                };
            }

            throw new Exception("获取token失败");
        }

        protected override Task<TokenResult?> RefreshTokenAsync(IServiceProvider serviceProvider, string refresh_token)
        {
            return this.RequestTokenAsync(serviceProvider);
        }
    }
}
