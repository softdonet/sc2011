using System;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlClient;


namespace Scada.DAL.Ado
{


    /// <summary>
    ///	SqlHelperParameterCache 提供了一些函数，
    ///	用于平衡在运行时存储过程参数的静态缓存与查找存储过程参数的能力。
    /// </summary>
    public sealed class SqlHelperParameterCache
    {

        #region 私有方法、变量以及构造函数 private methods, variables, and constructors

        /// <summary>
        /// 由于该类仅提供一些静态方法，因此设置默认构造函数为 private，
        /// 以阻止使用 "new SqlHelperParameterCache()" 来创建实例。
        /// </summary>
        private SqlHelperParameterCache() { }

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 在运行时为一个存储过程解析匹配的的 SqlParameter[] 参数数组。
        /// </summary>
        /// <param name="connection">一个有效的 SqlConnection 连接实例 object</param>
        /// <param name="spName">存储过程名</param>
        /// <param name="includeReturnValueParameter">是否包含它们的 return value parameter 返回值参数</param>
        /// <returns>查找到的 SqlParameter[] 参数数组</returns>
        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                // 移除返回值参数
                cmd.Parameters.RemoveAt(0);
            }

            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // 使用 DBNull.Value 初始化参数
            foreach (SqlParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        /// <summary>
        /// 深度拷贝缓存的 SqlParameter[] 数组。
        /// </summary>
        /// <param name="originalParameters">原始的参数数组</param>
        /// <returns>深度拷贝的参数数组</returns>
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion 私有方法、变量以及构造函数 private methods, variables, and constructors

        #region 缓存函数 caching functions

        /// <summary>
        /// 将 SqlParameter[] 参数数组添加到缓存中。
        /// </summary>
        /// <param name="connectionString">一个有效的用于建立 SqlConnection 的连接字符串</param>
        /// <param name="commandText">存储过程名称 或 T-SQL 命令</param>
        /// <param name="commandParameters">一个将被缓存的 SqlParameter[] 参数数组。</param>
        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// 从缓存中取回 SqlParameter[] 参数数组。
        /// </summary>
        /// <param name="connectionString">一个有效的用于建立 SqlConnection 的连接字符串</param>
        /// <param name="commandText">存储过程名称 或 T-SQL 命令</param>
        /// <returns>一个 SqlParameter[] 参数数组</returns>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            SqlParameter[] cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion 缓存函数 caching functions

        #region 参数查找函数 Parameter Discovery Functions

        /// <summary>
        /// 为该存储过程取回一个匹配的参数数组。
        /// </summary>
        /// <remarks>
        /// 该方法将在数据库中查询这些信息，并存储到缓存中以备将来使用。
        /// </remarks>
        /// <param name="connectionString">一个有效的用于建立 SqlConnection 的连接字符串</param>
        /// <param name="spName">存储过程名</param>
        /// <returns>一个 SqlParameter[] 参数数组</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        /// <summary>
        /// 为该存储过程取回一个匹配的参数数组。
        /// </summary>
        /// <remarks>
        /// 该方法将在数据库中查询这些信息，并存储到缓存中以备将来使用。
        /// </remarks>
        /// <param name="connectionString">一个有效的用于建立 SqlConnection 的连接字符串</param>
        /// <param name="spName">存储过程名</param>
        /// <param name="includeReturnValueParameter">一个布尔值，指示返回值参数是否应该包括在结果中</param>
        /// <returns>一个 SqlParameter[] 参数数组</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// 为该存储过程取回一个匹配的参数数组。
        /// </summary>
        /// <remarks>
        /// 该方法将在数据库中查询这些信息，并存储到缓存中以备将来使用。
        /// </remarks>
        /// <param name="connection">一个有效的 SqlConnection 连接实例 object</param>
        /// <param name="spName">存储过程名</param>
        /// <returns>一个 SqlParameter[] 参数数组</returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>
        /// 为该存储过程取回一个匹配的参数数组。
        /// </summary>
        /// <remarks>
        /// 该方法将在数据库中查询这些信息，并存储到缓存中以备将来使用。
        /// </remarks>
        /// <param name="connection">一个有效的 SqlConnection 连接实例 object</param>
        /// <param name="spName">存储过程名</param>
        /// <param name="includeReturnValueParameter">一个布尔值，指示返回值参数是否应该包括在结果中</param>
        /// <returns>一个 SqlParameter[] 参数数组</returns>
        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            using (SqlConnection clonedConnection = (SqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// 为该存储过程取回一个匹配的参数数组。
        /// </summary>
        /// <param name="connection">一个有效的 SqlConnection 连接实例 object</param>
        /// <param name="spName">存储过程名</param>
        /// <param name="includeReturnValueParameter">一个布尔值，指示返回值参数是否应该包括在结果中</param>
        /// <returns>一个 SqlParameter[] 参数数组</returns>
        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            SqlParameter[] cachedParameters;

            cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                SqlParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion 参数查找函数 Parameter Discovery Functions
        
    }

}
