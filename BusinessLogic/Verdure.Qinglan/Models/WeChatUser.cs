using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verdure.Qinglan
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class WeChatUserParams
    {
    }

    public class WeChatUserModel
    {
        public int Total { get; set; }
        public List<WeChatUser> Rows { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }
    }

    public class WeChatUser
    {
        public object SearchValue { get; set; }
        public string CreateBy { get; set; }
        public string CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateTime { get; set; }
        public string Remark { get; set; }
        public WeChatUserParams Params { get; set; }
        public int WxId { get; set; }
        public string Nickname { get; set; }
        public object Avatar { get; set; }
        public object Openid { get; set; }
        public object Gender { get; set; }
        public object Code { get; set; }
        public string Mobile { get; set; }
        public string DeptName { get; set; }
        public int DeptId { get; set; }
        public object MobileCode { get; set; }
        public string LoginIp { get; set; }
        public string Status { get; set; }
        public object LoginEndpoin { get; set; }
        public object DeptEmail { get; set; }
        public object DeptLeader { get; set; }
        public object SlaveMobile { get; set; }
        public object IsDelete { get; set; }
        public object SessionKey { get; set; }
        public List<string> Mobiles { get; set; }
    }
}
