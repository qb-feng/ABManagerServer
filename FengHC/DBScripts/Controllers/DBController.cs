using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FengHC.DBScripts.Model;
using MySql.Data.MySqlClient;
using FengHC.DBScripts.Manager;
using Newtonsoft.Json;

namespace FengHC.DBScripts.Controllers
{
    [Route("api/db")]
    public class DBController : Controller
    {
        [HttpGet("1")]
        public IActionResult TestShowDB()
        {

            var myConnectionString = "Data Source=localhost;Port=3306;Database=mu_test;User Id=root;Password=123456";
            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            string myInsertQuery = "select * from t_order";
            MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
            myCommand.Connection = myConnection;
            myConnection.Open();
            var result = myCommand.ExecuteReader();
            Console.WriteLine("#####################################################################");
            Console.WriteLine("#####################################################################");
            Console.WriteLine("#####################################################################");
            Console.WriteLine("#####################################################################");
            try
            {

                Console.WriteLine(result.FieldCount);
                var resultFieldCount = result.FieldCount;//获取当前行中的列数。
                var resultString = result.ToString();

                for (int i = 0; i < resultFieldCount; ++i)
                {
                    Console.WriteLine(result.GetName(i));//获取指定列的名称。
                }
                while (result.Read())//将MySqlDataReader推进到下一条记录
                {
                    if (result.HasRows)//获取一个值，该值指示MySqlDataReader是否包含一行或多行
                    {
                        for (int i = 0; i < resultFieldCount; i++)
                        {
                            Console.Write(result[i]);//重载。以其本机格式获取列的值。在C＃中，此属性是MySqlDataReader类的索引器。用来获取当前行的第n列的值 返回object类型
                            Console.Write("\t");

                        }
                        Console.Write("\n");
                    }
                }
            }
            finally
            {
                result.Close();
                myCommand.Connection.Close();

            }
            return Ok();
        }

        [HttpGet("2")]
        public IActionResult TestShowDB2()
        {
            DBContext dbContext = HttpContext.RequestServices.GetService(typeof(DBContext)) as DBContext;
            MySqlConnection mySqlConnection;
            using (var result = DBManager.ExecutiveSqlCommand(dbContext, "select * from t_order", out mySqlConnection))
            {

                List<OrderModel> orders = new List<OrderModel>();
                while (result.Read())//将MySqlDataReader推进到下一条记录
                {
                    if (result.HasRows)//获取一个值，该值指示MySqlDataReader是否包含一行或多行
                    {
                        orders.Add(new OrderModel()
                        {
                            Id = result.GetInt32("Id"),
                            order_no = result.GetString("order_no"),
                        });
                    }
                }

                Console.WriteLine(orders.Count);
                mySqlConnection.Close();
            }
            return Ok();
        }

        public enum OperateType
        {
            Default = 0,
            /// <summary>
            /// 增
            /// </summary>
            Add = 1,
            /// <summary>
            /// 删
            /// </summary>
            Del = 2,
            /// <summary>
            /// 查
            /// </summary>
            Select = 3,
            /// <summary>
            /// 改
            /// </summary>
            Update = 4,
        }

        [HttpGet("operate")]
        public IActionResult TestOperateDB(int type)
        {
            string resultString = null;
            try
            {
                DBContext dbContext = HttpContext.RequestServices.GetService(typeof(DBContext)) as DBContext;
                MySqlConnection mySqlConnection;
                string cmd = null;
                OperateType operateType = (OperateType)type;
                switch (operateType)
                {
                    case OperateType.Add:
                        cmd = "insert into t_order(Id,order_no) values(2,233)";
                        break;

                    case OperateType.Del:
                        cmd = "delete from t_order where Id = 2";
                        break;

                    case OperateType.Select:
                        cmd = "select * from t_order where Id > 0";
                        break;

                    case OperateType.Update:
                        cmd = "update t_order set order_no = 123456 where Id = 1";
                        break;
                    default:
                        return Ok();
                }

                using (var result = DBManager.ExecutiveSqlCommand(dbContext, cmd, out mySqlConnection))
                {
                    resultString = ParseCmdResult(result);
                    mySqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                resultString = e.Message;
            }

            return Ok(resultString);
        }

        /// <summary>
        /// 解析cmd命令执行后的结果
        /// </summary>
        private string ParseCmdResult(MySqlDataReader result)
        {
            List<OrderModel> orders = new List<OrderModel>();
            while (result.Read())//将MySqlDataReader推进到下一条记录
            {
                if (result.HasRows)//获取一个值，该值指示MySqlDataReader是否包含一行或多行
                {
                    orders.Add(new OrderModel()
                    {
                        Id = result.GetInt32("Id"),
                        order_no = result.GetString("order_no"),
                    });
                }
            }

            Console.WriteLine(orders.Count);
            return JsonConvert.SerializeObject(orders);

        }
    }
}