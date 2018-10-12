using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FengHC.DBScripts.Model;
using Microsoft.AspNetCore.Http;

namespace FengHC.DBScripts.Manager
{
    public class DBManager
    {
        /// <summary>
        /// 执行sql命令语句
        /// </summary>
        public static MySqlDataReader ExecutiveSqlCommand(DBContext dbContext, string commandString, out MySqlConnection myConnection)
        {
            MySqlDataReader result = null;
            myConnection = dbContext.GetConnection();

            myConnection.Open();
            MySqlCommand myCommand = new MySqlCommand(commandString);
            myCommand.Connection = myConnection;
            result = myCommand.ExecuteReader();
            
            return result;
        }
    }
}
