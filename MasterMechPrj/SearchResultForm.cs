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
    public partial class SearchResultForm : Form
    {
        List<User> lObjUserResultList;
        List<Customer> lObjCustomerResultList;
        List<Items> lObjItemsResultList;
        MasterMechUtil.FormType mMode;
        CustomerForm mObjCustForm;
        public SearchResultForm()
        {
            InitializeComponent();
            

        }
        public SearchResultForm(CustomerForm iObjCustForm, MasterMechUtil.FormType iFormType)
        {
            InitializeComponent();
            mMode = iFormType;
            mObjCustForm = iObjCustForm;
        }
        public SearchResultForm(List<User> iListResult, MasterMechUtil.FormType iMode)
        {
            InitializeComponent();
            lObjUserResultList = iListResult;
            mMode = iMode;
        }
        public SearchResultForm(List<Items> iListResult, MasterMechUtil.FormType iMode)
        {
            InitializeComponent();
            lObjItemsResultList = iListResult;
            mMode = iMode;
        }
        public SearchResultForm(List<Customer> iListResult, MasterMechUtil.FormType iMode)
        {
            InitializeComponent();
            lObjCustomerResultList = iListResult;
            mMode = iMode;
        }
        //public void UserData()
        //{

        //    DataGridResult.Columns.Add("UserIDCol", "User ID");
        //    DataGridResult.Columns.Add("PasswordCol", "Password");
        //    DataGridResult.Columns.Add("UserNameCol", "User Name");
        //    DataGridResult.Columns.Add("MobileCol", "Mobile Number");
        //    DataGridResult.Columns.Add("EmailCol", "Email");
        //    DataGridResult.Columns.Add("UserTypeCol", "User Type");
        //    DataGridResult.Columns.Add("LastLoginCol", "Last Login Time");
        //    DataGridResult.Columns.Add("LastPwdChangeCol", "Last Password Change Time");
        //    DataGridResult.Columns.Add("RemarksCol", "Remarks");
        //    DataGridResult.Columns.Add("CreatedCol", "Created");
        //    DataGridResult.Columns.Add("CreatedByCol", "Created By");
        //    DataGridResult.Columns.Add("ModifiedCol", "Modified");
        //    DataGridResult.Columns.Add("ModifiedByCol", "ModiFied By");


        //    foreach (User lObj in lObjUserResultList)
        //    {

        //        DataGridResult.Rows.Add(
        //            lObj.msUserID,
        //            lObj.msPassword,
        //            lObj.msUserName,
        //            lObj.msMobileNo,
        //            lObj.msEmail,
        //            lObj.msUserType,
        //            lObj.mdLastLoginTime,
        //            lObj.mdLastPasswordChangeTime,
        //            lObj.msRemarks,
        //            lObj.mdCreated,
        //            lObj.msCreatedBy,
        //            lObj.mdModified,
        //            lObj.msModifiedBy);
        //    }

        //}

        //public void CustomerData()
        //{
            
        //    DataGridResult.Columns.Add("CustNoCol", "Customer No");
        //    DataGridResult.Columns.Add("FirstNameCol", "First Name");
        //    DataGridResult.Columns.Add("LastNameCol", "Last Name");
        //    DataGridResult.Columns.Add("MobileCol", "Mobile Number");
        //    DataGridResult.Columns.Add("EmailCol", "Email");
        //    DataGridResult.Columns.Add("StatusCol", "Status");
        //    DataGridResult.Columns.Add("TypeCol", "Type");
        //    DataGridResult.Columns.Add("StreetCol", "Street");
        //    DataGridResult.Columns.Add("AreaCol", "Area");
        //    DataGridResult.Columns.Add("CityCol", "City");
        //    DataGridResult.Columns.Add("StateCol", "State");
        //    DataGridResult.Columns.Add("PincodeCol", "Pincode");
        //    DataGridResult.Columns.Add("CountryCol", "Country");
        //    DataGridResult.Columns.Add("GSTCol", "GST No.");
        //    DataGridResult.Columns.Add("LastVisitCol", "Last Visit");
        //    DataGridResult.Columns.Add("RemarksCol", "Remarks");
        //    DataGridResult.Columns.Add("CreatedCol", "Created");
        //    DataGridResult.Columns.Add("CreatedByCol", "CreatedBy");
        //    DataGridResult.Columns.Add("ModifiedCol", "Modified");
        //    DataGridResult.Columns.Add("ModifiedByCol", "ModifiedBy");

        //    foreach (Customer lObj in lObjCustomerResultList)
        //    {

        //        DataGridResult.Rows.Add(
        //            lObj.mnCustomerNo,
        //            lObj.msCustFirstName, 
        //            lObj.msCustLastName, 
        //            lObj.msCustMobile, 
        //            lObj.msCustEmail, 
        //            lObj.msCustStatus, 
        //            lObj.msCustType, 
        //            lObj.msCustStreet, 
        //            lObj.msCustArea, 
        //            lObj.msCustCity, 
        //            lObj.msCustStatus, 
        //            lObj.msCustPincode, 
        //            lObj.msCustCountry, 
        //            lObj.msCustGSTNo, 
        //            lObj.mdCustLastVisit, 
        //            lObj.msCustRemarks, 
        //            lObj.mdCustCreated, 
        //            lObj.msCustCreatedBy, 
        //            lObj.mdCustModified, 
        //            lObj.msCustModifiedBy);
        //    }

            
        //}

        //public void ItemData()
        //{

        //    DataGridResult.Columns.Add("ItemNoCol", "Item No");
        //    DataGridResult.Columns.Add("ItemDescCol", "Item Description");
        //    DataGridResult.Columns.Add("ItemTypeCol", "Item Type");
        //    DataGridResult.Columns.Add("ItemCatCol", "Item Category");
        //    DataGridResult.Columns.Add("ItemPriceCol", "Item Price");
        //    DataGridResult.Columns.Add("ItemUOMCol", "Item UOM");
        //    DataGridResult.Columns.Add("ItemStatusCol", "Item Status");
        //    DataGridResult.Columns.Add("CGST", "CGST");
        //    DataGridResult.Columns.Add("SGST", "SGST");
        //    DataGridResult.Columns.Add("IGST", "IGST");

        //    DataGridResult.Columns.Add("UPCCol", "UPC Code");
        //    DataGridResult.Columns.Add("HSNCol", "HSN Code");
        //    DataGridResult.Columns.Add("SACCol", "SAC Code");
        //    DataGridResult.Columns.Add("QtyInHandCol", "Quantity In Hand");
        //    DataGridResult.Columns.Add("ReorderLvlCol", "Reorder Level");
        //    DataGridResult.Columns.Add("ReorderQtyCol", "Reorder Quantity");
        //    DataGridResult.Columns.Add("NoOfPartsCol", "No. Of Parts");
        //    DataGridResult.Columns.Add("ItemRemarksCol", "Item Remarks");


        //    DataGridResult.Columns.Add("CreatedCol", "Created");
        //    DataGridResult.Columns.Add("CreatedByCol", "Created By");
        //    DataGridResult.Columns.Add("ModifiedCol", "Modified");
        //    DataGridResult.Columns.Add("ModifiedByCol", "ModiFied By");


        //    foreach (Items lObjItems in lObjItemsResultList)
        //    {

        //        DataGridResult.Rows.Add(
        //            lObjItems.mnItemNo,
        //            lObjItems.msItemDesc,
        //            lObjItems.msItemType,
        //            lObjItems.msItemCategory,
        //            lObjItems.mnItemPrice,
        //            lObjItems.msItemUOM,
        //            lObjItems.msItemStatus,
        //            lObjItems.mnCGST,
        //            lObjItems.mnSGST,

        //            lObjItems.mnIGST,
        //            lObjItems.msUPCCode,
        //            lObjItems.msHSNCode,
        //            lObjItems.msSACCode,
        //            lObjItems.mnQtyHand,
        //            lObjItems.mnReorderLevel,
        //            lObjItems.mnReorderQty,
        //            lObjItems.mnNoOfParts ,
        //            lObjItems.msItemRemarks,

        //            lObjItems.mdCreated,
        //            lObjItems.msCreatedBy,
        //            lObjItems.mdModified,
        //            lObjItems.msModifiedBy);
        //    }

        //}
        private void SearchResultForm_Load(object sender, EventArgs e)
        {
            
            //else if(mMode == MasterMechUtil.FormType.Customer)
            //{
            //    CustomerData();
            //}
            //else if(mMode == MasterMechUtil.FormType.Item)
            //{
            //    ItemData();
            //}
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Do you want to exit?", "Exit Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        

        private void DataGridResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridResult.Rows[e.RowIndex].Selected = true;
            
        }
    }
}
