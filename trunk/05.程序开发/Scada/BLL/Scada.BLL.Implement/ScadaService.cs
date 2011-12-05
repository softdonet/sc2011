using System;
using Scada.BLL.Contract;


namespace Scada.BLL.Implement
{

    public class ScadaService : IScadaService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
