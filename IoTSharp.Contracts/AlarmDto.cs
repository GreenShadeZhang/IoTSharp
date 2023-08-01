 
using System;

namespace IoTSharp.Contracts
{
    public class DeviceAlarmDto
    {
        /// <summary>
        /// 起因设备的名称或者GUID
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        public string OriginatorName { get; set; }

        /// <summary>
        /// 警告类型
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        public string AlarmType { get; set; }

        /// <summary>
        /// 警告细节描述
        /// </summary>
        public string AlarmDetail { get; set; }

        /// <summary>
        /// 严重程度
        /// </summary>
        public ServerityLevel Serverity { get; set; } = ServerityLevel.Indeterminate;


        public Guid warnDataId { get; set; }
    }

    public class CreateAlarmDto : DeviceAlarmDto
    {

        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 起因设备类型
        /// </summary>
        public OriginatorType OriginatorType { get; set; }
    }

    public class AlarmDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 警告类型
        /// </summary>
        public string AlarmType { get; set; }

        /// <summary>
        /// 警告细节描述
        /// </summary>
        public string AlarmDetail { get; set; }

        /// <summary>
        /// 警告创建时间
        /// </summary>
        public DateTime AckDateTime { get; set; }

        /// <summary>
        /// 手动清除时间
        /// </summary>
        public DateTime ClearDateTime { get; set; }

        /// <summary>
        /// 警告持续的开始时间
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 结束时间， 当警告自动或者手动确实得到处理的时间
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 告警状态
        /// </summary>
        public AlarmStatus AlarmStatus { get; set; }

        /// <summary>
        /// 严重程度
        /// </summary>
        public ServerityLevel Serverity { get; set; }

        /// <summary>
        /// 传播
        /// </summary>
        public bool Propagate { get; set; }

        /// <summary>
        /// 起因对象的Id
        /// </summary>
        public Guid OriginatorId { get; set; }

        /// <summary>
        /// 起因设备类型
        /// </summary>
        public OriginatorType OriginatorType { get; set; }

        public object Originator { get; set; }

        public int AlarmId { get; set; }
        public DateTime AlarmTime { get; set; }
        public string AlarmMsg { get; set; }
        public string EqtTypeId { get; set; }
        public string Uid { get; set; }
        public string EqtName { get; set; }
        public string ProcessingResult { get; set; }
        public object ProcessingOpinions { get; set; }
        public object ProcessingPeople { get; set; }
        public object ProcessingTime { get; set; }
        public int DeptId { get; set; }
    }


    public class AlarmStatusDto
    {
        public Guid Id { get; set; }

        public AlarmStatus AlarmStatus { get; set; }

    }

    
}
