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

namespace Test_ProfitSales
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private static String connString = "Data Source=localhost;Initial Catalog=MyDataBase;Integrated Security=True";
        public static DataTable Query(string query)
        {
            SqlConnection connect = new SqlConnection(connString);
            DataTable result = new DataTable();
            try
            {
                connect.Open();
                SqlCommand command = new SqlCommand(@"set dateformat dmy " + query, connect);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed)
                    connect.Close();
            }
            return result;
        }


        public string reportStart;

        public string reportEnd ;

        private void button1_Click(object sender, EventArgs e)
        {
        string zapros = "SELECT SUM(T2.Price * T2.Quantity * T1.Rate) AS Profit FROM[dbo].[factRateSale] T1 JOIN[dbo].[factSales] T2 ON T2.ProductID = T1.ProductID WHERE(T2.Date BETWEEN '" + reportStart + "' AND '" + reportEnd + "') AND(T1.Date = (SELECT max(T1.Date) FROM[dbo].[factRateSale] T1 WHERE T1.ProductID = T2.ProductID AND T1.Date<T2.Date))";
            dataGridView1.DataSource = Query(zapros);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            reportStart = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            reportEnd = textBox2.Text;
        }
    }
}
