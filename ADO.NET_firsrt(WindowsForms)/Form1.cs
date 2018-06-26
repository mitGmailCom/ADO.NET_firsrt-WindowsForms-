using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_firsrt_WindowsForms_
{
    public partial class Form1 : Form
    {
        SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn.DataSource = @"X\SQLEXPRESS";
            conn.InitialCatalog = "Academic_performance";
            conn.UserID = "sa";
            conn.Password = "maliwka8410";

            using (SqlConnection connect = new SqlConnection())
            {
                connect.ConnectionString = conn.ConnectionString;
                try
                {
                    connect.Open();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"{ex.Message}", "Exception");
                }
                finally
                {
                    MessageBox.Show("Close", "Conection");
                    connect.Close();
                }
                
            }
        }
    }
}
