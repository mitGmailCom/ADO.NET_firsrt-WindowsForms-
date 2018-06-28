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
        private static string strConnection;
        List<string> listAllTables;
        DataSet dataset;
        SqlDataAdapter adapter;

        public Form1()
        {
            InitializeComponent();
            listAllTables = new List<string>();
            dataset = new DataSet();
            adapter = new SqlDataAdapter();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection();
            strConnection = ConfigurationManager.ConnectionStrings["ConnSales"].ConnectionString;
            conn.ConnectionString = strConnection;
            SqlCommand command = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE LIKE '%TABLE%'", conn);

            try
            {
                conn.Open();
                MessageBox.Show("Connection", "Start");
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataset);

                foreach (DataTable dt in dataset.Tables)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var cells = row.ItemArray;
                        for (int i = 0; i < cells.Length; i++)
                        {
                            if (i == 2)
                            {
                                listAllTables.Add(cells[i].ToString());
                                Console.WriteLine();
                            }
                        }
                    }
                }
                comboBox1.Items.AddRange(listAllTables.ToArray());
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error", $"{ex.Message}");
            }

            finally
            {
                MessageBox.Show("Connection", "Close");
                conn.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader rdr = null;
            try
            {
                DataTable dataTable = new DataTable();
                string table = comboBox1.SelectedItem.ToString();
                string sqlQeury = $"select * from {table}";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQeury, conn);
                rdr = cmd.ExecuteReader();
                int line = 0;

                while (rdr.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            dataTable.Columns.Add(rdr.GetName(i));
                        }
                    }
                    line++;
                    DataRow row = dataTable.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row[i] = rdr[i];
                    }
                    dataTable.Rows.Add(row);
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error", $"{ex.Message}");
            }
            finally
            {
                if (conn != null)
                    conn.Close();
                if (rdr != null)
                    rdr.Close();
            }
        }
    }
}
