using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.Utility.Common.Helper
{
    public class FileServerHelper
    {
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileExtendName(string fileName)
        {
            return ".jpg";
 
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="data">字节数据</param>
        /// <returns></returns>
        public static bool SaveHeadImage(string fileName, byte[] data)
        {
            return false;
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool DeleteHeadImage(string fileName)
        {
            return false;
        }

        /// <summary>
        /// 获取图片Url
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetHeadeImageUrl(string fileName)
        {
            return "http://res1.pic.766.com/img1/824/46/5/31272614626350.jpg";
        }
    }
}
