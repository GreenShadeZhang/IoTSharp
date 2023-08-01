using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verdure.Qinglan
{

    public class AlarmParams
    {
    }

    public class RadarAlarmModel
    {
        public int Total { get; set; }
        public List<RadarAlarm> Rows { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }
    }

    public class RadarAlarm
    {
        public object SearchValue { get; set; }
        public object CreateBy { get; set; }
        public object CreateTime { get; set; }
        public object UpdateBy { get; set; }
        public object UpdateTime { get; set; }
        public object Remark { get; set; }
        public AlarmParams Params { get; set; }
        public int Id { get; set; }
        public DateTime AlarmTime { get; set; }
        public string AlarmMsg { get; set; }
        public string EqtTypeId { get; set; }
        public string AlarmType { get; set; }
        public string Uid { get; set; }
        public string EqtName { get; set; }
        public string ProcessingResult { get; set; }
        public object ProcessingOpinions { get; set; }
        public object ProcessingPeople { get; set; }
        public object ProcessingTime { get; set; }
        public int DeptId { get; set; }
    }
}
