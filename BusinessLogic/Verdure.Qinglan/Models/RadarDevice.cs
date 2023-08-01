using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verdure.Qinglan
{
    public class RadarDeviceModel
    {
        public int Total { get; set; }
        public List<RadarDevice> Rows { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }
    }
    public class RadarDevice
    {
        public object SearchValue { get; set; }
        public object CreateBy { get; set; }
        public string CreateTime { get; set; }
        public object UpdateBy { get; set; }
        public object UpdateTime { get; set; }
        public object Remark { get; set; }
        public Params Params { get; set; }
        public int EqtId { get; set; }
        public string EqtName { get; set; }
        public string Uid { get; set; }
        public int DeptId { get; set; }
        public int EqtTypeId { get; set; }
        public object UserPhoneNumber { get; set; }
        public string Espsfver { get; set; }
        public string Radarsfver { get; set; }
        public object LimitType { get; set; }
        public string Binding { get; set; }
        public string Online { get; set; }
        public string DeptName { get; set; }
        public object Operator { get; set; }
        public string EqtTypeName { get; set; }
        public object RoomId { get; set; }
        public object OrderId { get; set; }
        public object SlaveMobiles { get; set; }
        public string Hwver { get; set; }
    }

    public class Params
    {
    }
}
