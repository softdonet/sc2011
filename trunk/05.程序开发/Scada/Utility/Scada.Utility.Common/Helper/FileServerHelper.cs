using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Scada.Utility.Common.Helper
{
    public class FileServerHelper
    {
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="data">字节数据</param>
        /// <returns></returns>
        public static bool SaveHeadImage(string fileName, byte[] data)
        {
            try
            {
                string tempPath = HttpContext.Current.Request.PhysicalApplicationPath + "UploadFile/HeadImage/11.jpg";
                FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate);
                fs.Write(data, 0, data.Length);
                fs.Dispose();
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool DeleteHeadImage(string fileName)
        {
            return true;
        }

        /// <summary>
        /// 获取图片Url
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetHeadeImageUrl(string fileName)
        {
            string fileUrl = string.Format("http://{0}:{1}/UploadFile/HeadImage/{2}",
                       HttpContext.Current.Request.Url.Host,
                       HttpContext.Current.Request.Url.Port,
                       fileName);
            return fileUrl;
        }
    }
}
