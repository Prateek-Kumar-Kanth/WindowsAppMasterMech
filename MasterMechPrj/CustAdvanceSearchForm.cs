using MainForm;
using MasterMechData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMechPrj
{
    public partial class CustAdvanceSearchForm : Form
    {
        CustomerForm lObjGlobalCust;
        public CustAdvanceSearchForm(CustomerForm iObjCustForm)
        {
            InitializeComponent();
            lObjGlobalCust = iObjCustForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lsFirstName = TextBoxFName.Text;
            string lsLastName = TextBoxLName.Text;
            string lsCity = TextBoxCity.Text;

            Customer lObj = new Customer();
            List<Customer> ListCustomerData = lObj.AdvanceSearch(lsFirstName, lsLastName, lsCity);

            if (ListCustomerData.Count > 0)
            {
                //CustomerForm lObjCustForm = new CustomerForm();
                this.Hide();
                lObjGlobalCust.CustomerData(ListCustomerData);

                //SearchResultForm lObjResult = new SearchResultForm(ListItemData, MasterMechUtil.FormType.Customer);
                //this.Hide();
                //DialogResult Result = lObjResult.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Match Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
