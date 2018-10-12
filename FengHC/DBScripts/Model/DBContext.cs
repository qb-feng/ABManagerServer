using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FengHC.DBScripts.Model
{
    public class DBContext
    {
        private string connectionString;//连接数据库字符串

        public DBContext(string connection)
        {
            this.connectionString = connection;
        }

        /// <summary>
        /// 公有方法 - 提供连接数据库的MySqlConnection对象
        /// </summary>
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
