using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yangpeng1._0
{
    public class LINQ
    {
        //LINQ查询测试
        public void LINQTest()
        {
            string[] strWords = { "MingRI", "XiaoKe", "MRBccd" };//定义字符串数组
            var ChangeWord =
                from word in strWords
                select new { Upper = word.ToUpper(), Lower = word.ToLower() };
            foreach(var vWord in ChangeWord)
            {
                Console.WriteLine("大写：{0}，小写：{1}", vWord.Upper, vWord.Lower);//转换后的单词
            }
            Console.ReadLine();
        }

        //LINQ删除部门信息
        public void DeleteData()
        {
            DataClasses1DataContext context = new DataClasses1DataContext(Sqlserver.connString);//连接数据库
            var result = from info in context.Department
                         where info.Departmentid == 13 || info.Departmentid==14
                         select info;
            context.Department.DeleteAllOnSubmit(result);//删除数据表里面的信息
            context.SubmitChanges();                     //创建LINQ连接对象提交操作
        }

        //LINQ插入部门信息
        public void InsertData()
        {
            DataClasses1DataContext context = new DataClasses1DataContext(Sqlserver.connString);//连接数据库
            var newDepartment = new Department
            {
                DepartmentName="温海菁"
            };
            context.Department.InsertOnSubmit(newDepartment);
            context.SubmitChanges();

        }

        //LINQ更新部分信息
        public void UpData()
        {
            DataClasses1DataContext context = new DataClasses1DataContext(Sqlserver.connString);//连接数据库
            //var newResult = from info in context.Department
            //                where info.Departmentid==24
            //                set 
                             
               

        }
        
    }
}
