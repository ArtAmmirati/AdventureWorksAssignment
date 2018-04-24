using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AdventureWorksAssignment
{
    public partial class MasterForm : Form
    {
        const string ConnString = @"Server=DTPLAPTOP02;Database=AdventureWorks2012;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(ConnString);

        public MasterForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("dbo.sp_CustomerList", ConnString);
            DataTable dtCBCust = new DataTable();

            int CustomerID;
            string Customer;

            try
            {
                sqlDataAdapter.Fill(dtCBCust);

                foreach (DataRow drCustomer in dtCBCust.Rows)
                {
                    CustomerID = int.Parse(drCustomer.ItemArray[0].ToString());
                    Customer = drCustomer.ItemArray[1].ToString();
                    CustomerCB.Items.Add(new cboObject(CustomerID, Customer));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //it checks if the row index of the cell is greater than or equal to zero
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGrid.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                salesOrderIDTB.Text = row.Cells[0].Value.ToString();

            }
        }

        private void CustomerCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboObject current = (cboObject)CustomerCB.SelectedItem;
            int CustomerID = current.CustomID;
            DataTable dtCustomerOrders = new DataTable();

            try
            {
                SqlCommand sqlCom = new SqlCommand("dbo.sp_SalesBS1", sqlConnection);
                sqlCom.CommandType = CommandType.StoredProcedure;

                SqlParameter prmCustID = new SqlParameter("@CustomerID", CustomerID);
                sqlCom.Parameters.Add(prmCustID);

                SqlDataAdapter dataFeed = new SqlDataAdapter(sqlCom);

                dataFeed.Fill(dtCustomerOrders);

                dataGrid.DataSource = dtCustomerOrders;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public class cboObject
        {
            int custID;
            string custName;


            public cboObject(int CustomID, string CustomName)
            {
                custID = CustomID;
                custName = CustomName;
            }


            public cboObject(string CustomName)
            {
                custName = CustomName;
            }


            public int CustomID
            {
                get { return custID; }
                set { custID = value; }
            }


            public string CustomName
            {
                get { return custName; }
                set { custName = value; }
            }


            public override string ToString()
            {
                return CustomName;
            }

        }

        private void salesOrderIDTB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
