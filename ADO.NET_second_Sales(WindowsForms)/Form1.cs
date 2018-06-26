using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_second_Sales_WindowsForms_
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnSales"].ConnectionString;

            SqlCommand commandTables = new SqlCommand("select * from Sales.information_schema.tables where TABLE_TYPE like '%TABLE%'", conn);
        }
    }
}
