using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure.Core;
using IoTSharp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Verdure.Qinglan;
using WebApiClientCore.Extensions.OAuths;
using WebApiClientCore.Extensions.OAuths.TokenProviders;

namespace IoTSharp.Services
{
    public class QinglanCustomTokenProvider : TokenProvider
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        public QinglanCustomTokenProvider(IServiceProvider serviceProvider,
            ApplicationDbContext context,
            HttpClient httpClient)
            : base(serviceProvider)
        {
            _context = context;
            _httpClient = httpClient;
        }

        protected override async Task<TokenResult> RequestTokenAsync(IServiceProvider serviceProvider)
        {
            var tokenApi = serviceProvider.GetService<IQinglanTokenApi>();

            var options = serviceProvider.GetService<IOptions<QinglanAccountOptions>>();

            var input = new LoginInput
            {
                Username = options.Value.UserName,
                Password = options.Value.Password
            };

            var qinglanToken = _context.GetQinglanToken();

            if (qinglanToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", qinglanToken.AccessToken);

                var deptTree = await _httpClient.GetAsync("https://qinglanst.com/prod-api/system/user/deptTree");

                if (deptTree.StatusCode == HttpStatusCode.OK)
                {
                    return new TokenResult
                    {
                        Access_token = qinglanToken.AccessToken,
                        Refresh_token = qinglanToken.RefreshToken,
                        Expires_in = qinglanToken.ExpiresIn
                    };
                }

                var token = await tokenApi.LoginAsync(input);

                if (token.Code == 200 && token.Data != null)
                {

                    qinglanToken.AccessToken = token.Data.access_token;
                    qinglanToken.RefreshToken = token.Data.refresh_token;
                    qinglanToken.CreateDate = DateTime.Now;
                    qinglanToken.Scope = token.Data.scope;
                    qinglanToken.ExpiresIn = token.Data.expires_in;
                    qinglanToken.TokenType = token.Data.token_type;
                
                    _context.QinglanTokens.Update(qinglanToken);
                    await _context.SaveChangesAsync();
                    return new TokenResult
                    {
                        Access_token = token.Data.access_token,
                        Refresh_token = token.Data.refresh_token,
                        Expires_in = token.Data.expires_in
                    };
                }
            }
            else
            {
                var token = await tokenApi.LoginAsync(input);

                if (token.Code == 200 && token.Data != null)
                {
                    var qingToken = new QinglanToken()
                    {
                        AccessToken = token.Data.access_token,
                        RefreshToken = token.Data.refresh_token,
                        CreateDate = DateTime.Now,
                        Scope = token.Data.scope,
                        ExpiresIn = token.Data.expires_in,
                        TokenType = token.Data.token_type
                    };
                    _context.QinglanTokens.Add(qingToken);
                    await _context.SaveChangesAsync();
                    return new TokenResult
                    {
                        Access_token = token.Data.access_token,
                        Refresh_token = token.Data.refresh_token,
                        Expires_in = token.Data.expires_in
                    };
                }
            }

            throw new Exception("获取token失败");
        }

        protected override Task<TokenResult> RefreshTokenAsync(IServiceProvider serviceProvider, string refresh_token)
        {
            return this.RequestTokenAsync(serviceProvider);
        }
    }
}
