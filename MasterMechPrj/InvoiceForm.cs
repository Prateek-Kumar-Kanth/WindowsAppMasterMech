using MainForm;
using MasterMechData;
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
using System.Windows.Forms.VisualStyles;

namespace MasterMechPrj
{
    public partial class InvoiceForm : Form
    {
        public int lnSno = 1; // For GridBill Serial Number
        public string lsItemType = "";
        public string lsHSN = "";
        public string lsUPC = "";
        public string lsSAC = "";
        public string lsCat = "";
        public string lsStatus = "";
        public int lnTotalQuantityInHand = 0;
   
        MasterMechUtil.OPMode mOperation = MasterMechUtil.OPMode.New;

        //string lsSelectedNetAmt;
        public int lnSelectedRowIndex;
        MasterMechUtil.OPMode mMode;
        public InvoiceForm()
        {
            InitializeComponent();
           
        }
        public InvoiceForm(MasterMechUtil.OPMode iMode)
        {
            InitializeComponent();
            mMode = iMode;
        }

        public void BiillingTabUpdate()
        {
            if (GridBill.Rows.Count > 0)
            {  
                double lnSumParts = 0;
                double lnSumLabour = 0;

                double lnPartsSGST = 0;
                double lnPartsCGST = 0;
                double lnPartsIGST = 0;

                double lnLabourSGST = 0;
                double lnLabourCGST = 0;
                double lnLabourIGST = 0;
                double lnDiscount = 0;

                foreach (DataGridViewRow Records in GridBill.Rows)
                {
                    string lsRowMode = Records.Cells["RowMode"].Value.ToString();
                    if (Records.Cells["TypeCol"].Value.ToString() == "Parts" && lsRowMode != "Delete" )
                    {
                        string lsNetAmt = (Records.Cells["NetAmtCol"].Value).ToString();
                        lnSumParts += double.Parse(lsNetAmt);

                        string lnSGSTValue = (Records.Cells["SGSTCol"].Value).ToString();
                        lnPartsSGST += double.Parse(lnSGSTValue);

                        string lnCGSTValue = (Records.Cells["CGSTCol"].Value).ToString();
                        lnPartsCGST += double.Parse(lnCGSTValue);

                        string lnIGSTValue = (Records.Cells["IGSTCol"].Value).ToString();
                        lnPartsIGST += double.Parse(lnIGSTValue);

                    }
                    else if (Records.Cells["TypeCol"].Value.ToString() == "Labour" && lsRowMode != "Delete")
                    {
                        string lsNetAmt = (Records.Cells["NetAmtCol"].Value).ToString();
                        lnSumLabour += double.Parse(lsNetAmt);

                        string lnSGSTValue = (Records.Cells["SGSTCol"].Value).ToString();
                        lnLabourSGST += double.Parse(lnSGSTValue);

                        string lnCGSTValue = (Records.Cells["CGSTCol"].Value).ToString();
                        lnLabourCGST += double.Parse(lnCGSTValue);

                        string lnIGSTValue = (Records.Cells["IGSTCol"].Value).ToString();
                        lnLabourIGST += double.Parse(lnIGSTValue);
                    }
                    string lsDis = (Records.Cells["DiscountCol"].Value).ToString();
                    lnDiscount += double.Parse(lsDis);
                }

                TextBoxPartsTotal.Text = lnSumParts.ToString();
                TextBoxPartsSGST.Text = lnPartsSGST.ToString();
                TextBoxPartsCGST.Text = lnPartsCGST.ToString();
                TextBoxPartsIGST.Text = lnPartsIGST.ToString();
                TextBoxTotalDiscount.Text = lnDiscount.ToString();


                TextBoxLabourTotal.Text = lnSumLabour.ToString();
                TextBoxLabourSGST.Text = lnLabourSGST.ToString();
                TextBoxLabourCGST.Text = lnLabourCGST.ToString();
                TextBoxLabourIGST.Text = lnLabourIGST.ToString();

                TextBoxTotalNetAmt.Text = (int.Parse(TextBoxPartsTotal.Text) + int.Parse(TextBoxLabourTotal.Text)).ToString();
                TextBoxTotalCGST.Text = (int.Parse(TextBoxPartsCGST.Text) + int.Parse(TextBoxLabourCGST.Text)).ToString();
                TextBoxTotalSGST.Text = (int.Parse(TextBoxPartsSGST.Text) + int.Parse(TextBoxLabourSGST.Text)).ToString();
                TextBoxTotalIGST.Text = (int.Parse(TextBoxPartsIGST.Text) + int.Parse(TextBoxLabourIGST.Text)).ToString();

                TextBoxTotalTax.Text = (int.Parse(TextBoxTotalSGST.Text) + int.Parse(TextBoxTotalCGST.Text) + int.Parse(TextBoxTotalIGST.Text)).ToString();

                TextBoxGrandTotal.Text = (int.Parse(TextBoxTotalNetAmt.Text) + int.Parse(TextBoxTotalTax.Text)).ToString();
            }


        }
        public void ClearFields()
        {
            TextBoxItemNo.Text = string.Empty;
            TextBoxItemDesc.Text = string.Empty;
            TextBoxPrice.Text = string.Empty;
            TextBoxUOM.Text = string.Empty;
            TextBoxQty.Text = string.Empty;
            TextBoxGrossAmount.Text = string.Empty;
            TextBoxDiscount.Text = string.Empty;
            TextBoxSGST.Text = string.Empty;
            TextBoxSGSTValue.Text = string.Empty;
            TextBoxCGST.Text = string.Empty;
            TextBoxCGSTValue.Text = string.Empty;
            TextBoxIGST.Text = string.Empty;
            TextBoxIGSTValue.Text = string.Empty;
            TextBoxNetAmount.Text = string.Empty;
            TextBoxTax.Text = string.Empty;
            TextBoxTotalAmount.Text = string.Empty;
        }
        public bool CheckDuplicateDataInGrid(string isSelectedItemNo)
        {
            if (GridBill.Rows.Count > 0)
            {
                foreach (DataGridViewRow Records in GridBill.Rows)
                {
                    string lsRowMode = Records.Cells["RowMode"].Value.ToString();
                    if (lsRowMode != "Delete")
                    {

                        string lsGridItemNo = Records.Cells["ItemNoCol"].Value.ToString();

                        if (lsGridItemNo == isSelectedItemNo)
                        {
                            MessageBox.Show("You have already chosen this item. It you want to buy more then update it");
                            return true;
                        }
                    }
                    
                }
            }
            return false;
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
                    lObjItems.msItemRemarks);

                    
            }

