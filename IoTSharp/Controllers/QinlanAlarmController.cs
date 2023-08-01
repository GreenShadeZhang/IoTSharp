using IoTSharp.Contracts;
using IoTSharp.Controllers.Models;
using IoTSharp.Data;
using IoTSharp.Data.Extensions;
using IoTSharp.Extensions;
using IoTSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShardingCore.Extensions;
using Verdure.Qinglan;

namespace IoTSharp.Controllers
{
    /// <summary>
    /// 告警管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QinlanAlarmController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IQinglanApi _qinglanApi;
        public QinlanAlarmController(ApplicationDbContext context, IQinglanApi qinglanApi)
        {
            _context = context;
            _qinglanApi = qinglanApi;
        }

        /// <summary>
        /// 创建告警， 但不触发规则链。要触发规则链， 请使用设备相关的API
        /// </summary>
        /// <param name="dto">告警内容</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Occurred([FromBody] CreateAlarmDto dto)
        {
            var oname = dto.OriginatorName;
            return await _context.OccurredAlarm(dto, _alarm =>
            {
                Guid originator = Guid.Empty;
                switch (dto.OriginatorType)
                {
                    case OriginatorType.Device:
                        originator =
                            _context.Device.FirstOrDefault(d => (d.Id.ToString() == oname || d.Name == oname) && d.Deleted == false)?.Id ??
                            Guid.Empty;
                        break;

                    case OriginatorType.Gateway:
                        originator =
                            _context.Gateway.FirstOrDefault(d => (d.Id.ToString() == oname || d.Name == oname) && d.Deleted == false)?.Id ??
                            Guid.Empty;
                        break;

                    case OriginatorType.Asset:
                        originator =
                            _context.Assets.FirstOrDefault(d => (d.Id.ToString() == oname || d.Name == oname) && d.Deleted == false)?.Id ??
                            Guid.Empty;
                        break;

                    case OriginatorType.Unknow:
                    default:
                        break;
                }

                _alarm.OriginatorId = originator;
                _context.JustFill(this, _alarm);
            });
        }

        /// <summary>
        /// 查询告警信息
        /// </summary>
        /// <param name="m">指定OriginatorId时需要填写OriginatorType</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<PagedData<AlarmDto>>> List([FromBody] AlarmParam m)
        {
            var profile = this.GetUserProfile();

            m.Limit = m.Limit < 5 ? 5 : m.Limit;
            try
            {
                var list = new List<AlarmDto>();

                var alarmList = await _qinglanApi.GetRadarAlarmListAsync(m.Offset, m.Limit);

                if (alarmList.Code == 200)
                {
                    foreach (var alarm in alarmList.Rows)
                    {
                        var alarmObj = new AlarmDto()
                        {
                            AlarmId = alarm.Id,
                            AlarmTime = alarm.AlarmTime,
                            AlarmMsg = alarm.AlarmMsg,
                            EqtTypeId = alarm.EqtTypeId,
                            AlarmDetail = alarm.AlarmMsg,
                            Uid = alarm.Uid,
                            EqtName = alarm.EqtName,
                            ProcessingResult = alarm.ProcessingResult,
                            ProcessingOpinions = alarm.ProcessingOpinions,
                            ProcessingPeople = alarm.ProcessingPeople,
                            ProcessingTime = alarm.ProcessingTime,
                            DeptId = alarm.DeptId
                        };
                        list.Add(alarmObj);
                    }

                    return new ApiResult<PagedData<AlarmDto>>(ApiCode.Success, "OK", new PagedData<AlarmDto>
                    {
                        total = alarmList.Total,
                        rows = list
                    });
                }

            }
            catch (Exception e)
            {
                return new ApiResult<PagedData<AlarmDto>>(ApiCode.Exception, e.Message, null);
            }

            return new ApiResult<PagedData<AlarmDto>>(ApiCode.Success, "OK", new PagedData<AlarmDto>
            {
                total = 0,
                rows = new List<AlarmDto>()
            });
        }



