using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yangpeng1._0.高级通用类
{
    public class CRUDAdvance
    {
        public void Insert(string DepartmentName)
        {
            //定义SQL语句
            //string sql = "insert into Department(DepartmentName) values('{0}')";
            //string DepartmentName = Console.ReadLine();//输入部门名字
            //sql = string.Format(sql, DepartmentName);  //格式化


            //定义Sql语句
            string sql = "insert into Department(DepartmentName) values(@DepartmentName)";
            //封装参数
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@DepartmentName",DepartmentName)
            };

        }
    }
}
