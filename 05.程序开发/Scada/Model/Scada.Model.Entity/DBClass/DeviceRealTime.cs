using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{
    /// <summary>
    ///设备实时数据
    /// </summary>
    public partial class DeviceRealTime
    {
        public Guid ID { get; set; }
        public Guid DeviceID { get; set; }
        public string DeviceNo { get; set; }
        public decimal? Temperature1 { get; set; }
        public decimal? Temperature2 { get; set; }
        public int? Electricity { get; set; }
        public int? Signal { get; set; }
        public decimal? Humidity { get; set; }
        public int? Status { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}