        private object GetOriginator(Alarm Alarm)
        {
            switch (Alarm.OriginatorType)
            {
                case OriginatorType.Unknow:

                    break;
                case OriginatorType.Device:
                    return _context.Device.SingleOrDefault(c => c.Id == Alarm.OriginatorId);

                case OriginatorType.Gateway:
                    return _context.Device.SingleOrDefault(c => c.Id == Alarm.OriginatorId);

                case OriginatorType.Asset:
                    return _context.Assets.SingleOrDefault(c => c.Id == Alarm.OriginatorId);

            }

            return null;
        }

        /// <summary>
        /// 搜索告警发起对象
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ModelOriginatorItem>>> Originators([FromBody] ModelOriginatorSearch m)
        {
            var profile = this.GetUserProfile();
            switch ((OriginatorType)m.OriginatorType)
            {
                case OriginatorType.Unknow:
                default:
                case OriginatorType.Device:
                    return new ApiResult<List<ModelOriginatorItem>>(ApiCode.Success, "OK", await _context.Device
                        .Where(c => c.Name.Contains(m.OriginatorName) && c.DeviceType == DeviceType.Device &&
                                    c.Customer.Id == profile.Customer && c.Tenant.Id == profile.Tenant)
                        .Select(c => new ModelOriginatorItem { Id = c.Id, Name = c.Name }).ToListAsync());

                case OriginatorType.Gateway:
                    return new ApiResult<List<ModelOriginatorItem>>(ApiCode.Success, "OK", await _context.Device
                        .Where(c => c.Name.Contains(m.OriginatorName) && c.DeviceType == DeviceType.Gateway &&
                                    c.Customer.Id == profile.Customer && c.Tenant.Id == profile.Tenant)
                        .Select(c => new ModelOriginatorItem { Id = c.Id, Name = c.Name }).ToListAsync());
                    ;
                case OriginatorType.Asset:
                    return new ApiResult<List<ModelOriginatorItem>>(ApiCode.Success, "OK",
                        await _context.Assets
                            .Where(c => c.Name.Contains(m.OriginatorName) && c.Customer.Id == profile.Customer &&
                                        c.Tenant.Id == profile.Tenant).Select(c => new ModelOriginatorItem
                                        { Id = c.Id, Name = c.Name }).ToListAsync());
            }

        }

        /// <summary>
        /// 确认告警
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AckAlarm([FromBody] AlarmStatusDto m)
        {

            var alarm = await _context.Alarms.SingleOrDefaultAsync(c => c.Id == m.Id);
            if (alarm != null)
            {
                if (alarm.AlarmStatus == AlarmStatus.Active_UnAck)
                {
                    alarm.AlarmStatus = AlarmStatus.Active_Ack;
                    alarm.AckDateTime = DateTime.UtcNow;
                }

                if (alarm.AlarmStatus == AlarmStatus.Cleared_UnAck)
                {
                    alarm.AlarmStatus = AlarmStatus.Cleared_Act;
                    alarm.AckDateTime = DateTime.UtcNow;

                }
                _context.Alarms.Update(alarm);
                await _context.SaveChangesAsync();
                return new ApiResult<bool>(ApiCode.Success, "Alarm acknowledged", true);

            }



            return new ApiResult<bool>(ApiCode.CantFindObject, "Not found alarm", false);
        }


        /// <summary>
        /// 清除告警信息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ClearAlarm([FromBody] AlarmStatusDto m)
        {

            var alarm = await _context.Alarms.SingleOrDefaultAsync(c => c.Id == m.Id);
            if (alarm != null)
            {
                if (alarm.AlarmStatus == AlarmStatus.Active_Ack)
                {
                    alarm.AlarmStatus = AlarmStatus.Cleared_Act;
                    alarm.ClearDateTime = DateTime.UtcNow;

                }
                if (alarm.AlarmStatus == AlarmStatus.Active_UnAck)
                {
                    alarm.AlarmStatus = AlarmStatus.Active_Ack;
                    alarm.ClearDateTime = DateTime.UtcNow;

                }
                _context.Alarms.Update(alarm);
                await _context.SaveChangesAsync();
                return new ApiResult<bool>(ApiCode.Success, "Alarm cleared", true);

            }
            return new ApiResult<bool>(ApiCode.CantFindObject, "Not found alarm", false);
        }
    }
}