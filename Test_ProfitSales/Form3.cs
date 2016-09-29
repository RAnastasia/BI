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
    public partial class Form3 : Form
    {
        public Form3()
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

        private void button1_Click(object sender, EventArgs e)
        {
            string zapros = "SELECT MONTH(T2.Date) as ProfitMonth, SUM(T2.Price * T2.Quantity * T1.Rate) AS Profit FROM[dbo].[factRateSale] T1 JOIN[dbo].[factSales] T2 ON T2.ProductID = T1.ProductID WHERE(T1.Date = (SELECT max(T1.Date) FROM[dbo].[factRateSale] T1 WHERE T1.ProductID = T2.ProductID AND T1.Date<T2.Date)) GROUP BY MONTH(T2.Date), T2.ProductID HAVING SUM(Quantity) = 200";
            dataGridView1.DataSource = Query(zapros);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
     
            comboBox1.SelectedItem.ToString();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'myDataBaseDataSet.factSales' table. You can move, or remove it, as needed.
            this.factSalesTableAdapter.Fill(this.myDataBaseDataSet.factSales);

        }
    }
}
