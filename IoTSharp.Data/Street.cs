using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSharp.Data
{
    /// <summary>
    /// 街道小区
    /// </summary>
    public class Street
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 省码
        /// </summary>
        public int ProvinceCode { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        public int CityCode { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区县编码
        /// </summary>
        public int DistrictCode { get; set; }

        /// <summary>
        /// 区县名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public string NeighName { get; set; }

        /// <summary>
        /// 地址详情
        /// </summary>
        public string AddressDetail { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// 管理员手机号
        /// </summary>
        public string ManagerPhone { get; set; }

        /// <summary>
        /// 管理员邮箱
        /// </summary>

        public string ManagerEmail { get; set; }

        /// <summary>
        /// 小区人数
        /// </summary>
        public int PeopleNum { get; set; }

        /// <summary>
        /// 老人数
        /// </summary>
        public int OlderNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string CreateUserId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        public Tenant Tenant { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public Customer Customer { get; set; }

        public bool Deleted { get; set; }
    }
}
