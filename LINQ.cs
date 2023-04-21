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
    }
}
