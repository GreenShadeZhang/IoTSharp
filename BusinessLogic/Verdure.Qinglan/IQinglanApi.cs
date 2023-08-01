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

    /// <summary>
    /// 获取雷达设备列表
    /// </summary>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <param name="uid"></param>
    /// <param name="deptId"></param>
    /// <returns></returns>
    [OAuthToken]
    [HttpGet("prod-api/radar/equipment/list")]
    Task<RadarDeviceModel> GetRadarDeviceListAsync(int pageNum, int pageSize, string uid, int deptId = 258);

    /// <summary>
    /// 获取告警数据
    /// </summary>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <param name="alarmTime"></param>
    /// <param name="uid"></param>
    /// <param name="processingResult"></param>
    /// <returns></returns>
    [OAuthToken]
    [HttpGet("prod-api/radar/alarm/list")]
    Task<RadarAlarmModel> GetRadarAlarmListAsync(int pageNum, int pageSize, string? alarmTime = null, string? uid = null,
        int? processingResult = null);


    /// <summary>
    /// 获取微信用户列表
    /// </summary>
    /// <param name="pageNum"></param>
    /// <param name="pageSize"></param>
    /// <param name="mobile"></param>
    /// <param name="nickname"></param>
    /// <param name="slaveMobile"></param>
    /// <param name="deptId"></param>
    /// <returns></returns>
    [OAuthToken]
    [HttpGet("prod-api/member/wxuser/list")]
    Task<WeChatUserModel> GetWeChatUserListAsync(int pageNum, int pageSize, string? mobile = null, string? nickname = null,
        string? slaveMobile = null, int deptId = 258);
}

