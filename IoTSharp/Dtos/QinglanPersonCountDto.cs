namespace IoTSharp.Dtos
{
    public class QinglanPersonCountDto
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Cmd { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 人数变更
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 变更人数
        /// </summary>
        public int Count { get; set; }
    }
}
