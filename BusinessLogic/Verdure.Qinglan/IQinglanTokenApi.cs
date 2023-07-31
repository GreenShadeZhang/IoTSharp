using Verdure.Qinglan.Models;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace Verdure.Qinglan;

[HttpHost("https://qinglanst.com/")]
public interface IQinglanTokenApi : IHttpApi
{
    [HttpPost("prod-api/login")]
    Task<Result<QinglanTokenModel>> LoginAsync([JsonContent] LoginInput input);
}