            DialogResult Result = lObjResult.ShowDialog();

            string lsSelectedItemNo = lObjResult.DataGridResult.CurrentRow.Cells["ItemNoCol"].Value.ToString();
            
            if (Result == DialogResult.OK && CheckDuplicateDataInGrid(lsSelectedItemNo) == false)
            {
                
                //Column Name is not a Column Name, it's a Column's Variable Name

                TextBoxItemNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemNoCol"].Value.ToString();
                TextBoxItemDesc.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemDescCol"].Value.ToString();

                TextBoxPrice.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemPriceCol"].Value.ToString();
                TextBoxUOM.Text = lObjResult.DataGridResult.CurrentRow.Cells["ItemUOMCol"].Value.ToString();
                TextBoxIGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["IGST"].Value.ToString();

                TextBoxSGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["SGST"].Value.ToString();
                TextBoxCGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["CGST"].Value.ToString();


                TextBoxDiscount.Text = "0.00";

                // To Save into InvoiceItem Table, Not for GridBill
                lsItemType = lObjResult.DataGridResult.CurrentRow.Cells["ItemTypeCol"].Value.ToString();
                lsHSN = lObjResult.DataGridResult.CurrentRow.Cells["HSNCol"].Value.ToString();
                lsUPC = lObjResult.DataGridResult.CurrentRow.Cells["UPCCol"].Value.ToString();
                lsSAC = lObjResult.DataGridResult.CurrentRow.Cells["SACCol"].Value.ToString();
                lsCat = lObjResult.DataGridResult.CurrentRow.Cells["ItemCatCol"].Value.ToString();
                lsStatus = lObjResult.DataGridResult.CurrentRow.Cells["ItemStatusCol"].Value.ToString();
                lnTotalQuantityInHand = int.Parse(lObjResult.DataGridResult.CurrentRow.Cells["QtyInHandCol"].Value.ToString());

            }
            
