//*********************************************************************
//               异步通讯命令解析数据类型
//               Create by yanghk at 2011-12-18
//*********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting
{
    /// <summary>
    /// 通讯命令枚举
    /// </summary>
    public enum Command
    {
        Test,//发送哑数据  客户端到服务器
        Logout,//登出 客户端到服务器
        GetConfig,//配置请求 客户端到服务器
        RealTimeData,//设备实时信息 客户端到服务器
        UserEvent,//用户事件 客户端到服务器

        ReplyTest, //回复哑数据 服务器到客户端
        SendConfig //发送设备配置和天气预报信息 服务器到客户端
    }


    public class NetData
    {

    }
}
