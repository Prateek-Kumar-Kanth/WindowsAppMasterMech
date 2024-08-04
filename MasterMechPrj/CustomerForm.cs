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
using MasterMechData;
using MasterMechPrj;

namespace MainForm
{
    public partial class CustomerForm : Form
    {
        MasterMechUtil.OPMode mMode;
        public CustomerForm()
        {
            InitializeComponent();
        }
        public CustomerForm(MasterMechUtil.OPMode iMode)
        {
            InitializeComponent();
            mMode = iMode;
        }

        public void CustomerData(List<Customer> iObjCustomerResultList)
        {
            SearchResultForm lObjResult = new SearchResultForm(this,MasterMechUtil.FormType.User);
            lObjResult.DataGridResult.Columns.Add("CustNoCol", "Customer No");
            lObjResult.DataGridResult.Columns.Add("FirstNameCol", "First Name");
            lObjResult.DataGridResult.Columns.Add("LastNameCol", "Last Name");
            lObjResult.DataGridResult.Columns.Add("MobileCol", "Mobile Number");
            lObjResult.DataGridResult.Columns.Add("EmailCol", "Email");
            lObjResult.DataGridResult.Columns.Add("StatusCol", "Status");
            lObjResult.DataGridResult.Columns.Add("TypeCol", "Type");
            lObjResult.DataGridResult.Columns.Add("StreetCol", "Street");
            lObjResult.DataGridResult.Columns.Add("AreaCol", "Area");
            lObjResult.DataGridResult.Columns.Add("CityCol", "City");
            lObjResult.DataGridResult.Columns.Add("StateCol", "State");
            lObjResult.DataGridResult.Columns.Add("PincodeCol", "Pincode");
            lObjResult.DataGridResult.Columns.Add("CountryCol", "Country");
            lObjResult.DataGridResult.Columns.Add("GSTCol", "GST No.");
            lObjResult.DataGridResult.Columns.Add("LastVisitCol", "Last Visit");
            lObjResult.DataGridResult.Columns.Add("RemarksCol", "Remarks");
            lObjResult.DataGridResult.Columns.Add("CreatedCol", "Created");
            lObjResult.DataGridResult.Columns.Add("CreatedByCol", "CreatedBy");
            lObjResult.DataGridResult.Columns.Add("ModifiedCol", "Modified");
            lObjResult.DataGridResult.Columns.Add("ModifiedByCol", "ModifiedBy");

            foreach (Customer lObj in iObjCustomerResultList)
            {

                lObjResult.DataGridResult.Rows.Add(
                    lObj.mnCustomerNo,
                    lObj.msCustFirstName,
                    lObj.msCustLastName,
                    lObj.msCustMobile,
                    lObj.msCustEmail,
                    lObj.msCustStatus,
                    lObj.msCustType,
                    lObj.msCustStreet,
                    lObj.msCustArea,
                    lObj.msCustCity,
                    lObj.msCustStatus,
                    lObj.msCustPincode,
                    lObj.msCustCountry,
                    lObj.msCustGSTNo,
                    lObj.mdCustLastVisit,
                    lObj.msCustRemarks,
                    lObj.mdCustCreated,
                    lObj.msCustCreatedBy,
                    lObj.mdCustModified,
                    lObj.msCustModifiedBy);
            }

            DialogResult Result = lObjResult.ShowDialog();
            if (Result == DialogResult.OK)
            {
                //Column Name is not a Column Name, it's a Column's Variable Name

                TextBoxCustNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustNoCol"].Value.ToString();
                TextBoxFirstName.Text = lObjResult.DataGridResult.CurrentRow.Cells["FirstNameCol"].Value.ToString();
                TextBoxLastName.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastNameCol"].Value.ToString();
                TextBoxMobileNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["MobileCol"].Value.ToString();

                TextBoxEmail.Text = lObjResult.DataGridResult.CurrentRow.Cells["EmailCol"].Value.ToString();
                ComboBoxStatus.Text = lObjResult.DataGridResult.CurrentRow.Cells["StatusCol"].Value.ToString();
                ComboBoxType.Text = lObjResult.DataGridResult.CurrentRow.Cells["TypeCol"].Value.ToString();
                TextBoxStreet.Text = lObjResult.DataGridResult.CurrentRow.Cells["StreetCol"].Value.ToString();

                TextBoxArea.Text = lObjResult.DataGridResult.CurrentRow.Cells["AreaCol"].Value.ToString();
                TextBoxCity.Text = lObjResult.DataGridResult.CurrentRow.Cells["CityCol"].Value.ToString();

                TextBoxState.Text = lObjResult.DataGridResult.CurrentRow.Cells["StateCol"].Value.ToString();
                TextBoxPinCode.Text = lObjResult.DataGridResult.CurrentRow.Cells["PincodeCol"].Value.ToString();
                TextBoxCountry.Text = lObjResult.DataGridResult.CurrentRow.Cells["CountryCol"].Value.ToString();
                TextBoxGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["GSTCol"].Value.ToString();
                TextBoxLastVisited.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastVisitCol"].Value.ToString();
                TextBoxRemarks.Text = lObjResult.DataGridResult.CurrentRow.Cells["RemarksCol"].Value.ToString();


                TextBoxCreated.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedCol"].Value.ToString();
                TextBoxCreatedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedByCol"].Value.ToString();

                TextBoxModified.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedCol"].Value.ToString();
                TextBoxModifiedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedByCol"].Value.ToString();



            }


        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Do you want to exit?", "Exit Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Customer lObjCustomer = new Customer();

            if (mMode == MasterMechUtil.OPMode.Delete)
            {
                string lsCustNo = TextBoxCustNo.Text;
                
                DialogResult Result = MessageBox.Show("Do you want to delete?","Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if(Result==DialogResult.Yes)
                    lObjCustomer.DeleteData(lsCustNo);
                
            }
            else
            {
                if(mMode==MasterMechUtil.OPMode.Open)
                lObjCustomer.mnCustomerNo = int.Parse(TextBoxCustNo.Text); // because It is an Identity field
                lObjCustomer.msCustFirstName = TextBoxFirstName.Text;

                lObjCustomer.msCustLastName = TextBoxLastName.Text;
                lObjCustomer.msCustMobile = TextBoxMobileNo.Text;
                lObjCustomer.msCustEmail = TextBoxEmail.Text;
                lObjCustomer.msCustStatus = ComboBoxStatus.Text;
                lObjCustomer.msCustType = ComboBoxType.Text;

                lObjCustomer.msCustStreet = TextBoxStreet.Text;
                lObjCustomer.msCustArea = TextBoxArea.Text;
                lObjCustomer.msCustCity = TextBoxCity.Text;
                lObjCustomer.msCustState = TextBoxState.Text;
                lObjCustomer.msCustPincode = TextBoxPinCode.Text;
                lObjCustomer.msCustCountry = TextBoxCountry.Text;
                lObjCustomer.msCustGSTNo = TextBoxGST.Text;
                lObjCustomer.mdCustLastVisit = DateTime.Parse(TextBoxLastVisited.Text);
                lObjCustomer.msCustRemarks = TextBoxRemarks.Text;
                lObjCustomer.mdCustCreated = DateTime.Parse(TextBoxCreated.Text);
                lObjCustomer.msCustCreatedBy = TextBoxCreatedBy.Text;
                lObjCustomer.mdCustModified = DateTime.Parse(TextBoxModified.Text);
                lObjCustomer.msCustModifiedBy = TextBoxModifiedBy.Text;

                lObjCustomer.SaveSQL(mMode);

            }

            
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            if (mMode == MasterMechUtil.OPMode.New)
            {
                TextBoxCustNo.ReadOnly = true;
                ActiveControl = TextBoxFirstName; //To set Focus(Blinking Cursor to given TextBox)
            }
            else if (mMode == MasterMechUtil.OPMode.Open)
            {
                ButtonSave.Text = "Update";
            }
            else if(mMode == MasterMechUtil.OPMode.Delete)
            {
                ButtonSave.Text = "Delete";
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {

            Customer lObjCust = new Customer();
            string lsCustMob = TextBoxMobileSearch.Text;

            if (mMode == MasterMechUtil.OPMode.Open)
            {
                List<Customer> lObjUserResultList = lObjCust.ListData(lsCustMob);
                CustomerData(lObjUserResultList);

            }
            else if (mMode == MasterMechUtil.OPMode.New)
            {

                //bool lbIDFound = lObjCust.SearchUniqueID(lsCustMob);

                //if (lbIDFound)
                //    MessageBox.Show("Duplicate User ID");
                //else
                //    MessageBox.Show("Valid User ID. You can continue");
            }
            //Customer lObjItems = new Customer();


            //if (mMode == MasterMechUtil.OPMode.Open)
            //{
            //    string lsMobNo = TextBoxMobileSearch.Text;
            //    List<Customer> ListItemData = lObjItems.ListData(lsMobNo);

            //    SearchResultForm lObjResult = new SearchResultForm(ListItemData, MasterMechUtil.FormType.Customer);
            //    DialogResult Result = lObjResult.ShowDialog();

            //    if (Result == DialogResult.OK)
            //    {
            //        //Column Name is not a Column Name, it's a Column's Variable Name

            //        TextBoxCustNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustNoCol"].Value.ToString();
            //        TextBoxFirstName.Text = lObjResult.DataGridResult.CurrentRow.Cells["FirstNameCol"].Value.ToString();
            //        TextBoxLastName.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastNameCol"].Value.ToString();
            //        TextBoxMobileNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["MobileCol"].Value.ToString();

            //        TextBoxEmail.Text = lObjResult.DataGridResult.CurrentRow.Cells["EmailCol"].Value.ToString();
            //        ComboBoxStatus.Text = lObjResult.DataGridResult.CurrentRow.Cells["StatusCol"].Value.ToString();
            //        ComboBoxType.Text = lObjResult.DataGridResult.CurrentRow.Cells["TypeCol"].Value.ToString();
            //        TextBoxStreet.Text = lObjResult.DataGridResult.CurrentRow.Cells["StreetCol"].Value.ToString();

            //        TextBoxArea.Text = lObjResult.DataGridResult.CurrentRow.Cells["AreaCol"].Value.ToString();
            //        TextBoxCity.Text = lObjResult.DataGridResult.CurrentRow.Cells["CityCol"].Value.ToString();

            //        TextBoxState.Text = lObjResult.DataGridResult.CurrentRow.Cells["StateCol"].Value.ToString();
            //        TextBoxPinCode.Text = lObjResult.DataGridResult.CurrentRow.Cells["PincodeCol"].Value.ToString();
            //        TextBoxCountry.Text = lObjResult.DataGridResult.CurrentRow.Cells["CountryCol"].Value.ToString();
            //        TextBoxGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["GSTCol"].Value.ToString();
            //        TextBoxLastVisited.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastVisitCol"].Value.ToString();
            //        TextBoxRemarks.Text = lObjResult.DataGridResult.CurrentRow.Cells["RemarksCol"].Value.ToString();


            //        TextBoxCreated.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedCol"].Value.ToString();
            //        TextBoxCreatedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedByCol"].Value.ToString();

            //        TextBoxModified.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedCol"].Value.ToString();
            //        TextBoxModifiedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedByCol"].Value.ToString();



            //    }

            //}
            //else if (mMode == MasterMechUtil.OPMode.New)
            //{
            //    //string lsItemDesc = TextBoxItemDesc1.Text;
            //    //bool lbIDFound = lObjItems.SearchUniqueID(lsItemDesc);

            //    //if (lbIDFound)
            //    //    MessageBox.Show("Duplicate User ID");
            //    //else
            //    //    MessageBox.Show("Valid User ID. You can continue");
            //}
        }

        private void ButtonAdvSearch_Click(object sender, EventArgs e)
        {

            if (mMode == MasterMechUtil.OPMode.Open)
            {
                CustAdvanceSearchForm lObj = new CustAdvanceSearchForm(this);
                lObj.ShowDialog();

            }
        }
    }
}
