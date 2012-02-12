using System;
using System.IO;
using System.Text;

using System.Runtime.Serialization.Json;

namespace Scada.Client.VM.CommClass
{

    /// <summary>
    /// 序列化
    /// </summary>
    public class BinaryObjTransfer
    {

        /// <summary>
        /// 序列化对象为二进制流
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string BinarySerialize<T>(T obj)
        {
            DataContractJsonSerializer bf = new DataContractJsonSerializer(typeof(T));
            byte[] array = null;
            using (MemoryStream ms = new MemoryStream())
            {
                bf.WriteObject(ms, obj);
                ms.Close();
                array = ms.ToArray();
            }
            return Encoding.UTF8.GetString(array, 0, array.Length);
        }

        /// <summary>
        /// 反序列化二进制流为对象
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T BinaryDeserialize<T>(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }


        }

    }

}
