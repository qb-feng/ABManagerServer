using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FengHC.DBScripts.Model;
using MySql.Data.MySqlClient;
using FengHC.DBScripts.Manager;

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
            using (var result = DBManager.ExecutiveSqlCommand(dbContext, "select * from t_order",out mySqlConnection))
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
    }
}