            ButtonDelete.Visible = false;
            ButtonCancel.Visible = true;
            mOperation = MasterMechUtil.OPMode.New;


        }
        public void CustomerData(List<Customer> iObjCustomerResultList)
        {
            SearchResultForm lObjResult = new SearchResultForm();
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
                TextBoxCustFirstName.Text = lObjResult.DataGridResult.CurrentRow.Cells["FirstNameCol"].Value.ToString();
                TextBoxCustLastName.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastNameCol"].Value.ToString();
                TextBoxCustMobNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["MobileCol"].Value.ToString();

                TextBoxCustEmail.Text = lObjResult.DataGridResult.CurrentRow.Cells["EmailCol"].Value.ToString();
                ComboBoxStatus.Text = lObjResult.DataGridResult.CurrentRow.Cells["StatusCol"].Value.ToString();
                ComboBoxType.Text = lObjResult.DataGridResult.CurrentRow.Cells["TypeCol"].Value.ToString();
                TextBoxStreet.Text = lObjResult.DataGridResult.CurrentRow.Cells["StreetCol"].Value.ToString();

                TextBoxArea.Text = lObjResult.DataGridResult.CurrentRow.Cells["AreaCol"].Value.ToString();
                TextBoxCity.Text = lObjResult.DataGridResult.CurrentRow.Cells["CityCol"].Value.ToString();

                TextBoxState.Text = lObjResult.DataGridResult.CurrentRow.Cells["StateCol"].Value.ToString();
                TextBoxPincode.Text = lObjResult.DataGridResult.CurrentRow.Cells["PincodeCol"].Value.ToString();
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
        public void InvoiceData(List<Invoice> iObjInvoiceResultList)
        {
            SearchResultForm lObjResult = new SearchResultForm();
            //Assuming lObjResult is your DataGrid object
            lObjResult.DataGridResult.Columns.Add("InvoiceNo", "Invoice No");
            lObjResult.DataGridResult.Columns.Add("InvoiceSNo", "Invoice Serial No");
            lObjResult.DataGridResult.Columns.Add("InvoiceDate", "Invoice Date");
            lObjResult.DataGridResult.Columns.Add("InvoiceSts", "Invoice Status");
            lObjResult.DataGridResult.Columns.Add("CustNo", "Customer No");
            lObjResult.DataGridResult.Columns.Add("CustFName", "Customer First Name");
            lObjResult.DataGridResult.Columns.Add("CustLName", "Customer Last Name");
            lObjResult.DataGridResult.Columns.Add("CustMobNo", "Customer Mobile No");
            lObjResult.DataGridResult.Columns.Add("CustEmail", "Customer Email");
            lObjResult.DataGridResult.Columns.Add("CustSts", "Customer Status");
            lObjResult.DataGridResult.Columns.Add("CustType", "Customer Type");
            lObjResult.DataGridResult.Columns.Add("CustStAddr", "Customer Street Address");
            lObjResult.DataGridResult.Columns.Add("CustArAddr", "Customer Area Address");
            lObjResult.DataGridResult.Columns.Add("CustCity", "Customer City");
            lObjResult.DataGridResult.Columns.Add("CustState", "Customer State");
            lObjResult.DataGridResult.Columns.Add("CustPinCode", "Customer Pin Code");
            lObjResult.DataGridResult.Columns.Add("CustCountry", "Customer Country");
            lObjResult.DataGridResult.Columns.Add("CustGSTNo", "Customer GST No");
            lObjResult.DataGridResult.Columns.Add("CustlLastVisit", "Customer Last Visit");
            lObjResult.DataGridResult.Columns.Add("CustRemarks", "Customer Remarks");
            lObjResult.DataGridResult.Columns.Add("VehicleRegNo", "Vehicle Registration No");
            lObjResult.DataGridResult.Columns.Add("VehicleModel", "Vehicle Model");
            lObjResult.DataGridResult.Columns.Add("ChassisNo", "Chassis No");
            lObjResult.DataGridResult.Columns.Add("EngineNo", "Engine No");
            lObjResult.DataGridResult.Columns.Add("Mileage", "Mileage");
            lObjResult.DataGridResult.Columns.Add("ServiceType", "Service Type");
            lObjResult.DataGridResult.Columns.Add("ServiceAssoName", "Service Associate Name");
            lObjResult.DataGridResult.Columns.Add("ServiceAssoMobNo", "Service Associate Mobile No");
            lObjResult.DataGridResult.Columns.Add("PartsTotal", "Parts Total");
            lObjResult.DataGridResult.Columns.Add("LabourTotal", "Labour Total");
            lObjResult.DataGridResult.Columns.Add("PartsCGSTTotal", "Parts CGST Total");
            lObjResult.DataGridResult.Columns.Add("LabourCGSTTotal", "Labour CGST Total");
            lObjResult.DataGridResult.Columns.Add("PartsSGSTTotal", "Parts SGST Total");
            lObjResult.DataGridResult.Columns.Add("LabourSGSTTotal", "Labour SGST Total");
            lObjResult.DataGridResult.Columns.Add("PartsIGSTTotal", "Parts IGST Total");
            lObjResult.DataGridResult.Columns.Add("LabourIGSTTotal", "Labour IGST Total");
            lObjResult.DataGridResult.Columns.Add("TotalSGST", "Total SGST");
            lObjResult.DataGridResult.Columns.Add("TotalCGST", "Total CGST");
            lObjResult.DataGridResult.Columns.Add("TotalIGST", "Total IGST");
            lObjResult.DataGridResult.Columns.Add("TotalTax", "Total Tax");
            lObjResult.DataGridResult.Columns.Add("TotalAmount", "Total Amount");
            lObjResult.DataGridResult.Columns.Add("GrandTotal", "Grand Total");
            lObjResult.DataGridResult.Columns.Add("DiscountAmount", "Discount Amount");
            lObjResult.DataGridResult.Columns.Add("InvoiceTotal", "Invoice Total");
            lObjResult.DataGridResult.Columns.Add("InvoiceRemarks", "Invoice Remarks");
            lObjResult.DataGridResult.Columns.Add("Created", "Created");
            lObjResult.DataGridResult.Columns.Add("CreatedBy", "Created By");
            lObjResult.DataGridResult.Columns.Add("Modified", "Modified");
            lObjResult.DataGridResult.Columns.Add("ModifiedBy", "Modified By");
           


            foreach (Invoice lObj in iObjInvoiceResultList)
            {

                // Assuming lObjResult is your DataGrid object
                // Assuming lObj is an instance of a class representing the data from the table

                lObjResult.DataGridResult.Rows.Add(
                    lObj.msInvoiceNo,
                    lObj.mnInvoiceSNo,
                    lObj.mdInvoiceDate,
                    lObj.msInvoiceSts,
                    lObj.mnCustNo,
                    lObj.msCustFName,
                    lObj.msCustLName,
                    lObj.msCustMobNo,
                    lObj.msCustEmail,
                    lObj.msCustSts,
                    lObj.msCustType,
                    lObj.msCustStAddr,
                    lObj.msCustArAddr,
                    lObj.msCustCity,
                    lObj.msCustState,
                    lObj.msCustPinCode,
                    lObj.msCustCountry,
                    lObj.msCustGSTNo,
                    lObj.mdCustlLastVisit,
                    lObj.msCustRemarks,
                    lObj.msVehicleRegNo,
                    lObj.msVehicleModel,
                    lObj.msChassisNo,
                    lObj.msEngineNo,
                    lObj.mnMileage,
                    lObj.msServiceType,
                    lObj.msServiceAssoName,
                    lObj.msServiceAssoMobNo,
                    lObj.mnPartsTotal,
                    lObj.mnLabourTotal,
                    lObj.mnPartsCGSTTotal,
                    lObj.mnLabourCGSTTotal,
                    lObj.mnPartsSGSTTotal,
                    lObj.mnLabourSGSTTotal,
                    lObj.mnPartsIGSTTotal,
                    lObj.mnLabourIGSTTotal,
                    lObj.mnTotalSGST,
                    lObj.mnTotalCGST,
                    lObj.mnTotalIGST,
                    lObj.mnTotalTax,
                    lObj.mnTotalAmount,
                    lObj.mnGrandTotal,
                    lObj.mnDiscountAmount,
                    lObj.mnInvoiceTotal,
                    lObj.msInvoiceRemarks,
                    lObj.mdCreated,
                    lObj.msCreatedBy,
                    lObj.mdModified,
                    lObj.msModifiedBy
                   
                );
                
                // Visible Only Some Fields and rest are false

               // lObjResult.DataGridResult.Columns["InvoiceNo"].Visible = false;
                //lObjResult.DataGridResult.Columns["InvoiceSNo"].Visible = false;
                //lObjResult.DataGridResult.Columns["InvoiceDate"].Visible = false;
                //lObjResult.DataGridResult.Columns["InvoiceSts"].Visible = false;
                //lObjResult.DataGridResult.Columns["CustNo"].Visible = false;
                //lObjResult.DataGridResult.Columns["CustFName"].Visible = false;
                //lObjResult.DataGridResult.Columns["CustLName"].Visible = false;
                //lObjResult.DataGridResult.Columns["CustMobNo"].Visible = false;
                //lObjResult.DataGridResult.Columns["CustEmail"].Visible = false;
                //lObjResult.DataGridResult.Columns["CustSts"].Visible = false;
                lObjResult.DataGridResult.Columns["CustType"].Visible = false;
                lObjResult.DataGridResult.Columns["CustStAddr"].Visible = false;
                lObjResult.DataGridResult.Columns["CustArAddr"].Visible = false;
                lObjResult.DataGridResult.Columns["CustCity"].Visible = false;
                lObjResult.DataGridResult.Columns["CustState"].Visible = false;
                lObjResult.DataGridResult.Columns["CustPinCode"].Visible = false;
                lObjResult.DataGridResult.Columns["CustCountry"].Visible = false;
                //lObjResult.DataGridResult.Columns["CustGSTNo"].Visible = false;
                lObjResult.DataGridResult.Columns["CustlLastVisit"].Visible = false;
                lObjResult.DataGridResult.Columns["CustRemarks"].Visible = false;
                //lObjResult.DataGridResult.Columns["VehicleRegNo"].Visible = false;
                //lObjResult.DataGridResult.Columns["VehicleModel"].Visible = false;
                lObjResult.DataGridResult.Columns["ChassisNo"].Visible = false;
                lObjResult.DataGridResult.Columns["EngineNo"].Visible = false;
                lObjResult.DataGridResult.Columns["Mileage"].Visible = false;
                lObjResult.DataGridResult.Columns["ServiceType"].Visible = false;
                lObjResult.DataGridResult.Columns["ServiceAssoName"].Visible = false;
                lObjResult.DataGridResult.Columns["ServiceAssoMobNo"].Visible = false;
                lObjResult.DataGridResult.Columns["PartsTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["LabourTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["PartsCGSTTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["LabourCGSTTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["PartsSGSTTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["LabourSGSTTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["PartsIGSTTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["LabourIGSTTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["TotalSGST"].Visible = false;
                lObjResult.DataGridResult.Columns["TotalCGST"].Visible = false;
                lObjResult.DataGridResult.Columns["TotalIGST"].Visible = false;
                lObjResult.DataGridResult.Columns["TotalTax"].Visible = false;
                lObjResult.DataGridResult.Columns["TotalAmount"].Visible = false;
                //lObjResult.DataGridResult.Columns["GrandTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["DiscountAmount"].Visible = false;
                lObjResult.DataGridResult.Columns["InvoiceTotal"].Visible = false;
                lObjResult.DataGridResult.Columns["InvoiceRemarks"].Visible = false;
                //lObjResult.DataGridResult.Columns["Created"].Visible = false;
                //lObjResult.DataGridResult.Columns["CreatedBy"].Visible = false;
                lObjResult.DataGridResult.Columns["Modified"].Visible = false;
                lObjResult.DataGridResult.Columns["ModifiedBy"].Visible = false;
               

            }

            DialogResult Result = lObjResult.ShowDialog();
            if (Result == DialogResult.OK)
            {
                GridBill.Rows.Clear(); // Clear Old Customer data first
                //Column Name is not a Column Name, it's a Column's Variable Name

                TextBoxInvoiceNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["InvoiceNo"].Value.ToString();
                Invoice.mnSearchInvoiceNo = (int?)lObjResult.DataGridResult.CurrentRow.Cells["InvoiceSNo"].Value;
                TextBoxInvoiceDate.Text = lObjResult.DataGridResult.CurrentRow.Cells["InvoiceDate"].Value.ToString();
                //TextBoxInvoiceSts.Text = lObjResult.DataGridResult.CurrentRow.Cells["InvoiceSts"].Value.ToString();
                TextBoxCustNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustNo"].Value.ToString();
                TextBoxCustFirstName.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustFName"].Value.ToString();
                TextBoxCustLastName.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustLName"].Value.ToString();
                TextBoxCustMobNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustMobNo"].Value.ToString();
                TextBoxCustEmail.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustEmail"].Value.ToString();
                //TextBoxCustSts.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustSts"].Value.ToString();
                ComboBoxType.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustType"].Value.ToString();
                TextBoxStreet.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustStAddr"].Value.ToString();
                TextBoxArea.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustArAddr"].Value.ToString();
                TextBoxCity.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustCity"].Value.ToString();
                TextBoxState.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustState"].Value.ToString();
                TextBoxPincode.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustPinCode"].Value.ToString();
                TextBoxCountry.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustCountry"].Value.ToString();
                TextBoxGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustGSTNo"].Value.ToString();
                TextBoxLastVisited.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustlLastVisit"].Value.ToString();
                TextBoxRemarks.Text = lObjResult.DataGridResult.CurrentRow.Cells["CustRemarks"].Value.ToString();
                TextBoxRegNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["VehicleRegNo"].Value.ToString();
                TextBoxModel.Text = lObjResult.DataGridResult.CurrentRow.Cells["VehicleModel"].Value.ToString();
                TextBoxChassisNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["ChassisNo"].Value.ToString();
                TextBoxEngineNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["EngineNo"].Value.ToString();
                TextBoxMilage.Text = lObjResult.DataGridResult.CurrentRow.Cells["Mileage"].Value.ToString();
                ComboBoxServiceType.Text = lObjResult.DataGridResult.CurrentRow.Cells["ServiceType"].Value.ToString();
                TextBoxServiceAssociate.Text = lObjResult.DataGridResult.CurrentRow.Cells["ServiceAssoName"].Value.ToString();
                TextBoxAssociateMobNo.Text = lObjResult.DataGridResult.CurrentRow.Cells["ServiceAssoMobNo"].Value.ToString();
                TextBoxPartsTotal.Text = lObjResult.DataGridResult.CurrentRow.Cells["PartsTotal"].Value.ToString();
                TextBoxLabourTotal.Text = lObjResult.DataGridResult.CurrentRow.Cells["LabourTotal"].Value.ToString();
                TextBoxPartsCGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["PartsCGSTTotal"].Value.ToString();
                TextBoxLabourCGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["LabourCGSTTotal"].Value.ToString();
                TextBoxPartsSGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["PartsSGSTTotal"].Value.ToString();
                TextBoxLabourSGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["LabourSGSTTotal"].Value.ToString();
                TextBoxPartsIGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["PartsIGSTTotal"].Value.ToString();
                TextBoxLabourIGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["LabourIGSTTotal"].Value.ToString();
                TextBoxTotalSGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["TotalSGST"].Value.ToString();
                TextBoxTotalCGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["TotalCGST"].Value.ToString();
                TextBoxTotalIGST.Text = lObjResult.DataGridResult.CurrentRow.Cells["TotalIGST"].Value.ToString();
                TextBoxTotalTax.Text = lObjResult.DataGridResult.CurrentRow.Cells["TotalTax"].Value.ToString();
                //TextBoxTotalAmount.Text = lObjResult.DataGridResult.CurrentRow.Cells["TotalAmount"].Value.ToString();
                TextBoxGrandTotal.Text = lObjResult.DataGridResult.CurrentRow.Cells["GrandTotal"].Value.ToString();
                //TextBoxDiscount.Text = lObjResult.DataGridResult.CurrentRow.Cells["DiscountAmount"].Value.ToString();
                //TextBoxInvoiceTotal.Text = lObjResult.DataGridResult.CurrentRow.Cells["InvoiceTotal"].Value.ToString();
                TextBoxRemarks.Text = lObjResult.DataGridResult.CurrentRow.Cells["InvoiceRemarks"].Value.ToString();
                TextBoxCreated.Text = lObjResult.DataGridResult.CurrentRow.Cells["Created"].Value.ToString();
                //TextBoxCreatedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedBy"].Value.ToString();
                TextBoxModified.Text = lObjResult.DataGridResult.CurrentRow.Cells["Modified"].Value.ToString();
                //TextBoxModifiedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedBy"].Value.ToString();

                
            }


        }
        public void InvoiceItemData(List<InvoiceItem> iObjInvoiceItemResultList)
        {

            //GridBill.Columns.Add("RowMode", "Record Mode");
            //GridBill.Columns.Add("SNoCol", "S. No.");
            if (GridBill.Columns.Count <= 0)
            {
                GridBill.Columns.Add("InvoiceItemSNo", "Invoice Item SNo");
                GridBill.Columns.Add("ItemNoCol", "Item No.");
                GridBill.Columns.Add("DescCol", "Description");
                GridBill.Columns.Add("TypeCol", "Type");
                GridBill.Columns.Add("PriceCol", "Price");
                GridBill.Columns.Add("UOMCol", "UOM");
                GridBill.Columns.Add("QtyCol", "Quantity");
                GridBill.Columns.Add("SGSTPercentCol", "SGST%");
                GridBill.Columns.Add("SGSTCol", "SGST");
                GridBill.Columns.Add("CGSTPercentCol", "CGST%");
                GridBill.Columns.Add("CGSTCol", "CGST");
                GridBill.Columns.Add("IGSTPercentCol", "IGST%");
                GridBill.Columns.Add("IGSTCol", "IGST");
                //GridBill.Columns.Add("GrossAmtCol", "Gross Amount");

                GridBill.Columns.Add("DiscountCol", "Discount");
                //GridBill.Columns.Add("NetAmtCol", "Net Amount");
                // GridBill.Columns.Add("TotalTaxCol", "Total Tax");
                GridBill.Columns.Add("TotalAmtCol", "Total Amount");
            }


            foreach (InvoiceItem lObj in iObjInvoiceItemResultList)
            {

                
                 GridBill.Rows.Add(
                     lObj.InvoiceItemSNo,
                     lObj.ItemNo,
                     lObj.ItemDesc,
                     lObj.ItemType,
                     lObj.ItemPrice,
                     lObj.ItemUOM,
                     lObj.Qty,
                     lObj.SGSTRate,
                     lObj.SGSTAmount,
                     lObj.CGSTRate,
                     lObj.CGSTAmount,
                     lObj.IGSTRate,
                     lObj.IGSTAmount,
                     lObj.DiscountAmount,
                     lObj.TotalAmount      

                );

            }


        }
        private void ButtonCustSearch_Click(object sender, EventArgs e)
        {
            Customer lObjCust = new Customer();
            
            string lsCustMob = TextBoxSearchMobileNo.Text;

           
            List<Customer> lObjCustResultList = lObjCust.ListData(lsCustMob);
            CustomerData(lObjCustResultList);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
            ButtonAdd.Text = "Add";

            Items lObjItem = new Items();
            string lsItemDesc = TextBoxItemDesc.Text;

            if (mMode == MasterMechUtil.OPMode.New)
            {
                List<Items> lObjItemResultList = lObjItem.ListData(lsItemDesc);
                ItemData(lObjItemResultList);
            }
            
        }

        private void TextBoxQty_TextChanged(object sender, EventArgs e)
        {
            
        }
        public void GridHeader()
        {
            GridBill.Columns.Add("RowMode", "Record Mode");
            GridBill.Columns.Add("SNoCol", "S. No.");
            GridBill.Columns.Add("ItemNoCol", "Item No.");
            GridBill.Columns.Add("DescCol", "Description");
            GridBill.Columns.Add("TypeCol", "Type");
            GridBill.Columns.Add("PriceCol", "Price");
            GridBill.Columns.Add("UOMCol", "UOM");
            GridBill.Columns.Add("QtyCol", "Quantity");
            GridBill.Columns.Add("SGSTPercentCol", "SGST%");
            GridBill.Columns.Add("SGSTCol", "SGST");
            GridBill.Columns.Add("CGSTPercentCol", "CGST%");
            GridBill.Columns.Add("CGSTCol", "CGST");
            GridBill.Columns.Add("IGSTPercentCol", "IGST%");
            GridBill.Columns.Add("IGSTCol", "IGST");
            GridBill.Columns.Add("GrossAmtCol", "Gross Amount");

            GridBill.Columns.Add("DiscountCol", "Discount");
            GridBill.Columns.Add("NetAmtCol", "Net Amount");
            GridBill.Columns.Add("TotalTaxCol", "Total Tax");
            GridBill.Columns.Add("TotalAmtCol", "Total Amount");

            GridBill.Columns.Add("HSNCol", "HSN");
            GridBill.Columns.Add("SACCol", "SAC");
            GridBill.Columns.Add("UPCCol", "UPC");
            GridBill.Columns.Add("ItemCatCol", "Category");
            GridBill.Columns.Add("ItemStsCol", "Status");
            GridBill.Columns.Add("ItemTypeCol", "Type");
            GridBill.Columns.Add("QtyInHandCol", "QtyInHand");
        }
        public void RowsAddIntoGrid(MasterMechUtil.OPMode iMode)
        {
            
            string lsItemNo = TextBoxItemNo.Text;
            string lsDesc = TextBoxItemDesc.Text;
            
            string lsPrice = TextBoxPrice.Text;
            string lsUOM = TextBoxUOM.Text;
            string lsQuantity = TextBoxQty.Text;
            string lsSGSTPercent = TextBoxSGST.Text;
            string lsSGSTValue = TextBoxSGSTValue.Text;
            string lsCGSTPercent = TextBoxCGST.Text;
            string lsCGSTValue = TextBoxCGSTValue.Text;
            string lsIGSTPercent = TextBoxIGST.Text;
            string lsIGSTValue = TextBoxIGSTValue.Text;
            string lsNetAmount = TextBoxNetAmount.Text;
            string lsTax = TextBoxTax.Text;
            string lsTotalAmount = TextBoxTotalAmount.Text;
            string lsGrossAmount = TextBoxGrossAmount.Text;
            string lsDiscount = TextBoxDiscount.Text;
           
            
            GridBill.Rows.Add
            (
                iMode,
                lnSno++, 
                lsItemNo, 
                lsDesc,
                lsItemType, // it is not initialised here, it is in the ItemData()
                lsPrice, 
                lsUOM, 
                lsQuantity, 
                lsSGSTPercent, 
                lsSGSTValue, 
                lsCGSTPercent, 
                lsCGSTValue,
                lsIGSTPercent,
                lsIGSTValue,
                lsGrossAmount,
                lsDiscount,
                lsNetAmount,
                lsTax,
                lsTotalAmount,
                lsHSN,
                lsSAC,
                lsUPC,
                lsCat,
                lsStatus,
                lsItemType,
                lnTotalQuantityInHand
                );

            GridBill.Columns["RowMode"].Visible = false;
            GridBill.Columns["HSNCol"].Visible = false;
            GridBill.Columns["SACCol"].Visible = false;
            GridBill.Columns["UPCCol"].Visible = false;
            GridBill.Columns["ItemCatCol"].Visible = false;
            GridBill.Columns["ItemTypeCol"].Visible = false;
            GridBill.Columns["ItemStsCol"].Visible = false;
            GridBill.Columns["QtyInHandCol"].Visible = false;

        }
        public void GridDataUpdate()
        {
            //This is And if else will be deleted after when invoice form is running successfully
            string lsRowMode = GridBill.Rows[lnSelectedRowIndex].Cells["RowMode"].Value.ToString();
            if (lsRowMode != "Delete")
            {
                GridBill.Rows[lnSelectedRowIndex].Cells["ItemNoCol"].Value = TextBoxItemNo.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["DescCol"].Value = TextBoxItemDesc.Text;

                GridBill.Rows[lnSelectedRowIndex].Cells["PriceCol"].Value = TextBoxPrice.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["UOMCol"].Value = TextBoxUOM.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["QtyCol"].Value = TextBoxQty.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["SGSTPercentCol"].Value = TextBoxSGST.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["SGSTCol"].Value = TextBoxSGSTValue.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["CGSTPercentCol"].Value = TextBoxCGST.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["CGSTCol"].Value = TextBoxCGSTValue.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["IGSTPercentCol"].Value = TextBoxIGST.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["IGSTCol"].Value = TextBoxIGSTValue.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["NetAmtCol"].Value = TextBoxNetAmount.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["TotalTaxCol"].Value = TextBoxTax.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["TotalAmtCol"].Value = TextBoxTotalAmount.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["GrossAmtCol"].Value = TextBoxGrossAmount.Text;
                GridBill.Rows[lnSelectedRowIndex].Cells["DiscountCol"].Value = TextBoxDiscount.Text;
            }
            else
            {
                MessageBox.Show("You can not Update deleted Data");
            }

        }
        public void AddUpdateDataIntoGrid()
        {
            if (mOperation == MasterMechUtil.OPMode.New)
            {
                // To Create Header only once
                //if (!lbAddButtonClicked)
                if (GridBill.Rows.Count == 0)
                {
                    GridHeader();
                    //lbAddButtonClicked = true; //To change Button Name From "Add" to "Update"
                }

                RowsAddIntoGrid(mOperation);

            }
            else if (mOperation == MasterMechUtil.OPMode.Open)
            {

                GridDataUpdate();

            }

            // Wheater It is "New" or "Update", Billing Tab will be Updated
            BiillingTabUpdate();

        }
        public void InvoiceData()
        {
            Invoice lObjInvoice = new Invoice();

            //For Customer Data Input

            if (!TextBoxCustNo.Text.Equals(""))
                lObjInvoice.mnCustNo = int.Parse(TextBoxCustNo.Text); // else mnCustNo is depend on NewCustUpdate()

            lObjInvoice.msCustFName = TextBoxCustFirstName.Text;
            lObjInvoice.msCustLName = TextBoxCustLastName.Text;
            lObjInvoice.msCustMobNo = TextBoxCustMobNo.Text;
            lObjInvoice.msCustEmail = TextBoxCustEmail.Text;

            if (!TextBoxLastVisited.Text.Equals(""))
                lObjInvoice.mdCustlLastVisit = DateTime.Parse(TextBoxLastVisited.Text);
            else
                lObjInvoice.mdCustlLastVisit = DateTime.Now;

            lObjInvoice.msCustStAddr = TextBoxStreet.Text;
            lObjInvoice.msCustArAddr = TextBoxArea.Text;
            lObjInvoice.msCustCity = TextBoxCity.Text;
            lObjInvoice.msCustState = TextBoxState.Text;
            lObjInvoice.msCustPinCode = TextBoxPincode.Text;
            lObjInvoice.msCustCountry = TextBoxCountry.Text;
            lObjInvoice.msCustType = ComboBoxType.Text;
            lObjInvoice.msCustSts = ComboBoxStatus.Text;
            lObjInvoice.msCustGSTNo = TextBoxGST.Text;
            lObjInvoice.msCustRemarks = TextBoxRemarks.Text;

            if (!TextBoxModified.Text.Equals(""))
                lObjInvoice.mdModified = DateTime.Parse(TextBoxModified.Text);
            else
                lObjInvoice.mdModified = DateTime.Now;

            
            lObjInvoice.msModifiedBy = (TextBoxModifiedBy.Equals(""))? "Prateek": TextBoxModifiedBy.Text;

            if (!TextBoxCreated.Text.Equals(""))
                lObjInvoice.mdCreated = DateTime.Parse(TextBoxCreated.Text);
            else
                lObjInvoice.mdCreated = DateTime.Now;


            lObjInvoice.msCreatedBy = (TextBoxCreatedBy.Equals("")) ? "Prateek" : TextBoxCreatedBy.Text;



            //For Vehicle Data Input

            lObjInvoice.msVehicleRegNo = TextBoxRegNo.Text;
            lObjInvoice.msVehicleModel = TextBoxModel.Text;
            lObjInvoice.msChassisNo = TextBoxChassisNo.Text;
            lObjInvoice.msEngineNo = TextBoxEngineNo.Text;
            lObjInvoice.mnMileage = int.Parse(TextBoxMilage.Text);
            lObjInvoice.msServiceType = ComboBoxServiceType.Text;
            lObjInvoice.msServiceAssoName = TextBoxServiceAssociate.Text;
            lObjInvoice.msServiceAssoMobNo = TextBoxAssociateMobNo.Text;

            //For Billing
            lObjInvoice.mnPartsTotal = float.Parse(TextBoxPartsTotal.Text);
            lObjInvoice.mnLabourTotal = float.Parse(TextBoxLabourTotal.Text);
            lObjInvoice.mnPartsCGSTTotal = float.Parse(TextBoxPartsCGST.Text);
            lObjInvoice.mnLabourCGSTTotal = float.Parse(TextBoxLabourCGST.Text);
            lObjInvoice.mnPartsSGSTTotal = float.Parse(TextBoxPartsSGST.Text);
            lObjInvoice.mnLabourSGSTTotal = float.Parse(TextBoxLabourSGST.Text);
            lObjInvoice.mnPartsIGSTTotal = float.Parse(TextBoxPartsIGST.Text);
            lObjInvoice.mnLabourIGSTTotal = float.Parse(TextBoxLabourIGST.Text);
            lObjInvoice.mnTotalSGST = float.Parse(TextBoxTotalSGST.Text);
            lObjInvoice.mnTotalCGST = float.Parse(TextBoxTotalCGST.Text);
            lObjInvoice.mnTotalIGST = float.Parse(TextBoxTotalIGST.Text);
            lObjInvoice.mnTotalTax = float.Parse(TextBoxTotalTax.Text);
            lObjInvoice.mnTotalAmount = float.Parse(TextBoxTotalAmount.Text);
            lObjInvoice.mnGrandTotal = float.Parse(TextBoxGrandTotal.Text);
            lObjInvoice.mnDiscountAmount = float.Parse(TextBoxDiscount.Text);            
            lObjInvoice.msInvoiceRemarks = TextBoxBillingRemarks.Text;

            //What is InvoiceTotal, 
            lObjInvoice.mnInvoiceTotal = lObjInvoice.mnGrandTotal;




            //For Item Data Input from grid

            List< DataGridViewRow > lObjReecords = new List< DataGridViewRow >();

            foreach (DataGridViewRow Records in GridBill.Rows)
            {
                lObjReecords.Add(Records);
            }

            string msConnStr = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
            SqlConnection lObjConn = new SqlConnection(msConnStr);
            lObjConn.Open();
            SqlTransaction transaction = lObjConn.BeginTransaction();
            
            // Call All Methods for saving into Database
            if(lObjInvoice.NewCustUpdate(lObjConn, transaction) && lObjInvoice.SaveInvoiceData(lObjConn, transaction) && lObjInvoice.SaveInvoiceItemData(lObjConn, transaction, lObjReecords))
            {
                MessageBox.Show("Invoice Saved Successfully");
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
            lObjConn.Close();

        }
        //public bool ItemInputNotEmpty()
        //{
        //    if(TextBoxItemNo.Text.Equals(""))
        //    {
        //        return false;
        //    }
        //    else if ()
        //}
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
           
            if (!TextBoxQty.Text.Equals(""))
            {
                AddUpdateDataIntoGrid();
            }
            else
            {
                MessageBox.Show("Please Fill The Quantity First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mMode == MasterMechUtil.OPMode.New)
            {


                int lastIndex = GridBill.Rows.Count - 1;
                //lsSelectedNetAmt = GridBill.Rows[lastIndex].Cells["NetAmtCol"].Value.ToString();
                lnTotalQuantityInHand = int.Parse(GridBill.CurrentRow.Cells["QtyInHandCol"].Value.ToString());

                GridBill.Rows[e.RowIndex].Selected = true; // to select whole row when one cell is selected

                mOperation = MasterMechUtil.OPMode.Open;
                ButtonDelete.Visible = true;
                ButtonCancel.Visible = true;
                ButtonAdd.Text = "Update";

                //Following Two lines are for selected row's index for updating row
                mOperation = MasterMechUtil.OPMode.Open;
                lnSelectedRowIndex = GridBill.SelectedRows[0].Index;

                TextBoxItemNo.Text = GridBill.CurrentRow.Cells["ItemNoCol"].Value.ToString();
                TextBoxItemDesc.Text = GridBill.CurrentRow.Cells["DescCol"].Value.ToString();
                TextBoxPrice.Text = GridBill.CurrentRow.Cells["PriceCol"].Value.ToString();
                TextBoxUOM.Text = GridBill.CurrentRow.Cells["UOMCol"].Value.ToString();

                TextBoxGrossAmount.Text = GridBill.CurrentRow.Cells["GrossAmtCol"].Value.ToString();
                TextBoxDiscount.Text = GridBill.CurrentRow.Cells["DiscountCol"].Value.ToString();
                TextBoxSGST.Text = GridBill.CurrentRow.Cells["SGSTPercentCol"].Value.ToString();
                TextBoxSGSTValue.Text = GridBill.CurrentRow.Cells["SGSTCol"].Value.ToString();
                TextBoxCGST.Text = GridBill.CurrentRow.Cells["CGSTPercentCol"].Value.ToString();
                TextBoxCGSTValue.Text = GridBill.CurrentRow.Cells["CGSTCol"].Value.ToString();
                TextBoxIGST.Text = GridBill.CurrentRow.Cells["IGSTPercentCol"].Value.ToString();
                TextBoxIGSTValue.Text = GridBill.CurrentRow.Cells["IGSTCol"].Value.ToString();
                TextBoxNetAmount.Text = GridBill.CurrentRow.Cells["NetAmtCol"].Value.ToString();
                TextBoxTax.Text = GridBill.CurrentRow.Cells["TotalTaxCol"].Value.ToString();
                TextBoxTotalAmount.Text = GridBill.CurrentRow.Cells["TotalAmtCol"].Value.ToString();


                //This below line is for TextBox Quantity and it must be at last in cellclick because 
                // if we write it before above line then textbox_textChanged event it called and some 
                // TextBox will be empty at that time so exception will be occured

                TextBoxQty.Text = GridBill.CurrentRow.Cells["QtyCol"].Value.ToString();
            }
            

           
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

       
        public void Calculation()
        {
            double lnDiscount = (TextBoxDiscount.Text == null) ? 0.0 : double.Parse(TextBoxDiscount.Text);

            double lnPrice = double.Parse(TextBoxPrice.Text);
            int lnQuantity = int.Parse(TextBoxQty.Text);

            double lnCGSTPercent = double.Parse(TextBoxCGST.Text);
            double lnSGSTPercent = double.Parse(TextBoxSGST.Text);
            double lnIGSTPercent = double.Parse(TextBoxIGST.Text);


            TextBoxCGSTValue.Text = ((lnQuantity * lnPrice * lnCGSTPercent) / 100).ToString();
            TextBoxSGSTValue.Text = ((lnQuantity * lnPrice * lnSGSTPercent) / 100).ToString();
            TextBoxIGSTValue.Text = ((lnQuantity * lnPrice * lnIGSTPercent) / 100).ToString();

            double lnCGSTValue = double.Parse(TextBoxCGSTValue.Text);
            double lnSGSTValue = double.Parse(TextBoxSGSTValue.Text);
            double lnIGSTValue = double.Parse(TextBoxIGSTValue.Text);

            TextBoxTax.Text = (lnCGSTValue + lnSGSTValue + lnIGSTValue).ToString();

            double lnTotalAmt = lnQuantity * lnPrice;
            TextBoxTotalAmount.Text = lnTotalAmt.ToString();

            double lnNetAmt = lnTotalAmt - lnDiscount;
            TextBoxNetAmount.Text = lnNetAmt.ToString();

            double lnTax = double.Parse(TextBoxTax.Text);
            double lnGrossAmt = lnNetAmt + lnTax;
            TextBoxGrossAmount.Text = lnGrossAmt.ToString();
        }
        
        public bool QuantityValid()
        {
            //To check Whether my stock is less than my demand or not
            int lnDemandQuantity = 0;
            foreach (DataGridViewRow Records in GridBill.Rows)
            {
                if(Records.Cells["ItemNoCol"].Value.ToString()== TextBoxItemNo.Text && Records.Cells["RowMode"].Value.ToString() != "Delete")
                {
                    lnDemandQuantity += int.Parse(Records.Cells["QtyCol"].Value.ToString());
                }
            }

            if (mOperation == MasterMechUtil.OPMode.New)
                lnDemandQuantity += int.Parse(TextBoxQty.Text);
            else if (mOperation == MasterMechUtil.OPMode.Open)
                lnDemandQuantity = int.Parse(TextBoxQty.Text);


            if (lnDemandQuantity > lnTotalQuantityInHand)
            {
                MessageBox.Show($"Only {lnTotalQuantityInHand} Item(s) Available");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void TextBoxQty_Validated(object sender, EventArgs e)
        {
            if (TextBoxQty.Text.Length > 0 && QuantityValid())
            {

                Calculation(); 

            }

        }

        private void TextBoxDiscount_Validated(object sender, EventArgs e)
        {
            Calculation();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            lnSelectedRowIndex = GridBill.SelectedRows[0].Index;
            DialogResult Result = MessageBox.Show("Do you want to delete?","Delete Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Result == DialogResult.Yes)
            {

                // To Assign a "Delete" Value Into Grid
                GridBill.Rows[lnSelectedRowIndex].Cells["RowMode"].Value = MasterMechUtil.OPMode.Delete;

                //To Disable The Row Which is deleted
                GridBill.Rows[lnSelectedRowIndex].Visible = false;


                // To Change Deleted Row's Fore Colour
                //GridBill.Rows[lnSelectedRowIndex].DefaultCellStyle.ForeColor = Color.Red;

                //To Remove Selection from That Particular Row
                //GridBill.Rows[lnSelectedRowIndex].Selected = false;

                BiillingTabUpdate();

            }   

        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            if(mMode == MasterMechUtil.OPMode.New)
            {
                LabelInvMob.Visible = false;
                TextBoxInvMob.Visible=false;
                ButtonInvoiceSearch.Visible=false;
                ButtonAdvInvoiceSearch.Visible = false;
                LabelInvoiceNo.Visible=false;
                TextBoxInvoiceNo.Visible=false;
                LabelInvoiceDate.Visible=false;
                TextBoxInvoiceDate.Visible=false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(mMode == MasterMechUtil.OPMode.New)
                InvoiceData();
            else
            {
                
                string lsInvoiceSNo = TextBoxInvoiceNo.Text;
                if (!lsInvoiceSNo.Equals(""))
                {
                    Invoice lObjInv = new Invoice();
                    lObjInv.CancelInvoice(lsInvoiceSNo);
                }
                else
                {
                    MessageBox.Show("Invoice No field is Empty");
                }
            }
        }

        private void ButtonInvoiceSearch_Click(object sender, EventArgs e)
        {
            
            Invoice lObjInvoice = new Invoice();

            string lsInvoiceMobNo = TextBoxInvMob.Text;

            List<Invoice> lObjInvoiceResultList = lObjInvoice.ListInvoiceData(lsInvoiceMobNo);
            InvoiceData(lObjInvoiceResultList);
            List<InvoiceItem> lObjInvoiceItemResultList = lObjInvoice.ListInvoiceItemData();
            InvoiceItemData(lObjInvoiceItemResultList);         
           

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(mMode==MasterMechUtil.OPMode.Open)
            {
                List<InvoiceItem> ListGridBill = new List<InvoiceItem>();
                Invoice lObjInvoice = new Invoice();

                lObjInvoice.msCustFName = TextBoxCustFirstName.Text;
                lObjInvoice.msCustLName = TextBoxCustLastName.Text;
                lObjInvoice.msInvoiceNo = TextBoxInvoiceNo.Text;
                lObjInvoice.mdInvoiceDate = DateTime.Parse(TextBoxInvoiceDate.Text);
                lObjInvoice.mnTotalTax = double.Parse(TextBoxTotalTax.Text);
                lObjInvoice.mnDiscountAmount = double.Parse(TextBoxTotalDiscount.Text);
                lObjInvoice.mnGrandTotal = double.Parse(TextBoxGrandTotal.Text);
                lObjInvoice.mnInvoiceTotal = double.Parse(TextBoxGrandTotal.Text);

                foreach (DataGridViewRow Row in GridBill.Rows)
                {
                    
                    InvoiceItem lObjInvoiceItem = new InvoiceItem();
                    lObjInvoiceItem.InvoiceSNo = (int?)Row.Cells["InvoiceItemSNo"].Value;
                    lObjInvoiceItem.ItemNo = (int?)Row.Cells["ItemNoCol"].Value;
                    lObjInvoiceItem.ItemDesc = Row.Cells["DescCol"].Value.ToString();
                    lObjInvoiceItem.ItemUOM = Row.Cells["UOMCol"].ToString();
                    lObjInvoiceItem.ItemPrice = (double)Row.Cells["PriceCol"].Value;
                    lObjInvoiceItem.Qty = (double?)Row.Cells["QtyCol"].Value;
                    lObjInvoiceItem.DiscountAmount = (double)Row.Cells["DiscountCol"].Value;
                    lObjInvoiceItem.CGSTAmount = (double)Row.Cells["CGSTCol"].Value;
                    lObjInvoiceItem.SGSTAmount = (double)Row.Cells["SGSTCol"].Value;
                    lObjInvoiceItem.IGSTAmount = (double)Row.Cells["IGSTCol"].Value;
                    lObjInvoiceItem.TotalAmount = (double)Row.Cells["TotalAmtCol"].Value;
                    lObjInvoiceItem.ItemUOM = Row.Cells["UOMCOl"].Value.ToString();
                    //lObjInvoiceItem.NetAmt = (double)Row.Cells["NetAmtCol"].Value;
                    ListGridBill.Add(lObjInvoiceItem);
                    
                    
                }
                lObjInvoice.PrintInvoice(ListGridBill);

                PrintInvoiceForm lObj = new PrintInvoiceForm();
                lObj.ShowDialog();
            }
        }
    }
}
