using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FengHC.DBScripts.Model
{
    /*
[Table("Tablename")] 标识对应的表名
[Key] 对应的主键
[ExplicitKey] 如果主键不是自增长的，用此标识
[Write(true/false)] 该字段是否可被写入
     * */

    /// <summary>
    /// 对应数据库中的t_order 表的数据结构
    /// </summary>
    public class OrderModel
    {
        public int Id;
        public string order_no;
    }
}
