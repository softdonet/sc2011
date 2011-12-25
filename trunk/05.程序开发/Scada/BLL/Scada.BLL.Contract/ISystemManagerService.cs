using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.BLL.Contract
{
    public interface ISystemManagerService
    {
        int AddDD(int x, int y);
        string ListStudents();


        //登录
        Int32 GetLoginResultType(String username, String userpwd);

    }
}
