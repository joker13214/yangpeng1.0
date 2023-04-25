using System;
using System.Collections.Generic;
using System.Linq;  //Enumerable类的扩展方法
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
                         where info.Departmentid == 13 || info.Departmentid == 14
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
            context.SubmitChanges();                  //创建LINQ连接提交操作
        }

        //LINQ更新部分信息
        public void UpData()
        {
            DataClasses1DataContext context = new DataClasses1DataContext(Sqlserver.connString);//连接数据库
            context.ExecuteCommand("update Department set DepartmentName = '杨鹏' where Departmentid = 19 ");//直接对数据库执行命令
            
        }


        //方法测试
        public void LinqTest()
        {
            string[] names = new string[]{ "yangpeng", "wenhaijing", "yangkai", "yanghaonan" };
            //第一中写法（变量n在每一个lambda表达式都是私有的）
            //IEnumerable<string> vs = names.Where(n => n.Contains("y"));  
            //IEnumerable<string> vs1 = vs.OrderBy(n => n.Length);
            //IEnumerable<string> vs2 = vs1.Select(n => n.ToUpper());

            //第二种写法(使用静态方法)
            IEnumerable<string> vs3 = Enumerable.Where(names, n => n.Contains("y"));
            IEnumerable<string> vs4 = Enumerable.OrderBy(vs3, n => n.Length);
            IEnumerable<string> vs5 = Enumerable.Select(vs4, n => n.ToUpper());

            foreach (string name in vs5)
            {
                Console.WriteLine(name + "|");
            }
            Console.WriteLine("**********************************************************");
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 78, 8 };
            //Take方法是输出前x个元素，而丢弃其他的元素
            IEnumerable<int> result = numbers.Take(3);
            //Skip运算符会跳过集合中的前x个元素而输出剩余元素
            IEnumerable<int> result2 = numbers.Skip(3);
            //Reverse运算符会将集合中的所有元素进行反转
            IEnumerable<int> result3 = numbers.Reverse();
            foreach (int num in result)
            {
                Console.WriteLine(num);
            }

            int[] seq1 = { 1, 2, 3 };
            int[] seq2 = { 3, 4, 5 };
            IEnumerable<int> concat = seq1.Concat(seq2);//将seq2附加到seq1上
            IEnumerable<int> union = seq1.Union(seq2);  //将seq2附加到seq1上并且去掉重复元素


            string[] name1 = { "yangYang","yangkai","yangpeng","Mary"};
            IEnumerable<string> query = from n in name1
                                        where n.Contains("yang")
                                        orderby n.Length
                                        select n.ToLower();
            foreach(string name in query)
            {
                Console.WriteLine(name);
            }

        }
    }
}
