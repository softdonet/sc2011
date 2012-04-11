using System;


namespace Scada.Model.Entity.Common
{

    public class LoginResult
    {

        public LoginResultType loginResultType { get; set; }

        public User sysUser { get; set; }

    }

    public enum LoginResultType
    {
        IP地址已锁定 = 0,
        密码错误 = 1,
        成功 = 2,
        账户已锁定 = 3,
        账户无效 = 4,
        IP与账户绑定错误 = 5
    }

}
