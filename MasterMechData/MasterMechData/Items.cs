using MasterMechPrj;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using System.ComponentModel;

namespace MasterMechData
{
    public class Items
    {
        public int mnItemNo;
        public string msItemDesc;
        public string msItemType;
        public string msItemCategory;
        public double mnItemPrice;
        public string msItemUOM;
        public string msItemStatus;
        public double mnCGST;
        public double mnSGST;
        public double mnIGST;
        public string msUPCCode;
        public string msHSNCode;
        public string msSACCode;
        public double mnQtyHand;
        public double mnReorderLevel;
        public double mnReorderQty;
        public int mnNoOfParts;
        public string msItemRemarks;
        public string msCreatedBy;
        public DateTime mdCreated;
        public DateTime? mdModified;
        public string msModifiedBy;
        public char mcDeleted;
        public DateTime? mdDeletedOn;
        public string mdDeletedBy;



        public string msConnStr;
        public Items() 
        {
            msConnStr = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
        }

        public bool SearchUniqueID(string isItemDesc)
        {
            try
            {

                //string lsConString = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
                SqlConnection lObjCon = new SqlConnection(msConnStr);
                lObjCon.Open();
                string lsQuery = $"Select *from ItemMaster Where ItemDesc = '{isItemDesc}'";
                SqlCommand lObjCmd = new SqlCommand(lsQuery, lObjCon);
                SqlDataReader lObjReader = lObjCmd.ExecuteReader();

                if (lObjReader.HasRows)
                {
                    return true;
                }

                lObjCon.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        public List<Items> AdvanceSearch(string isItemDesc, string isItemType, string isItemCatg)
        {
            List<Items> ListRecordsMatched = new List<Items>();

            string query = $"SELECT * FROM ItemMaster WHERE ItemDesc LIKE '{isItemDesc}%' and ItemType LIKE '{isItemType}%' and ItemCatg LIKE '{isItemCatg}%' and Deleted = 'N'";

            using (SqlConnection connection = new SqlConnection(msConnStr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Items lObjItem = new Items();

                            lObjItem.mnItemNo = (int)(reader["ItemNo"]);
                            lObjItem.msItemDesc = reader["ItemDesc"].ToString();

                            lObjItem.msItemType = reader["ItemType"].ToString();
                            lObjItem.msItemCategory = reader["ItemCatg"].ToString();
                            lObjItem.mnItemPrice = (double)reader["ItemPrice"];
                            lObjItem.msItemUOM = reader["ItemUOM"].ToString();
                            lObjItem.msItemStatus = reader["ItemSts"].ToString();
                            lObjItem.mnCGST = (double)reader["CGSTRate"];
                            lObjItem.mnSGST = (double)reader["SGSTRate"];
                            lObjItem.mnIGST = (double)reader["IGSTRate"];
                            lObjItem.msUPCCode = reader["UPCCode"].ToString();
                            lObjItem.msHSNCode = reader["HSNCode"].ToString();
                            lObjItem.msSACCode = reader["SACCode"].ToString();
                            lObjItem.mnQtyHand = (double)reader["QtyHand"];
                            lObjItem.mnReorderQty = (double)reader["ReorderQty"];
                            lObjItem.msItemRemarks = reader["ItemRemarks"].ToString();
                            lObjItem.mnNoOfParts = (int)reader["NoOfParts"];
                            lObjItem.mnReorderLevel = (double)reader["ReOrderLvl"];
                            lObjItem.mdCreated = (DateTime)reader["Created"];
                            lObjItem.msCreatedBy = reader["CreatedBy"].ToString();
                            lObjItem.mdModified = (DateTime)reader["Modified"];
                            lObjItem.msModifiedBy = reader["ModifiedBy"].ToString();
                            

                            ListRecordsMatched.Add(lObjItem);
                        }
                    }
                }
            }
            return ListRecordsMatched;


        }
        public List<Items> ListData(string isItemDesc)
        {
            List<Items> ListRecordsMatched = new List<Items>();

            string query = $"SELECT * FROM ItemMaster WHERE ItemDesc Like '{isItemDesc}%' and Deleted = 'N'";

            using (SqlConnection connection = new SqlConnection(msConnStr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Items lObjItems = new Items();

                            lObjItems.mnItemNo = (int)reader["ItemNo"];
                            lObjItems.msItemDesc = reader["ItemDesc"].ToString();
                            lObjItems.msItemType = reader["ItemType"].ToString();
                            lObjItems.msItemCategory = reader["ItemCatg"].ToString();
                            lObjItems.mnItemPrice = (double)reader["ItemPrice"];
                            lObjItems.msItemUOM = reader["ItemUOM"].ToString();
                            lObjItems.msItemStatus = reader["ItemSts"].ToString();
                            lObjItems.mnCGST = (double)reader["CGSTRate"];
                            lObjItems.mnSGST = (double)reader["SGSTRate"];
                            lObjItems.mnIGST = (double)reader["IGSTRate"];
                            lObjItems.msUPCCode = reader["UPCCode"].ToString();
                            lObjItems.msHSNCode = reader["HSNCode"].ToString();
                            lObjItems.msSACCode = reader["SACCode"].ToString();
                            lObjItems.mnQtyHand = (double)reader["QtyHand"];
                            lObjItems.mnReorderLevel = (double)reader["ReorderLvl"];
                            lObjItems.mnReorderQty = (double)reader["ReorderQty"];
                            lObjItems.mnNoOfParts = (int)reader["NoOfParts"];
                            lObjItems.msItemRemarks = reader["ItemRemarks"].ToString();



                            lObjItems.mdCreated = (DateTime)reader["Created"];
                            lObjItems.msCreatedBy = reader["CreatedBy"].ToString();
                            
                            lObjItems.msModifiedBy = (reader["Modified"] != DBNull.Value) ? reader["Modified"].ToString() : null;
                            
                            lObjItems.msModifiedBy = reader["ModifiedBy"].ToString();
                            
                            ListRecordsMatched.Add(lObjItems);
                        }
                    }
                }
            }
            return ListRecordsMatched;


        }
        
        public void DeleteData(string isItemNo)
        {
            try
            {

                //string lsConString = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
                SqlConnection lObjCon = new SqlConnection(msConnStr);
                lObjCon.Open();

                // No data is exactly deleted it is available for auditing purpose so you cannot delete it only change the status
                string lsQuery = "Update ItemMaster Set ";
                lsQuery += "Deleted = @Deleted, ";
                lsQuery += "DeletedOn = @DeletedOn, ";
                lsQuery += "DeletedBy = @DeletedBy ";
                lsQuery += "Where ItemNo = @ItemNo and Deleted = 'N'";

                SqlCommand lObjCmd = new SqlCommand(lsQuery, lObjCon);

                lObjCmd.Parameters.AddWithValue("@Deleted", SqlDbType.VarChar).Value = 'Y';
                lObjCmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.Time).Value = DateTime.Now;
                lObjCmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.VarChar).Value = "Prateek";
                lObjCmd.Parameters.AddWithValue("@ItemNo", SqlDbType.Int).Value = isItemNo;


                SqlDataReader lObjReader = lObjCmd.ExecuteReader();

                int lnRowsAffected = lObjReader.RecordsAffected;

                if (lnRowsAffected > 0)
                    MessageBox.Show("Deleted Successfully");
                else
                    MessageBox.Show("No Such Record");

                lObjCon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveSQL(MasterMechUtil.OPMode inMode)
        {
            string lsQueryText = "";
            
            using (SqlConnection lObjConn = new SqlConnection(msConnStr))
            {
                if (inMode == MasterMechUtil.OPMode.New)
                {
                   
                    lsQueryText = "INSERT INTO ItemMaster(ItemDesc, ItemType, ItemCatg, ItemPrice, ItemUOM, ItemSts, CGSTRate, SGSTRate, IGSTRate, UPCCode, HSNCode, SACCode, QtyHand, ReOrderLvl, ReorderQty, NoOfParts, ItemRemarks, Created, CreatedBy, Modified, ModifiedBy, Deleted, DeletedOn, DeletedBy ) Values";
                    lsQueryText += "(@ItemDesc, @ItemType, @ItemCatg, @ItemPrice, @ItemUOM, @ItemSts, @CGSTRate, @SGSTRate, @IGSTRate, @UPCCode, @HSNCode, @SACCode, @QtyHand, @ReOrderLvl, @ReorderQty, @NoOfParts, @ItemRemarks, @Created, @CreatedBy, @Modified, @ModifiedBy, 'N', NULL, 'N' )";
                    
                    MessageBox.Show("Inserted Successfully");
                }
                else if (inMode == MasterMechUtil.OPMode.Open)
                {
                   
                    lsQueryText = "UPDATE ItemMaster SET ";

                    lsQueryText += "ItemDesc = @ItemDesc,";
                    lsQueryText += "ItemType = @ItemType,";
                    lsQueryText += "ItemCatg = @ItemCatg,";
                    lsQueryText += "ItemPrice = @ItemPrice,";
                    lsQueryText += "ItemUOM = @ItemUOM,";
                    lsQueryText += "ItemSts = @ItemSts,";
                    lsQueryText += "CGSTRate = @CGSTRate,";
                    lsQueryText += "SGSTRate = @SGSTRate,";

                    lsQueryText += "IGSTRate = @IGSTRate,";
                    lsQueryText += "UPCCode = @UPCCode,";
                    lsQueryText += "HSNCode = @HSNCode,";
                    lsQueryText += "SACCode = @SACCode,";
                    lsQueryText += "QtyHand = @QtyHand,";
                    lsQueryText += "ReOrderLvl = @ReOrderLvl,";
                    lsQueryText += "ReorderQty = @ReorderQty,";
                    lsQueryText += "NoOfParts = @NoOfParts,";
                    lsQueryText += "ItemRemarks = @ItemRemarks,";

                    lsQueryText += "Created = @Created,";
                    lsQueryText += "CreatedBy = @CreatedBy,";
                    lsQueryText += "Modified = @Modified,";
                    lsQueryText += "ModifiedBy = @ModifiedBy ";
                    lsQueryText += "WHERE ItemNo = @ItemNo";

                    MessageBox.Show("Updated Successfully");

                }
                using (SqlCommand cmd = new SqlCommand(lsQueryText, lObjConn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@ItemNo", SqlDbType.Int).Value = mnItemNo;
                        cmd.Parameters.AddWithValue("@ItemDesc", SqlDbType.VarChar).Value = msItemDesc;
                        cmd.Parameters.AddWithValue("@ItemType", SqlDbType.VarChar).Value = msItemType;
                        cmd.Parameters.AddWithValue("@ItemCatg", SqlDbType.VarChar).Value = msItemCategory;
                        cmd.Parameters.AddWithValue("@ItemPrice", SqlDbType.Float).Value = mnItemPrice;
                        cmd.Parameters.AddWithValue("@ItemUOM", SqlDbType.VarChar).Value = msItemUOM;
                        cmd.Parameters.AddWithValue("@ItemSts", SqlDbType.VarChar).Value = msItemStatus;
                        cmd.Parameters.AddWithValue("@CGSTRate", SqlDbType.Float).Value = mnCGST;
                        cmd.Parameters.AddWithValue("@SGSTRate", SqlDbType.Float).Value = mnSGST;

                        cmd.Parameters.AddWithValue("@IGSTRate", SqlDbType.Float).Value = mnIGST;
                        cmd.Parameters.AddWithValue("@UPCCode", SqlDbType.VarChar).Value = msUPCCode;
                        cmd.Parameters.AddWithValue("@HSNCode", SqlDbType.VarChar).Value = msHSNCode;
                        cmd.Parameters.AddWithValue("@SACCode", SqlDbType.VarChar).Value = msSACCode;
                        cmd.Parameters.AddWithValue("@QtyHand", SqlDbType.Float).Value = mnQtyHand;
                        cmd.Parameters.AddWithValue("@ReOrderLvl", SqlDbType.Float).Value = mnReorderLevel;
                        cmd.Parameters.AddWithValue("@ReorderQty", SqlDbType.Float).Value = mnReorderQty;
                        cmd.Parameters.AddWithValue("@NoOfParts", SqlDbType.Int).Value = mnNoOfParts;
                        cmd.Parameters.AddWithValue("@ItemRemarks", SqlDbType.VarChar).Value = msItemRemarks;
                        
                        cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = msCreatedBy;
                        cmd.Parameters.AddWithValue("@Created", SqlDbType.Time).Value = DateTime.Now;

                        cmd.Parameters.AddWithValue("@Modified", SqlDbType.Time).Value = mdModified;
                        //When Modify, UserName is saved as Modified BY
                        cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = "Prateek";

                        lObjConn.Open();
                        cmd.ExecuteNonQuery();
                        lObjConn.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                      
                    }


                }
            }
        }

    }
}
