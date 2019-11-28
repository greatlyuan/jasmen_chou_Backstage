using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace jasmen_chou_Backstage.Manager
{
    /// <summary>
    /// MySQL数据库操作
    /// author:pengcy
    /// date:2019-10-05
    /// </summary>
    public class MySqlDbHelper
    {
        #region 私有变量
        private const string defaultConfigKeyName = "DbHelper";
        private string connectionString;
        private string providerName;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数(DbHelper)
        /// </summary>
        public MySqlDbHelper()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[defaultConfigKeyName].ConnectionString;
            this.providerName = ConfigurationManager.ConnectionStrings[defaultConfigKeyName].ProviderName;
        }

        /// <summary>
        /// DbHelper构造函数
        /// </summary>
        /// <param name="keyName">连接字符串名</param>
        public MySqlDbHelper(string keyName)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[keyName].ConnectionString;
            this.providerName = ConfigurationManager.ConnectionStrings[keyName].ProviderName;
        }

        #endregion

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddRange(parameters);
            int res;
            try
            {
                res = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                res = -1;
            }
            cmd.Dispose();
            con.Close();
            return res;
        }

        /// <summary>
        /// 查询 返回对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddRange(parameters);
            object res = cmd.ExecuteScalar();
            cmd.Dispose();
            con.Close();
            return res;
        }

        /// <summary>
        /// 查询 返回表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddRange(parameters);
            DataSet dataset = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dataset);
            cmd.Dispose();
            con.Close();
            return dataset.Tables[0];
        }
    }
}