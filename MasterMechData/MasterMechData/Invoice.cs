using MasterMechPrj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MasterMechData
{
    public class Invoice
    {
        public string msInvoiceNo;
        public int? mnInvoiceSNo;
        public DateTime? mdInvoiceDate;
        public string msInvoiceSts;
        public Customer InvoiceCustomer;
        public int? mnCustNo;
        public string msCustFName;
        public string msCustLName;
        public string msCustMobNo;
        public string msCustEmail;
        public string msCustSts;
        public string msCustType;
        public string msCustStAddr;
        public string msCustArAddr;
        public string msCustCity;
        public string msCustState;
        public string msCustPinCode;
        public string msCustCountry;
        public string msCustGSTNo;
        public DateTime? mdCustlLastVisit;
        public string msCustRemarks;
        public string msVehicleRegNo;
        public string msVehicleModel;
        public string msChassisNo;
        public string msEngineNo;
        public int? mnMileage;
        public string msServiceType;
        public string msServiceAssoName;
        public string msServiceAssoMobNo;
        public double? mnPartsTotal;
        public double? mnLabourTotal;
        public double? mnPartsCGSTTotal;
        public double? mnLabourCGSTTotal;
        public double? mnPartsSGSTTotal;
        public double? mnLabourSGSTTotal;
        public double? mnPartsIGSTTotal;
        public double? mnLabourIGSTTotal;
        public double? mnTotalSGST;
        public double? mnTotalCGST;
        public double? mnTotalIGST;
        public double? mnTotalTax;
        public double? mnTotalAmount;
        public double? mnGrandTotal;
        public double? mnDiscountAmount;
        public double? mnInvoiceTotal;
        public string msInvoiceRemarks;
        public DateTime? mdCreated;
        public string msCreatedBy;
        public DateTime? mdModified;
        public string msModifiedBy;
        public char? mcDeleted;
        public DateTime? mdDeletedOn;
        public string msDeletedBy;

        public string msConnStr;
        
        public static int? mnSearchInvoiceNo;


        public Invoice()
        {
            msConnStr = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";

        }
        
        public void PrintInvoice(List<InvoiceItem> iListGridBillData)
        {
            FileStream lObjFS = new FileStream("D:\\Invoice.html", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(lObjFS);

            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head>");
            writer.WriteLine("    <title>Invoice</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<style>");
            writer.WriteLine("    tr");
            writer.WriteLine("    {");
            writer.WriteLine("        border: 0px;");
            writer.WriteLine("    }");
            writer.WriteLine("</style>");
            writer.WriteLine("<body>");
            writer.WriteLine("    <table border=\"1\" width=\"100%\" style=\"color:#005a00\">");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"12\">");
            writer.WriteLine("                <center>");
            writer.WriteLine($"                <p>{MasterMechUtil.msCompName}</p>");
            writer.WriteLine($"                <p>{MasterMechUtil.msArea}</p>");
            writer.WriteLine($"                <p>{MasterMechUtil.msCity}</p>");
            writer.WriteLine("            </center>");
            writer.WriteLine("            </td>");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"5\"></td>");
            writer.WriteLine("            <td colspan=\"7\">");
            writer.WriteLine("                <p>To,</p>");
            writer.WriteLine($"                <p>{msCustFName} {msCustLName}</p>");
            writer.WriteLine("                <p>Customer</p>");
            writer.WriteLine("            </td>");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"3\">Invoice No.:-</td>");
            writer.WriteLine($"            <td colspan=\"3\">{msInvoiceNo}</td>");
            writer.WriteLine("            <td colspan=\"3\">Invoice Date</td>");
            writer.WriteLine($"            <td colspan=\"3\">{mdInvoiceDate}</td>");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"12\">");
            writer.WriteLine($"                <p>GST No. :- {MasterMechUtil.msGSTNo}</p>");
            writer.WriteLine("            </td>");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <th>SI.No.</th>");
            writer.WriteLine("            <th>Item</th>");
            writer.WriteLine("            <th>Description</th>");
            writer.WriteLine("            <th>UOM</th>");
            writer.WriteLine("            <th>Unit Price</th>");
            writer.WriteLine("            <th>Quantity</th>");
            writer.WriteLine("            <th>Discount</th>");
            writer.WriteLine("            <th>CGST</th>");
            writer.WriteLine("            <th>SGST</th>");
            writer.WriteLine("            <th>IGST</th>");
            writer.WriteLine("            <th>Total Tax</th>");
            writer.WriteLine("            <th>Total Amount</th>");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <!--This tr will be in loop according to number of item purchasaed-->");


            foreach(InvoiceItem RecordList in iListGridBillData)
            {
                writer.WriteLine("        <tr>");
                writer.WriteLine($"            <td>{RecordList.InvoiceSNo}</td>");
                writer.WriteLine($"            <td>{RecordList.ItemNo}</td>");
                writer.WriteLine($"            <td>{RecordList.ItemDesc}</td>");
                writer.WriteLine($"            <td>{RecordList.ItemUOM}</td>");
                writer.WriteLine($"            <td>{RecordList.ItemPrice}</td>");
                writer.WriteLine($"            <td>{RecordList.Qty}</td>");
                writer.WriteLine($"            <td>{RecordList.DiscountAmount}</td>");
                writer.WriteLine($"            <td>{RecordList.CGSTAmount}</td>");
                writer.WriteLine($"            <td>{RecordList.SGSTAmount}</td>");
                writer.WriteLine($"            <td>{RecordList.IGSTAmount}</td>");
                //writer.WriteLine($"            <td>{RecordList.Tax}</td>");
                //writer.WriteLine($"            <td>{RecordList.TotalAmount}</td>");
                writer.WriteLine("        </tr>");

            }

            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"9\" align=\"right\">Total Net Amount</td>");
            writer.WriteLine($"            <td colspan=\"3\" align=\"right\">Dynaic</td>");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"9\" align=\"right\">Total Discount</td>");
            writer.WriteLine($"            <td colspan=\"3\" align=\"right\">{mnDiscountAmount}</td>");
            writer.WriteLine("       ");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"9\" align=\"right\">Total Gross</td>");
            writer.WriteLine($"            <td colspan=\"3\" align=\"right\">{mnGrandTotal}</td>");
            writer.WriteLine("       ");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"9\" align=\"right\">Invoice Total</td>");
            writer.WriteLine($"            <td colspan=\"3\" align=\"right\">{mnGrandTotal}</td>");
            writer.WriteLine("       ");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"9\">Thanks For Your Bussiness</td>");
            writer.WriteLine("            <td colspan=\"2\">Payable Amount:-</td>");
            writer.WriteLine($"            <td align=\"right\">{mnGrandTotal}</td>");
            writer.WriteLine("        </tr>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td colspan=\"12\">");
            writer.WriteLine("                <p>Term & Condition</p>");
            writer.WriteLine("                <p>Full Payment is due upon this invoice. Late payment may incure additional charges or interest as per the application laws.</p>");
            writer.WriteLine("            </td>");
            writer.WriteLine("        </tr>");
            writer.WriteLine("    </table>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");



            writer.Flush();
            writer.Close();
            lObjFS.Close();

            //Process.Start("msedge.exe", "D:\\Invoice.html");
            
        }
        public void CancelInvoice(string iInvoiceNo)
        {
            string lsQueryText = $"Update [Invoice{MasterMechUtil.sFY}] Set ";
             lsQueryText += $"InvoiceSts = 'Cancel' Where InnvoiceNo = '{iInvoiceNo}'";
            
            SqlConnection lObjCon = new SqlConnection(msConnStr);
            lObjCon.Open();

            SqlCommand cmd = new SqlCommand(lsQueryText, lObjCon);
            SqlDataReader reader = cmd.ExecuteReader();
            
            lObjCon.Close();
           
        }
        public List<Invoice> ListInvoiceData(string isMobileNo)
        {
            List<Invoice> ListRecordsMatched = new List<Invoice>();

            string query = $"SELECT * FROM [Invoice{MasterMechUtil.sFY}] WHERE CustMobNo Like '{isMobileNo}%' and Deleted = 'N'";

            using (SqlConnection connection = new SqlConnection(msConnStr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Invoice lObjInvoice = new Invoice();

                            lObjInvoice.msInvoiceNo = (string)reader["InnvoiceNo"];
                            lObjInvoice.mnInvoiceSNo = (int)reader["InvoiceSNo"];
                            lObjInvoice.mdInvoiceDate = (DateTime)reader["InvoiceDate"];
                            lObjInvoice.msInvoiceSts = (string)reader["InvoiceSts"];
                            lObjInvoice.mnCustNo = (int)reader["CustNo"];
                            lObjInvoice.msCustFName = (string)reader["CustFName"];
                            lObjInvoice.msCustLName = (string)reader["CustLName"];
                            lObjInvoice.msCustMobNo = (string)reader["CustMobNo"];
                            lObjInvoice.msCustEmail = (string)reader["CustEmail"];
                            lObjInvoice.msCustSts = (string)reader["CustSts"];
                            lObjInvoice.msCustType = reader["CustType"] == DBNull.Value ? null : (string)reader["CustType"];
                            lObjInvoice.msCustStAddr = reader["CustStAddr"] == DBNull.Value ? null : (string)reader["CustStAddr"];
                            lObjInvoice.msCustArAddr = reader["CustArAddr"] == DBNull.Value ? null : (string)reader["CustArAddr"];
                            lObjInvoice.msCustCity = reader["CustCity"] == DBNull.Value ? null : (string)reader["CustCity"];
                            lObjInvoice.msCustState = reader["CustState"] == DBNull.Value ? null : (string)reader["CustState"];
                            lObjInvoice.msCustPinCode = (string)reader["CustPinCode"];
                            lObjInvoice.msCustCountry = reader["CustCountry"] == DBNull.Value ? null : (string)reader["CustCountry"];
                            lObjInvoice.msCustGSTNo = reader["CustGSTNo"] == DBNull.Value ? null : (string)reader["CustGSTNo"];
                            lObjInvoice.mdCustlLastVisit = reader["CustlLastVisit"] == DBNull.Value ? null : (DateTime?)reader["CustlLastVisit"];
                            lObjInvoice.msCustRemarks = reader["CustRemarks"] == DBNull.Value ? null : (string)reader["CustRemarks"];
                            lObjInvoice.msVehicleRegNo = reader["VehicleRegNo"] == DBNull.Value ? null : (string)reader["VehicleRegNo"];
                            lObjInvoice.msVehicleModel = reader["VehicleModel"] == DBNull.Value ? null : (string)reader["VehicleModel"];
                            lObjInvoice.msChassisNo = reader["ChassisNo"] == DBNull.Value ? null : (string)reader["ChassisNo"];
                            lObjInvoice.msEngineNo = reader["EngineNo"] == DBNull.Value ? null : (string)reader["EngineNo"];
                            lObjInvoice.mnMileage = reader["Mileage"] == DBNull.Value ? null : (int?)reader["Mileage"];
                            lObjInvoice.msServiceType = reader["ServiceType"] == DBNull.Value ? null : (string)reader["ServiceType"];
                            lObjInvoice.msServiceAssoName = reader["ServiceAssoName"] == DBNull.Value ? null : (string)reader["ServiceAssoName"];
                            lObjInvoice.msServiceAssoMobNo = reader["ServiceAssoMobNo"] == DBNull.Value ? null : (string)reader["ServiceAssoMobNo"];
                            lObjInvoice.mnPartsTotal = reader["PartsTotal"] == DBNull.Value ? null : (double?)reader["PartsTotal"];
                            lObjInvoice.mnLabourTotal = reader["LabourTotal"] == DBNull.Value ? null : (double?)reader["LabourTotal"];
                            lObjInvoice.mnPartsCGSTTotal = reader["PartsCGSTTotal"] == DBNull.Value ? null : (double?)reader["PartsCGSTTotal"];
                            lObjInvoice.mnLabourCGSTTotal = reader["LabourCGSTTotal"] == DBNull.Value ? null : (double?)reader["LabourCGSTTotal"];
                            lObjInvoice.mnPartsSGSTTotal = reader["PartsSGSTTotal"] == DBNull.Value ? null : (double?)reader["PartsSGSTTotal"];
                            lObjInvoice.mnLabourSGSTTotal = reader["LabourSGSTTotal"] == DBNull.Value ? null : (double?)reader["LabourSGSTTotal"];
                            lObjInvoice.mnPartsIGSTTotal = reader["PartsIGSTTotal"] == DBNull.Value ? null : (double?)reader["PartsIGSTTotal"];
                            lObjInvoice.mnLabourIGSTTotal = reader["LabourIGSTTotal"] == DBNull.Value ? null : (double?)reader["LabourIGSTTotal"];
                            lObjInvoice.mnTotalSGST = reader["TotalSGST"] == DBNull.Value ? null : (double?)reader["TotalSGST"];
                            lObjInvoice.mnTotalCGST = reader["TotalCGST"] == DBNull.Value ? null : (double?)reader["TotalCGST"];
                            lObjInvoice.mnTotalIGST = reader["TotalIGST"] == DBNull.Value ? null : (double?)reader["TotalIGST"];
                            lObjInvoice.mnTotalTax = reader["TotalTax"] == DBNull.Value ? null : (double?)reader["TotalTax"];
                            lObjInvoice.mnTotalAmount = reader["TotalAmount"] == DBNull.Value ? null : (double?)reader["TotalAmount"];
                            lObjInvoice.mnGrandTotal = reader["GrandTotal"] == DBNull.Value ? null : (double?)reader["GrandTotal"];
                            lObjInvoice.mnDiscountAmount = reader["DiscountAmount"] == DBNull.Value ? null : (double?)reader["DiscountAmount"];
                            lObjInvoice.mnInvoiceTotal = reader["InvoiceTotal"] == DBNull.Value ? null : (double?)reader["InvoiceTotal"];
                            lObjInvoice.msInvoiceRemarks = reader["InvoiceRemarks"] == DBNull.Value ? null : (string)reader["InvoiceRemarks"];
                            lObjInvoice.mdCreated = reader["Created"] == DBNull.Value ? null : (DateTime?)reader["Created"];
                            lObjInvoice.msCreatedBy = reader["CreatedBy"] == DBNull.Value ? null : (string)reader["CreatedBy"];
                            lObjInvoice.mdModified = reader["Modified"] == DBNull.Value ? null : (DateTime?)reader["Modified"];
                            lObjInvoice.msModifiedBy = reader["ModifiedBy"] == DBNull.Value ? null : (string)reader["ModifiedBy"];

                            string lsDel = reader["Deleted"].ToString();

                            lObjInvoice.mcDeleted =lsDel[0]; // Char Reading from table as string and convert it into char

                            lObjInvoice.mdDeletedOn = reader["DeletedOn"] == DBNull.Value ? null : (DateTime?)reader["DeletedOn"];
                            lObjInvoice.msDeletedBy = reader["DeletedBy"] == DBNull.Value ? null : (string)reader["DeletedBy"];



                            ListRecordsMatched.Add(lObjInvoice);

                            
                        }
                    }
                }
            }
            return ListRecordsMatched;


        }
        public List<InvoiceItem> ListInvoiceItemData()
        {
            List<InvoiceItem> ListRecordsMatched = new List<InvoiceItem>();

            string query = $"SELECT * FROM [InvoiceItem{MasterMechUtil.sFY}] WHERE InvoiceSNo = '{mnSearchInvoiceNo}' and Deleted = 'N'";

            using (SqlConnection connection = new SqlConnection(msConnStr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                           
                                InvoiceItem lObjItem = new InvoiceItem();

                                lObjItem.InvoiceItemSNo = (int)reader["InvoiceItemSNo"];
                                lObjItem.InvoiceItemSNo = (int)reader["InvoiceSNo"];
                                lObjItem.ItemNo = (int)reader["ItemNo"];
                                lObjItem.ItemDesc = reader["ItemDesc"].ToString();
                                lObjItem.ItemType = reader["ItemType"].ToString();
                                lObjItem.ItemCatg = reader["ItemCatg"].ToString();
                                lObjItem.ItemPrice = (double)reader["ItemPrice"];
                                lObjItem.ItemUOM = reader["ItemUOM"].ToString();
                                lObjItem.ItemSts = reader["ItemSts"].ToString();
                                lObjItem.CGSTRate = (double)reader["CGSTRate"];
                                lObjItem.SGSTRate = (double)reader["SGSTRate"];
                                lObjItem.IGSTRate = (double)reader["IGSTRate"];
                                lObjItem.UPCCode = reader["UPCCode"].ToString();
                                lObjItem.HSNCode = reader["HSNCode"].ToString();
                                lObjItem.SACCode = reader["SACCode"].ToString();
                                lObjItem.Qty = (double)reader["Qty"];
                                lObjItem.SGSTAmount = (double)reader["SGSTAmount"];
                                lObjItem.CGSTAmount = (double)reader["CGSTAmount"];
                                lObjItem.IGSTAmount = (double)reader["IGSTAmount"];
                                lObjItem.DiscountAmount = (double)reader["DiscountAmount"];
                                lObjItem.TotalAmount = (double)reader["TotalAmount"];
                                lObjItem.Created = (DateTime)reader["Created"];
                                lObjItem.CreatedBy = reader["CreatedBy"].ToString();
                                lObjItem.Modified = (DateTime)reader["Modified"];
                                lObjItem.ModifiedBy = reader["ModifiedBy"].ToString();

                                
                                // Now you can use these variables as per your application logic
                            


                            ListRecordsMatched.Add(lObjItem);

                        }
                    }
                }
            }
            return ListRecordsMatched;

        }
        public bool NewCustUpdate(SqlConnection lObjCon, SqlTransaction transaction)
        {
            try
            {
                //SqlConnection lObjCon = new SqlConnection(msConnStr);
                if (mnCustNo == null)
                {
                    string lsQueryText = "INSERT INTO Customer Output INSERTED.CustNo Values(";
                    lsQueryText += " @CustFName, @CustLName, @CustMobNo, @CustEmail, @CustSts, @CustType, @CustStAddr, @CustArAddr, @CustCity, @CustState, @CustPinCode, @CustCountry, @CustGSTNo, @CustLastVisit, @CustRemarks, @Created, @CreatedBy, @Modified, @ModifiedBy, 'N', NULL, 'N' )";


                    SqlCommand cmd = new SqlCommand(lsQueryText, lObjCon);
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.Text;


                    cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = msCustFName;
                    cmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = msCustLName;
                    cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = msCustMobNo;
                    cmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = msCustEmail;
                    cmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = msCustSts;
                    cmd.Parameters.AddWithValue("@CustType", SqlDbType.VarChar).Value = msCustType;
                    cmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.Time).Value = msCustStAddr;
                    cmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.Time).Value = msCustArAddr;
                    cmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = msCustCity;
                    cmd.Parameters.AddWithValue("@CustState", SqlDbType.Time).Value = msCustState;
                    cmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.Time).Value = msCustPinCode;
                    cmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = msCustCountry;
                    cmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = msCustGSTNo;
                    cmd.Parameters.AddWithValue("@CustLastVisit", SqlDbType.VarChar).Value = mdCustlLastVisit;
                    cmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = msCustRemarks;
                    cmd.Parameters.AddWithValue("@Created", SqlDbType.VarChar).Value = mdCreated;
                    cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = msCreatedBy;
                    cmd.Parameters.AddWithValue("@Modified", SqlDbType.VarChar).Value = mdModified;
                    cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = msModifiedBy;

                    //lObjCon.Open();
                    mnCustNo = (int)cmd.ExecuteScalar();
                   // lObjCon.Close();

                    // note:- we don't require lobjCon.Open() or Close() beacuse
                    //this lObjzCon is parameter and hence it is open at that place from where it has passed


                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        
        public bool SaveInvoiceItemData(SqlConnection lObjCon, SqlTransaction transaction, List<DataGridViewRow> iObjReecords)
        {
            try
            {

                foreach (DataGridViewRow Records in iObjReecords)
                {
                    
                    string lsQueryText = $"INSERT INTO [InvoiceItem{MasterMechUtil.sFY}]" +
                        $"(InvoiceSNo, ItemNo, ItemDesc, ItemType, ItemCatg, ItemPrice," +
                        $"ItemUOM, ItemSts, CGSTRate, SGSTRate, IGSTRate, UPCCode, " +
                        $"HSNCode, SACCode, Qty, SGSTAmount, CGSTAmount, IGSTAmount," +
                        $"DiscountAmount, TotalAmount, Created, CreatedBy, Modified," +
                        $"ModifiedBy, Deleted, DeletedOn, DeletedBy)" +
                        $"VALUES" +
                        $"(@InvoiceSNo, @ItemNo, @ItemDesc, @ItemType, @ItemCatg, @ItemPrice, " +
                        $"@ItemUOM, @ItemSts, @CGSTRate, @SGSTRate, @IGSTRate, @UPCCode," +
                        $"@HSNCode, @SACCode, @Qty, @SGSTAmount, @CGSTAmount, @IGSTAmount, " +
                        $"@DiscountAmount, @TotalAmount, @Created, @CreatedBy, @Modified, " +
                        $"@ModifiedBy, 'N', NULL, 'N')";



                    SqlCommand cmd = new SqlCommand(lsQueryText, lObjCon);

                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.Text;


                    cmd.Parameters.Add("@InvoiceSNo", SqlDbType.Int).Value = mnInvoiceSNo;
                    cmd.Parameters.Add("@ItemNo", SqlDbType.Int).Value = Records.Cells["ItemNoCol"].Value.ToString();
                    cmd.Parameters.Add("@ItemDesc", SqlDbType.VarChar).Value = Records.Cells["DescCol"].Value.ToString();
                    cmd.Parameters.Add("@ItemType", SqlDbType.VarChar).Value = Records.Cells["TypeCol"].Value.ToString();
                    cmd.Parameters.Add("@ItemCatg", SqlDbType.VarChar).Value = Records.Cells["ItemCatCol"].Value.ToString();
                    cmd.Parameters.Add("@ItemPrice", SqlDbType.Float).Value = Records.Cells["PriceCol"].Value.ToString();
                    cmd.Parameters.Add("@ItemUOM", SqlDbType.VarChar).Value = Records.Cells["UOMCol"].Value.ToString();
                    cmd.Parameters.Add("@ItemSts", SqlDbType.VarChar).Value = Records.Cells["ItemStsCol"].Value.ToString();
                    cmd.Parameters.Add("@CGSTRate", SqlDbType.Float).Value = Records.Cells["CGSTPercentCol"].Value.ToString();
                    cmd.Parameters.Add("@SGSTRate", SqlDbType.Float).Value = Records.Cells["SGSTPercentCol"].Value.ToString();
                    cmd.Parameters.Add("@IGSTRate", SqlDbType.Float).Value = Records.Cells["IGSTPercentCol"].Value.ToString();
                    cmd.Parameters.Add("@UPCCode", SqlDbType.VarChar).Value = Records.Cells["UPCCol"].Value.ToString();
                    cmd.Parameters.Add("@HSNCode", SqlDbType.VarChar).Value = Records.Cells["HSNCol"].Value.ToString();
                    cmd.Parameters.Add("@SACCode", SqlDbType.VarChar).Value = Records.Cells["SACCol"].Value.ToString();
                    cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Records.Cells["QtyCol"].Value.ToString();
                    cmd.Parameters.Add("@SGSTAmount", SqlDbType.Float).Value = Records.Cells["SGSTCol"].Value.ToString();
                    cmd.Parameters.Add("@CGSTAmount", SqlDbType.Float).Value = Records.Cells["CGSTCol"].Value.ToString();
                    cmd.Parameters.Add("@IGSTAmount", SqlDbType.Float).Value = Records.Cells["IGSTCol"].Value.ToString();
                    cmd.Parameters.Add("@DiscountAmount", SqlDbType.Float).Value = Records.Cells["DisCountCol"].Value.ToString();
                    cmd.Parameters.Add("@TotalAmount", SqlDbType.Float).Value = Records.Cells["TotalAmtCol"].Value.ToString();

                    //If you want to put this value from invoice form then first you have to add all these fields into GridBill
                    // then hide it from display and then access them into here as above
                    // till that time, we set it manually
                    cmd.Parameters.Add("@Created", SqlDbType.SmallDateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = "Prateek";
                    cmd.Parameters.Add("@Modified", SqlDbType.SmallDateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar).Value = "Prateek";

                    //lObjCon.Open();
                    int lnRowAffected = cmd.ExecuteNonQuery();
                    //lObjCon.Close();
                    
                    // note:- we don't require lobjCon.Open() or Close() beacuse
                    //this lObjzCon is parameter and hence it is open at that place from where it has passed


                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public bool SaveInvoiceData(SqlConnection lObjCon, SqlTransaction transaction)
        {
            try
            {
             

                string lsQueryText = $"INSERT INTO [Invoice{MasterMechUtil.sFY}] " +
                    $"(InvoiceDate, InvoiceSts, CustNo, CustFName, CustLName, CustMobNo," +
                    $"CustEmail, CustSts, CustType, CustStAddr, CustArAddr, CustCity," +
                    $"CustState, CustPinCode, CustCountry, CustGSTNo, CustlLastVisit, " +
                    $"CustRemarks, VehicleRegNo, VehicleModel, ChassisNo, EngineNo, Mileage," +
                    $"ServiceType, ServiceAssoName, ServiceAssoMobNo, PartsTotal, LabourTotal," +
                    $"PartsCGSTTotal, LabourCGSTTotal, PartsSGSTTotal, LabourSGSTTotal, " +
                    $"PartsIGSTTotal, LabourIGSTTotal, TotalSGST, TotalCGST, TotalIGST, " +
                    $"TotalTax, TotalAmount, GrandTotal, DiscountAmount, InvoiceTotal, " +
                    $"InvoiceRemarks, Created, CreatedBy, Modified, ModifiedBy, Deleted," +
                    $"DeletedOn, DeletedBy) Output INSERTED.InvoiceSNo " +
                    $"VALUES " +
                    $"(@InvoiceDate, @InvoiceSts, @CustNo, @CustFName, @CustLName, @CustMobNo," +
                    $"@CustEmail, @CustSts, @CustType, @CustStAddr, @CustArAddr, @CustCity, " +
                    $"@CustState, @CustPinCode, @CustCountry, @CustGSTNo, @CustlLastVisit, " +
                    $"@CustRemarks, @VehicleRegNo, @VehicleModel, @ChassisNo, @EngineNo, @Mileage, " +
                    $" @ServiceType, @ServiceAssoName, @ServiceAssoMobNo, @PartsTotal, @LabourTotal, " +
                    $"@PartsCGSTTotal, @LabourCGSTTotal, @PartsSGSTTotal, @LabourSGSTTotal, " +
                    $"@PartsIGSTTotal, @LabourIGSTTotal, @TotalSGST, @TotalCGST, @TotalIGST, " +
                    $" @TotalTax, @TotalAmount, @GrandTotal, @DiscountAmount, @InvoiceTotal, " +
                    $"@InvoiceRemarks, @Created, @CreatedBy, @Modified, @ModifiedBy, 'N',NULL, 'N')";


                SqlCommand cmd = new SqlCommand(lsQueryText, lObjCon);
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.AddWithValue("@InvoiceDate", SqlDbType.SmallDateTime).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@InvoiceSts", SqlDbType.VarChar).Value = "Save";
                cmd.Parameters.AddWithValue("@CustNo", SqlDbType.Int).Value = mnCustNo;
                cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = msCustFName;
                cmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = msCustLName;
                cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = msCustMobNo;
                cmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = msCustEmail;
                cmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = msCustSts;
                cmd.Parameters.AddWithValue("@CustType", SqlDbType.Char).Value = msCustType;
                cmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.VarChar).Value = msCustStAddr;
                cmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.VarChar).Value = msCustArAddr;
                cmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = msCustCity;
                cmd.Parameters.AddWithValue("@CustState", SqlDbType.VarChar).Value = msCustState;
                cmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.Char).Value = msCustPinCode;
                cmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = msCustCountry;
                cmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = msCustGSTNo;
                cmd.Parameters.AddWithValue("@CustlLastVisit", SqlDbType.SmallDateTime).Value = mdCustlLastVisit;
                cmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = msCustRemarks;
                cmd.Parameters.AddWithValue("@VehicleRegNo", SqlDbType.VarChar).Value = msVehicleRegNo;
                cmd.Parameters.AddWithValue("@VehicleModel", SqlDbType.VarChar).Value = msVehicleModel;
                cmd.Parameters.AddWithValue("@ChassisNo", SqlDbType.VarChar).Value = msChassisNo;
                cmd.Parameters.AddWithValue("@EngineNo", SqlDbType.VarChar).Value = msEngineNo;
                cmd.Parameters.AddWithValue("@Mileage", SqlDbType.Int).Value = mnMileage;
                cmd.Parameters.AddWithValue("@ServiceType", SqlDbType.VarChar).Value = msServiceType;
                cmd.Parameters.AddWithValue("@ServiceAssoName", SqlDbType.VarChar).Value = msServiceAssoName;
                cmd.Parameters.AddWithValue("@ServiceAssoMobNo", SqlDbType.VarChar).Value = msServiceAssoMobNo;
                cmd.Parameters.AddWithValue("@PartsTotal", SqlDbType.Float).Value = mnPartsTotal;
                cmd.Parameters.AddWithValue("@LabourTotal", SqlDbType.Float).Value = mnLabourTotal;
                cmd.Parameters.AddWithValue("@PartsCGSTTotal", SqlDbType.Float).Value = mnPartsCGSTTotal;
                cmd.Parameters.AddWithValue("@LabourCGSTTotal", SqlDbType.Float).Value = mnLabourCGSTTotal;
                cmd.Parameters.AddWithValue("@PartsSGSTTotal", SqlDbType.Float).Value = mnPartsSGSTTotal;
                cmd.Parameters.AddWithValue("@LabourSGSTTotal", SqlDbType.Float).Value = mnLabourSGSTTotal;
                cmd.Parameters.AddWithValue("@PartsIGSTTotal", SqlDbType.Float).Value = mnPartsIGSTTotal;
                cmd.Parameters.AddWithValue("@LabourIGSTTotal", SqlDbType.Float).Value = mnLabourIGSTTotal;
                cmd.Parameters.AddWithValue("@TotalSGST", SqlDbType.Float).Value = mnTotalSGST;
                cmd.Parameters.AddWithValue("@TotalCGST", SqlDbType.Float).Value = mnTotalCGST;
                cmd.Parameters.AddWithValue("@TotalIGST", SqlDbType.Float).Value = mnTotalIGST;
                cmd.Parameters.AddWithValue("@TotalTax", SqlDbType.Float).Value = mnTotalTax;
                cmd.Parameters.AddWithValue("@TotalAmount", SqlDbType.Float).Value = mnTotalAmount;
                cmd.Parameters.AddWithValue("@GrandTotal", SqlDbType.Float).Value = mnGrandTotal;
                cmd.Parameters.AddWithValue("@DiscountAmount", SqlDbType.Float).Value = mnDiscountAmount;
                cmd.Parameters.AddWithValue("@InvoiceTotal", SqlDbType.Float).Value = mnInvoiceTotal;
                cmd.Parameters.AddWithValue("@InvoiceRemarks", SqlDbType.VarChar).Value = msInvoiceRemarks;
                cmd.Parameters.AddWithValue("@Created", SqlDbType.SmallDateTime).Value = mdCreated;
                cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = msCreatedBy;
                cmd.Parameters.AddWithValue("@Modified", SqlDbType.SmallDateTime).Value = mdModified;
                cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = msModifiedBy;


               // lObjCon.Open();
                mnInvoiceSNo = (int)cmd.ExecuteScalar();
                // lObjCon.Close();

                // note:- we don't require lobjCon.Open() or Close() beacuse
                //this lObjzCon is parameter and hence it is open at that place from where it has passed


                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

    }
}
