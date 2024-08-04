using MasterMechData;
using MasterMechPrj;
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

namespace MainForm
{
    public partial class MainForm : Form
    {
        string  lsUserType;
        List<User> lObjUserResultList;
        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm(string isUserType)
        {
            InitializeComponent();
            lsUserType = isUserType;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Do You Want To Exit?", "Exit Window", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void newToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UserForm lObjUser = new UserForm(MasterMechUtil.OPMode.New);
            lObjUser.Show();
        }

        private void newToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CustomerForm lObjCustomer = new CustomerForm(MasterMechUtil.OPMode.New);
            lObjCustomer.Show();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemForm lObjItem = new ItemForm(MasterMechUtil.OPMode.New);
            lObjItem.Show();
        }

        private void listToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            User lObjUser = new User();
            List<User> lObjUserResultList = lObjUser.ListData("");

            UserForm lObjUserForm = new UserForm();
            lObjUserForm.UserData(lObjUserResultList); 
            
        }

        private void deleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UserForm lObjUser = new UserForm(MasterMechUtil.OPMode.Delete);
            lObjUser.Show();
        }

        private void openToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UserForm lObjUser = new UserForm(MasterMechUtil.OPMode.Open);
            lObjUser.Show();
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Customer lObjCust = new Customer();
            List<Customer> lObjCustResultList = lObjCust.ListData("");

            CustomerForm lObjCustForm = new CustomerForm();
            lObjCustForm.CustomerData(lObjCustResultList);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
            LabelFY.Text += ":  " + MasterMechUtil.sFY;
            LabelLastLogin.Text += ":  " + DateTime.Now;
            LabelWelcome.Text += ": " + MasterMechUtil.msUserName;

            if(MasterMechUtil.msUserType != "Admin")
            {
                userToolStripMenuItem.Visible = false;
            }

        }

        private void openToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CustomerForm lObjCustomer = new CustomerForm(MasterMechUtil.OPMode.Open);
            lObjCustomer.Show();


        }

        private void listToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CustomerForm lObjCustomer = new CustomerForm(MasterMechUtil.OPMode.Delete);
            lObjCustomer.Show();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemForm lObjItem = new ItemForm(MasterMechUtil.OPMode.Open);
            lObjItem.Show();

        }

        private void listToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemForm lObjItem = new ItemForm(MasterMechUtil.OPMode.Delete);
            lObjItem.Show();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Items lObjItem = new Items();
            List<Items> lObjItemResultList = lObjItem.ListData("");

            ItemForm lObjItemForm = new ItemForm();
            lObjItemForm.ItemData(lObjItemResultList);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm lObjInvoice = new InvoiceForm(MasterMechUtil.OPMode.New);
            lObjInvoice.ShowDialog();
        }

        private void LabelFY_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm lObj = new InvoiceForm(MasterMechUtil.OPMode.Open);
            lObj.ShowDialog();
        }

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm lObj = new InvoiceForm(MasterMechUtil.OPMode.Delete);
            lObj.ShowDialog();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterMechConfigFormForm lObjConfigForm = new MasterMechConfigFormForm(MasterMechUtil.OPMode.Open);
            lObjConfigForm.ShowDialog();
        }
    }
}
