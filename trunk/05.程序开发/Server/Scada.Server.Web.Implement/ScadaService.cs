using System;
using Scada.Server.Contract;

namespace Scada.Server.Implement
{

    /// <summary>
    /// 天津爱德信息有限公司
    /// </summary>
    public class ScadaService : IScadaService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }

}
