using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetData;


namespace BusinessRules
{
    public class Class1
    {
        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="dc_r"></param>
        /// <returns></returns>
        public bool RefreshData(RequestBase_R dc_r)
        {
            return false;
        }

        /// <summary>
        /// 获取设备配置信息、天气预报、广播等
        /// </summary>
        /// <param name="deviceSN"></param>
        /// <returns></returns>
        public ReplyBase_S GetDeviceInfor(string deviceSN)
        {
            return null;
        }
    }
}
