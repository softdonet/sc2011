using System;
using Scada.BLL.Contract;


namespace Scada.BLL.Implement
{

    public class ScadaDeviceService : IScadaDeviceService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
