using Verdure.Qinglan.Models;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace Verdure.Qinglan;

[HttpHost("https://qinglanst.com/")]
public interface IQinglanApi : IHttpApi
{
    [HttpPost("prod-api/login")]
    Task<Result<QinglanTokenModel>> LoginAsync([JsonContent] LoginInput input);

    [OAuthToken]
    [HttpPost("prod-api/thirdparty/population")]
    Task<object> GetPopulationAsync([JsonContent] List<string> uids);

    [HttpPost("prod-api/thirdparty/online")]
    Task<object> GetOnlineAsync([JsonContent] List<string> uids);

    [OAuthToken]
    [HttpGet("prod-api/radar/equipment/list")]
    Task<RadarDeviceModel> GetRadarDeviceListAsync(int pageNum, int pageSize, string uid, int deptId = 258);
}

