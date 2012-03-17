using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class RandomHelper
    {
        /// <summary>
        /// 生成一个整数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int GetRandom(int start,int end)
        {
            Random r = new Random();
            return r.Next(start, end);
        }
    }
}
