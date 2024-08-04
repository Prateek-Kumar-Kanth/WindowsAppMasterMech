using MasterMechData;
using MasterMechPrj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MainForm
{
    public partial class ItemForm : Form
    {
        MasterMechUtil.OPMode mMode;
        public ItemForm()
        {
            InitializeComponent();
        }
        public ItemForm(MasterMechUtil.OPMode iMode)
        {
            InitializeComponent();
            mMode = iMode;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Do you want to exit?", "Exit Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                Close();
            }
        }

        public void ItemData(List<Items> iObjItemResultList)
        {
            SearchResultForm lObjResult = new SearchResultForm();
            lObjResult.DataGridResult.Columns.Add("ItemNoCol", "Item No");
            lObjResult.DataGridResult.Columns.Add("ItemDescCol", "Item Description");
            lObjResult.DataGridResult.Columns.Add("ItemTypeCol", "Item Type");
            lObjResult.DataGridResult.Columns.Add("ItemCatCol", "Item Category");
            lObjResult.DataGridResult.Columns.Add("ItemPriceCol", "Item Price");
            lObjResult.DataGridResult.Columns.Add("ItemUOMCol", "Item UOM");
            lObjResult.DataGridResult.Columns.Add("ItemStatusCol", "Item Status");
            lObjResult.DataGridResult.Columns.Add("CGST", "CGST");
            lObjResult.DataGridResult.Columns.Add("SGST", "SGST");
            lObjResult.DataGridResult.Columns.Add("IGST", "IGST");

            lObjResult.DataGridResult.Columns.Add("UPCCol", "UPC Code");
            lObjResult.DataGridResult.Columns.Add("HSNCol", "HSN Code");
            lObjResult.DataGridResult.Columns.Add("SACCol", "SAC Code");
            lObjResult.DataGridResult.Columns.Add("QtyInHandCol", "Quantity In Hand");
            lObjResult.DataGridResult.Columns.Add("ReorderLvlCol", "Reorder Level");
            lObjResult.DataGridResult.Columns.Add("ReorderQtyCol", "Reorder Quantity");
            lObjResult.DataGridResult.Columns.Add("NoOfPartsCol", "No. Of Parts");
            lObjResult.DataGridResult.Columns.Add("ItemRemarksCol", "Item Remarks");


            lObjResult.DataGridResult.Columns.Add("CreatedCol", "Created");
            lObjResult.DataGridResult.Columns.Add("CreatedByCol", "Created By");
            lObjResult.DataGridResult.Columns.Add("ModifiedCol", "Modified");
            lObjResult.DataGridResult.Columns.Add("ModifiedByCol", "ModiFied By");


            foreach (Items lObjItems in iObjItemResultList)
            {

                lObjResult.DataGridResult.Rows.Add(
                    lObjItems.mnItemNo,
                    lObjItems.msItemDesc,
                    lObjItems.msItemType,
                    lObjItems.msItemCategory,
                    lObjItems.mnItemPrice,
                    lObjItems.msItemUOM,
                    lObjItems.msItemStatus,
                    lObjItems.mnCGST,
                    lObjItems.mnSGST,

                    lObjItems.mnIGST,
                    lObjItems.msUPCCode,
                    lObjItems.msHSNCode,
                    lObjItems.msSACCode,
                    lObjItems.mnQtyHand,
                    lObjItems.mnReorderLevel,
                    lObjItems.mnReorderQty,
                    lObjItems.mnNoOfParts,
                    lObjItems.msItemRemarks,

                    lObjItems.mdCreated,
                    lObjItems.msCreatedBy,
                    lObjItems.mdModified,
                    lObjItems.msModifiedBy);
            }
            DialogResult Result = lObjResult.ShowDialog();

            if (Result == DialogResult.OK)
            {
                //Column Name is not a Column Name, it's a Column's Variable Name

                TextBoxItemNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemNoCol"].Value.ToString();
                TextBoxItemDesc2.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemDescCol"].Value.ToString();
                ComboBoxType.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemTypeCol"].Value.ToString();
                ComboBoxCategory.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemCatCol"].Value.ToString();

                TextBoxPrice.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemPriceCol"].Value.ToString();
                TextBoxUOM.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemUOMCol"].Value.ToString();
                ComboBoxStatus.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemStatusCol"].Value.ToString();
                TextBoxCGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["CGST"].Value.ToString();

                TextBoxSGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["SGST"].Value.ToString();
                TextBoxIGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["IGST"].Value.ToString();

                TextBoxUPC.Text = lObjResult.DataGridResult.CurrentRow.Cells["UPCCol"].Value.ToString();
                TextBoxHSN.Text = lObjResult.DataGridResult.CurrentRow.Cells["HSNCol"].Value.ToString();
                TextBoxSAC.Text = lObjResult.DataGridResult.CurrentRow.Cells["SACCol"].Value.ToString();
                TextBoxQuantInHand.Text = lObjResult.DataGridResult.CurrentRow.Cells["QtyInHandCol"].Value.ToString();
                TextBoxReOrderLevel.Text = lObjResult.DataGridResult.CurrentRow.Cells["ReorderLvlCol"].Value.ToString();
                TextBoxReorderQty.Text = lObjResult.DataGridResult.CurrentRow.Cells["ReorderQtyCol"].Value.ToString();

                TextBoxNoOfParts.Text = lObjResult.DataGridResult.CurrentRow.Cells["NoOfPartsCol"].Value.ToString();
                TextBoxRemarks.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemRemarksCol"].Value.ToString();
                TextBoxCreated.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedCol"].Value.ToString();
                TextBoxCreatedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedByCol"].Value.ToString();

                TextBoxModified.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedCol"].Value.ToString();
                TextBoxModifiedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedByCol"].Value.ToString();

            }

        }
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            Items lObjItem = new Items();
            string lsItemDesc = TextBoxItemDesc1.Text;

            if (mMode == MasterMechUtil.OPMode.Open)
            {
                List<Items> lObjItemResultList = lObjItem.ListData(lsItemDesc);
                ItemData(lObjItemResultList);

            }
            else if (mMode == MasterMechUtil.OPMode.New)
            {

                //bool lbIDFound = lObjItem.SearchUniqueID(lsItemDesc);

                //if (lbIDFound)
                //    MessageBox.Show("Duplicate User ID");
                //else
                //    MessageBox.Show("Valid User ID. You can continue");
            }


            //Items lObjItems = new Items();


            //if (mMode == MasterMechUtil.OPMode.Open)
            //{
            //    string lsItemDesc = TextBoxItemDesc1.Text;
            //    List<Items> ListItemData = lObjItems.ListData(lsItemDesc);

            //    SearchResultForm lObjResult = new SearchResultForm(ListItemData, MasterMechUtil.FormType.Item);
            //    DialogResult Result = lObjResult.ShowDialog();

            //    if (Result == DialogResult.OK)
            //    {
            //        //Column Name is not a Column Name, it's a Column's Variable Name

            //        TextBoxItemNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemNoCol"].Value.ToString();
            //        TextBoxItemDesc2.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemDescCol"].Value.ToString();
            //        ComboBoxType.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemTypeCol"].Value.ToString();
            //        ComboBoxCategory.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemCatCol"].Value.ToString();

            //        TextBoxPrice.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemPriceCol"].Value.ToString();
            //        TextBoxUOM.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemUOMCol"].Value.ToString();
            //        ComboBoxStatus.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemStatusCol"].Value.ToString();
            //        TextBoxCGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["CGST"].Value.ToString();

            //        TextBoxSGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["SGST"].Value.ToString();
            //        TextBoxIGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["IGST"].Value.ToString();

            //        TextBoxUPC.Text = lObjResult.DataGridResult.CurrentRow.Cells["UPCCol"].Value.ToString();
            //        TextBoxHSN.Text = lObjResult.DataGridResult.CurrentRow.Cells["HSNCol"].Value.ToString();
            //        TextBoxSAC.Text = lObjResult.DataGridResult.CurrentRow.Cells["SACCol"].Value.ToString();
            //        TextBoxQuantInHand.Text = lObjResult.DataGridResult.CurrentRow.Cells["QtyInHandCol"].Value.ToString();
            //        TextBoxReOrderLevel.Text = lObjResult.DataGridResult.CurrentRow.Cells["ReorderLvlCol"].Value.ToString();
            //        TextBoxReorderQty.Text = lObjResult.DataGridResult.CurrentRow.Cells["ReorderQtyCol"].Value.ToString();

            //        TextBoxNoOfParts.Text = lObjResult.DataGridResult.CurrentRow.Cells["NoOfPartsCol"].Value.ToString();
            //        TextBoxRemarks.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemRemarksCol"].Value.ToString();
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

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Items lObjItems = new Items();

            if (mMode == MasterMechUtil.OPMode.Delete)
            {
                string lsItemNo = TextBoxItemNo.Text;

                DialogResult Result = MessageBox.Show("Do you want to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == DialogResult.Yes)
                    lObjItems.DeleteData(lsItemNo);
            }
            else
            {
                lObjItems.mnItemNo = int.Parse(TextBoxItemNo.Text);
                lObjItems.msItemDesc = TextBoxItemDesc2.Text;

                lObjItems.msItemType = ComboBoxType.Text;
                lObjItems.msItemCategory = ComboBoxCategory.Text;
                lObjItems.mnItemPrice = int.Parse(TextBoxPrice.Text);
                lObjItems.msItemUOM = TextBoxUOM.Text;
                lObjItems.msItemStatus = ComboBoxStatus.Text;
                lObjItems.mnCGST = double.Parse(TextBoxCGST.Text);
                lObjItems.mnSGST = double.Parse(TextBoxSGST.Text);
                lObjItems.mnIGST = double.Parse(TextBoxIGST.Text);
                lObjItems.msUPCCode = TextBoxUPC.Text;
                lObjItems.msHSNCode = TextBoxHSN.Text;
                lObjItems.msSACCode = TextBoxSAC.Text;
                lObjItems.mnQtyHand = double.Parse(TextBoxQuantInHand.Text);
                lObjItems.mnReorderLevel = double.Parse(TextBoxReOrderLevel.Text);
                lObjItems.mnReorderQty = double.Parse(TextBoxReorderQty.Text);
                lObjItems.mnNoOfParts = int.Parse(TextBoxNoOfParts.Text);
                lObjItems.msItemRemarks = TextBoxRemarks.Text;

                lObjItems.msCreatedBy = TextBoxCreatedBy.Text; // UserId is from LoginPage
                //lObjUser.mdCreated = DateTime.Parse(TextBoxCreated.Text);
                lObjItems.mdCreated = DateTime.Now;
                //lObjUser.mdModified = DateTime.Parse(TextBoxModified.Text);
                lObjItems.mdModified = DateTime.Now;

                lObjItems.msModifiedBy = "";

               
                lObjItems.SaveSQL(mMode);

            }
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            if (mMode == MasterMechUtil.OPMode.New)
            {
                // to do something
                
            }
            else if (mMode == MasterMechUtil.OPMode.Open)
            {
                ButtonSave.Text = "Update";
            }
            else if (mMode == MasterMechUtil.OPMode.Delete)
            {
                ButtonSave.Text = "Delete";
            }
        }

        private void ButtonAdvSearch_Click(object sender, EventArgs e)
        {

            if (mMode == MasterMechUtil.OPMode.Open)
            {
                ItemAdvSearchForm lObj = new ItemAdvSearchForm(this);
                lObj.ShowDialog();

            }
        }
    }
}
