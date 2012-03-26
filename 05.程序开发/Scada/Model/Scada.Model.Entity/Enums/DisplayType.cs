using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Scada.Model.Entity.Enums
{
    /// <summary>
    /// 显示屏设置
    /// </summary>
    public enum DisplayType
    {
        /// <summary>
        /// 超高
        /// </summary>
        [EnumMember(Value = "完整显示")]
        FullDisplay = 1,

        /// <summary>
        /// 基本显示
        /// </summary>
        [EnumMember(Value = "基本显示")]
        BaseDisplay = 2,

        /// <summary>
        /// 天气预报
        /// </summary>
        [EnumMember(Value = "天气预报")]
        WetherDisplay = 3,

        /// <summary>
        /// 不显示
        /// </summary>
        [EnumMember(Value = "不显示")]
        NoDisplay = 4

    }

    public enum CurrentModel
    {
        /// <summary>
        /// 实时模式
        /// </summary>
        [EnumMember(Value = "实时模式")]
        RealTimeMode = 1,
        /// <summary>
        /// 整点模式
        /// </summary>
        [EnumMember(Value = "整点模式")]
        FullTimeModel = 2,
        /// <summary>
        /// 优化模式
        /// </summary>
        [EnumMember(Value = "优化模式")]
        OptimizeModel = 3
    }
}
