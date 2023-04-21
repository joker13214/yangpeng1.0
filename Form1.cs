using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yangpeng1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // 数据库连接
        private void button1_Click(object sender, EventArgs e)
        {
            Sqlserver sqlserver = new Sqlserver();
            string tablename = textBox1.Text;
            sqlserver.SQL_connect(tablename);
            if (Sqlserver.departments.Count() == 0)
            {
                return;
            }
            for(int i=0;i< Sqlserver.departments.Count(); i++)
            {
                Console.Write(Sqlserver.departments[i].Departmentid);
                Console.Write(" ");
                Console.WriteLine(Sqlserver.departments[i].DepartmentName);
            }
            dataGridView1.ColumnHeadersVisible = false; //列标题隐藏
            dataGridView1.DataSource = Sqlserver.departments;//list列表数据绑定到dataGridView中
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Client client = new Client();
            //client.Show();
            Console.WriteLine("你好！");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //数据库测试
        private void button4_Click(object sender, EventArgs e)
        {
            Sqlserver sqlserver = new Sqlserver();
            sqlserver.insert();
        }

        //LINQTest（Linq）
        private void button5_Click(object sender, EventArgs e)
        {
            //LINQ lINQ = new LINQ();
            //lINQ.LINQTest();
            LINQ linq1 = new LINQ();
            //linq1.DeleteData();           //删除数据库里面的部门信息
            linq1.InsertData();             //插入部分信息
            DataClasses1DataContext linq = new DataClasses1DataContext(Sqlserver.connString); //创建一个LINQ对象
            var data = from info in linq.Department
                       select new
                       {
                           编号 = info.Departmentid,
                           部门 = info.DepartmentName
                       };

            dataGridView1.DataSource = data;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
       
    }
}
