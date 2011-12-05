using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scada.BLL.Contract;

namespace Scada.BLL.Implement
{
    public class SystemManagerService : ISystemManagerService
    {
        public int AddDD(int x, int y)
        {
            return x + y;
        }
    }
}
