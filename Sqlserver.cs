using LinqKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yangpeng1._0
{
    //数据库连接服务
   
    public  class Sqlserver
    {
        // public string Connect_path = "server=192.168.110.196;Database=MISDB;uid=sa;pwd=123456";//连接数据库的操作
        //public static string connString = "server=.;Database=MISDB;uid=sa;pwd=123456";
        public static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
        public static List<Department> departments = new List<Department>(); //数据存储list列表
        //数据库连接服务
        public  void SQL_connect(string tablename)
        { 
            //创建连接对象
            SqlConnection conn = new SqlConnection(connString);
            //定义Sql语句
            string sql = "select * from " + tablename;
            //创建Command对象
            //打开连接
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("连接成功！");
            }
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.CommandText = sql;
           // sqlCommand.EndExecuteNonQuery(); 此方法，可以执行insert、update、delete类型的sql语句，不能执行select
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Department departmentData = new Department();
                departmentData.Departmentid = (int)reader["Departmentid"];
                departmentData.DepartmentName = (string)reader["DepartmentName"];
                departments.Add(departmentData);
                //Console.WriteLine(reader["Departmentid"] + " " + reader["DepartmentName"]);
            }
            reader.Close();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);//创建一个数据适配器（）
            DataSet dataSet = new DataSet();                               //创建数据集对象DataSet(内存数据库)DataTable
            sqlDataAdapter.Fill(dataSet);                                  //填充数据
            conn.Close();
            //遍历DataTable的数据行
           // Department departmentData = new Department();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Department departmentData = new Department(); //对象每次都要实例化
                departmentData.Departmentid = (int)row["Departmentid"];
                departmentData.DepartmentName = (string)row["DepartmentName"];
                departments.Add(departmentData);
            }
            
            Console.WriteLine("****************************************************");
        }

        //插入
        public void insert()
        {
            SqlConnection sqlConnection = new SqlConnection(connString); //连接数据库
            string sql ="insert into Department(DepartmentName) values('审核部')";
            SqlCommand sql1 = new SqlCommand(sql, sqlConnection);
            sqlConnection.Open();
            int result = sql1.ExecuteNonQuery();//此方法可以执行insert、update、delete类型的sql语句,//专用方法
            Console.WriteLine("受影响的行数：" + result);
            sqlConnection.Close();
        }

        //update
        public void update()
        {
            SqlConnection sqlConnection = new SqlConnection(connString); //连接数据库
            string sql = "update Department set DepartmentName='九九部' where Departmentid=11";
            SqlCommand sql1 = new SqlCommand(sql, sqlConnection);
            sqlConnection.Open();
            int result = sql1.ExecuteNonQuery();//此方法可以执行insert、update、delete类型的sql语句
            Console.WriteLine("受影响的行数：" + result);
            sqlConnection.Close();
        }
        //delete
        public void delete()
        {
            SqlConnection sqlConnection = new SqlConnection(connString); //连接数据库
            string sql = "delete from Department where Departmentid=12";
            SqlCommand sql1 = new SqlCommand(sql, sqlConnection);
            sqlConnection.Open();
            int result = sql1.ExecuteNonQuery();//此方法可以执行insert、update、delete类型的sql语句
            Console.WriteLine("受影响的行数：" + result);
            sqlConnection.Close();
        }
        //返回多个数据集含多张数据表
        public void GetDataSet()
        {
            //创建连接对象
            SqlConnection conn = new SqlConnection(connString);
            //定义Sql语句
            string sql = "select * from Department";
            //创建Command对象
            //打开连接
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("连接成功！");
            }
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.CommandText = sql;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);//创建一个数据适配器（）
            DataSet dataSet = new DataSet();                               //创建数据集对象DataSet(内存数据库)DataTable
            sqlDataAdapter.Fill(dataSet,"Department");                                  //填充数据
            foreach (DataRow row in dataSet.Tables["Department"].Rows)  //可以不用索引直接使用表名来确认 
            {
                Department departmentData = new Department(); //对象每次都要实例化
                departmentData.Departmentid = (int)row["Departmentid"];
                departmentData.DepartmentName = (string)row["DepartmentName"];
                departments.Add(departmentData);
            }
            conn.Close();
            //遍历DataTable的数据行
            // Department departmentData = new Department();
            //foreach (DataRow row in dataSet.Tables[0].Rows)
            //{
            //    Department departmentData = new Department(); //对象每次都要实例化
            //    departmentData.Departmentid = (int)row["Departmentid"];
            //    departmentData.DepartmentName = (string)row["DepartmentName"];
            //    departments.Add(departmentData);
            //}
              
        }
        //通用方法
        public static int ExecuteNonQuery(string sql)
        {
            SqlConnection sqlConnection = new SqlConnection(connString);
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            try {
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                //throw new Exception { "发生异常：" + ex.Message };
                Console.WriteLine("出现异常");
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        //LINQ查询连接
        public void test() {
            DataClasses1DataContext linq =new DataClasses1DataContext(connString); //创建一个LINQ对象
            var data = from info in linq.Department select new { 
                编号=info.Departmentid,
                部门=info.DepartmentName
            };
        }
        //执行返回一个结果集的查询
        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection sqlConnection = new SqlConnection(connString);
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            try
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch(Exception ex)
            {
                throw new Exception("执行发生异常：" + ex.Message );
            }

        }
        //返回一个数据集的查询
        public static DataSet GetDataSet1(string sql, string tableName = null)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            conn.Open();
            try
            {
                
                if (tableName == null)
                {
                    da.Fill(dataSet);
                }
                else
                {
                    da.Fill(dataSet, tableName);
                }
                return dataSet;
            }
            catch(Exception ex)
            {
                throw new Exception("方法发生异常");
            }
            finally
            {
                conn.Close();
            }
        }
        //方法重载
        public static DataSet GetDataSet2(Dictionary<string,string> dictionary)//key,value
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            conn.Open();
            try
            {
                foreach (string tbname in dictionary.Keys)
                {
                    cmd.CommandText = dictionary[tbname];
                    da.Fill(dataSet, tbname);
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception("方法发生异常");
            }
            finally
            {
                conn.Close();
            }
        }
    }
    
}
