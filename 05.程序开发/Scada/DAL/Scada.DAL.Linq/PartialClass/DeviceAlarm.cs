using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scada.Utility.Common.Helper;
using Scada.Model.Entity.Enums;

namespace Scada.DAL.Linq
{
    public partial class DeviceAlarm
    {
        /// <summary>
        /// 处理人
        /// </summary>
        public string DealPeopleLoginName
        {
            get
            {
                if (this.User != null)
                {
                    return User.UserName;
                }
                return string.Empty;
            }
        }


        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventTypeName
        {
            get
            {
                return EnumHelper.Display<EventTypes>(this.EventType);
            }
        }
    }
}
