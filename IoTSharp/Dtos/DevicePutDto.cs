using IoTSharp.Contracts;
using IoTSharp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSharp.Dtos
{
    public class DevicePutDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EqtName { get; set; }


        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string EqtTypeName { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public long EqtId { get; set; }

        public int Timeout { get; set; }
        public Guid? DeviceModelId { get; set; }
        public IdentityType IdentityType { get; set; }
    }
}
