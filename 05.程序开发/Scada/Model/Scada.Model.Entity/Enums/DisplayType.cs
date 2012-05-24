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

    /// <summary>
    /// 对选择--实时模式--下的下拉框中的值的设置,单位：秒
    /// </summary>
    public enum ReaTime
    {
        /// <summary>
        /// 默认5分钟
        /// </summary>
        [EnumMember(Value = "5")]
        SendTerm5S = 1,
        /// <summary>
        /// 10分钟
        /// </summary>
        [EnumMember(Value = "10")]
        NextSendTerm10S = 2,
    }

    /// <summary>
    /// 对选择--整点模式:参数1--下的下拉框中的值的设置,单位：分钟
    /// </summary>
    public enum FullTime1
    {
        /// <summary>
        ///5分钟
        /// </summary>
        [EnumMember(Value = "5")]
        SendTerm5M = 1,

        /// <summary>
        ///15分钟
        /// </summary>
        [EnumMember(Value = "15")]
        SendTerm15M = 2,

        /// <summary>
        ///30分钟
        /// </summary>
        [EnumMember(Value = "30")]
        SendTerm30M = 3,

        /// <summary>
        ///45分钟
        /// </summary>
        [EnumMember(Value = "45")]
        SendTerm45M = 4,

        /// <summary>
        ///60分钟
        /// </summary>
        [EnumMember(Value = "60")]
        SendTerm60M = 5,
    }

    /// <summary>
    /// 对选择--整点模式:参数2--下的下拉框中的值的设置,单位：分钟
    /// </summary>
    public enum FullTime2
    {
        /// <summary>
        ///30分钟
        /// </summary>
        [EnumMember(Value = "30")]
        SendTerm30M = 1,

        /// <summary>
        ///1小时
        /// </summary>
        [EnumMember(Value = "60")]
        SendTerm60M = 2,

        /// <summary>
        ///2小时
        /// </summary>
        [EnumMember(Value = "120")]
        SendTerm120M = 3,

        /// <summary>
        ///3小时
        /// </summary>
        [EnumMember(Value = "180")]
        SendTerm180M = 4,

        /// <summary>
        ///4小时
        /// </summary>
        [EnumMember(Value = "240")]
        SendTerm240M = 5,
    }

    /// <summary>
    /// 对选择--优化模式:参数1--下的下拉框中的值的设置,单位：分钟
    /// </summary>
    public enum Optimize1
    {
        /// <summary>
        ///1分钟
        /// </summary>
        [EnumMember(Value = "1")]
        SendTerm1M = 1,

        /// <summary>
        ///2分钟
        /// </summary>
        [EnumMember(Value = "2")]
        SendTerm2M = 2,

        /// <summary>
        ///5分钟
        /// </summary>
        [EnumMember(Value = "5")]
        SendTerm5M = 3,
    }

    /// <summary>
    /// 对选择--优化模式:参数2--下的下拉框中的值的设置,单位：分钟
    /// </summary>
    public enum Optimize2
    {
        /// <summary>
        ///30分钟
        /// </summary>
        [EnumMember(Value = "30")]
        SendTerm30M = 1,

        /// <summary>
        ///60分钟
        /// </summary>
        [EnumMember(Value = "60")]
        SendTerm60M = 2,

        /// <summary>
        ///120分钟
        /// </summary>
        [EnumMember(Value = "120")]
        SendTerm120M = 3,
    }


    /// <summary>
    /// 对选择--优化模式:参数3--下的下拉框中的值的设置,单位：分钟
    /// </summary>
    public enum Optimize3
    {
        /// <summary>
        ///30分钟
        /// </summary>
        [EnumMember(Value = "30")]
        SendTerm30M = 1,

        /// <summary>
        ///60分钟
        /// </summary>
        [EnumMember(Value = "60")]
        SendTerm60M = 2,

        /// <summary>
        ///120分钟
        /// </summary>
        [EnumMember(Value = "120")]
        SendTerm120M = 3,
    }
}
