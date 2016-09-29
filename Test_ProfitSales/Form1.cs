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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'myDataBaseDataSet.factSales' table. You can move, or remove it, as needed.
            this.factSalesTableAdapter.Fill(this.myDataBaseDataSet.factSales);
            // TODO: This line of code loads data into the 'myDataBaseDataSet.factRateSale' table. You can move, or remove it, as needed.
            this.factRateSaleTableAdapter.Fill(this.myDataBaseDataSet.factRateSale);

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



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Form2 f = new Form2();
                f.Show(); 
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Form3 f = new Form3();
                f.Show();
            }
        }
    }
}
