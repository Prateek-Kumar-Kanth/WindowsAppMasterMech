using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MasterMechPrj;
using System.Globalization;

namespace MasterMechData
{
    public class Customer
    {
        public int mnCustomerNo;
        public string msCustFirstName;
        public string msCustLastName;
        public string msCustMobile;
        public string msCustEmail;
        public string msCustStatus;
        public string msCustType;
        public string msCustStreet;
        public string msCustArea;
        public string msCustCity;
        public string msCustState;
        public string msCustPincode;
        public string msCustCountry;
        public string msCustGSTNo;
        public DateTime mdCustLastVisit;
        public string msCustRemarks;
        public DateTime mdCustCreated;
        public string msCustCreatedBy;
        public DateTime? mdCustModified;
        public string msCustModifiedBy;
        public char mcCustDeleted;
        public DateTime mdDeletedOn;
        public string mcCustDeletedBy;
        public string msConnStr;

        public static string UserName { get; set; }
        public Customer() 
        {
            UserName = MasterMechUtil.msUserName;
            msConnStr = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
           

        }
        public bool SearchUniqueID(string iCustMobNo)
        {
            try
            {

                //string lsConString = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
                SqlConnection lObjCon = new SqlConnection(msConnStr);
                lObjCon.Open();
                string lsQuery = $"Select *from Customer Where CustMobNo = '{iCustMobNo}'";
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
        public List<Customer> ListData(string isMobileNo)
        {
            List<Customer> ListRecordsMatched = new List<Customer>();

            string query = $"SELECT * FROM Customer WHERE CustMobNo Like '{isMobileNo}%' and Deleted = 'N'";

            using (SqlConnection connection = new SqlConnection(msConnStr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer lObjUser = new Customer();

                            lObjUser.mnCustomerNo = (int)(reader["CustNo"]);
                            lObjUser.msCustFirstName = reader["CustFName"].ToString();

                            lObjUser.msCustLastName = reader["CustLName"].ToString();
                            lObjUser.msCustMobile = reader["CustMobNo"].ToString();
                            lObjUser.msCustEmail = reader["CustEmail"].ToString();
                            lObjUser.msCustStatus = reader["CustSts"].ToString();
                            lObjUser.msCustType = reader["CustType"].ToString();
                            lObjUser.msCustStreet = reader["CustStAddr"].ToString();
                            lObjUser.msCustArea = reader["CustArAddr"].ToString();
                            lObjUser.msCustCity = reader["CustCity"].ToString();
                            lObjUser.msCustState = reader["CustSts"].ToString();
                            lObjUser.msCustPincode = reader["CustPinCode"].ToString();
                            lObjUser.msCustCountry = reader["CustCountry"].ToString();
                            lObjUser.msCustGSTNo = reader["CustGSTNo"].ToString();
                            lObjUser.mdCustLastVisit = (DateTime)reader["CustLastVisit"];
                            lObjUser.msCustRemarks = reader["CustRemarks"].ToString();
                            lObjUser.mdCustCreated = (DateTime)reader["Created"];
                            lObjUser.msCustCreatedBy = reader["CreatedBy"].ToString();
                            lObjUser.mdCustModified = (DateTime?)reader["Modified"];
                            lObjUser.msCustModifiedBy = reader["ModifiedBy"].ToString();
                            //lObjUser.mcCustDeleted = (char)reader["Deleted"];
                            //lObjUser.mdDeletedOn = (DateTime)(reader["DeletedOn"]);
                            //lObjUser.mcCustDeletedBy = reader["DeletedBy"].ToString();

                            
                            ListRecordsMatched.Add(lObjUser);
                        }
                    }
                }
            }
            return ListRecordsMatched;


        }

        public List<Customer> AdvanceSearch(string isFName, string isLName, string isCity)
        {
            List<Customer> ListRecordsMatched = new List<Customer>();

            string query = $"SELECT * FROM Customer WHERE CustFName Like '{isFName}%' and CustLName Like '{isLName}%' and CustCity Like '{isCity}%' and Deleted = 'N'";

            using (SqlConnection connection = new SqlConnection(msConnStr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer lObjUser = new Customer();

                            lObjUser.mnCustomerNo = (int)(reader["CustNo"]);
                            lObjUser.msCustFirstName = reader["CustFName"].ToString();

                            lObjUser.msCustLastName = reader["CustLName"].ToString();
                            lObjUser.msCustMobile = reader["CustMobNo"].ToString();
                            lObjUser.msCustEmail = reader["CustEmail"].ToString();
                            lObjUser.msCustStatus = reader["CustSts"].ToString();
                            lObjUser.msCustType = reader["CustType"].ToString();
                            lObjUser.msCustStreet = reader["CustStAddr"].ToString();
                            lObjUser.msCustArea = reader["CustArAddr"].ToString();
                            lObjUser.msCustCity = reader["CustCity"].ToString();
                            lObjUser.msCustState = reader["CustSts"].ToString();
                            lObjUser.msCustPincode = reader["CustPinCode"].ToString();
                            lObjUser.msCustCountry = reader["CustCountry"].ToString();
                            lObjUser.msCustGSTNo = reader["CustGSTNo"].ToString();
                            lObjUser.mdCustLastVisit = (DateTime)reader["CustLastVisit"];
                            lObjUser.msCustRemarks = reader["CustRemarks"].ToString();
                            lObjUser.mdCustCreated = (DateTime)reader["Created"];
                            lObjUser.msCustCreatedBy = reader["CreatedBy"].ToString();
                            lObjUser.mdCustModified = (DateTime)reader["Modified"];
                            lObjUser.msCustModifiedBy = reader["ModifiedBy"].ToString();
                            //lObjUser.mcCustDeleted = (char)reader["Deleted"];
                            //lObjUser.mdDeletedOn = (DateTime)(reader["DeletedOn"]);
                            //lObjUser.mcCustDeletedBy = reader["DeletedBy"].ToString();


                            ListRecordsMatched.Add(lObjUser);
                        }
                    }
                }
            }
            return ListRecordsMatched;

        }
        public void DeleteData(string isCustNo)
        {
            try
            {

                //string lsConString = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
                SqlConnection lObjCon = new SqlConnection(msConnStr);
                lObjCon.Open();

                // No data is exactly deleted it is available for auditing purpose so you cannot delete it, only change the status
                string lsQuery = "Update Customer Set ";
                lsQuery += "Deleted = @Deleted, ";
                lsQuery += "DeletedOn = @DeletedOn, ";
                lsQuery += "DeletedBy = @DeletedBy ";
                lsQuery += "Where CustNo = @CustNo and Deleted = 'N'";

                SqlCommand lObjCmd = new SqlCommand(lsQuery, lObjCon);

                lObjCmd.Parameters.AddWithValue("@Deleted", SqlDbType.Char).Value = 'Y';
                lObjCmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.Time).Value = DateTime.Now;
                lObjCmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.VarChar).Value = UserName; //This UserName Value is accessed From User.cs class File
                lObjCmd.Parameters.AddWithValue("@CustNo", SqlDbType.Int).Value = isCustNo;


                SqlDataReader lObjReader = lObjCmd.ExecuteReader();

                int lnRowsAffected = lObjReader.RecordsAffected;
                if (lnRowsAffected > 0)
                    MessageBox.Show("Deleted Successfully");
                else
                    MessageBox.Show("No Such Record");     //Reader.HasRows is not working why

                lObjCon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SaveSQL(MasterMechUtil.OPMode iMode)
        {
            string lsQueryText = "";
            string msConnStr = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";

            using (SqlConnection lObjConn = new SqlConnection(msConnStr))
            {
                if (iMode == MasterMechUtil.OPMode.New)
                {
                    lsQueryText = "INSERT INTO Customer(CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr, CustCity, CustState, CustPinCode, CustCountry, CustGSTNo, CustLastVisit, CustRemarks, Created, CreatedBy, Modified, ModifiedBy, Deleted, DeletedOn, DeletedBy) Values";
                    lsQueryText += "(@CustFName, @CustLName, @CustMobNo, @CustEmail, @CustSts, @CustType, @CustStAddr, @CustArAddr, @CustCity, @CustState, @CustPinCode, @CustCountry, @CustGSTNo, @CustLastVisit, @CustRemarks, @Created, @CreatedBy, @Modified, @ModifiedBy, 'N', NULL, 'N')";
                    MessageBox.Show("Inserted Successfully");
                }
                else if (iMode == MasterMechUtil.OPMode.Open)
                {
                   
                    lsQueryText = "UPDATE Customer SET ";
                    lsQueryText += "CustFName=@CustFName,";
                    lsQueryText += "CustLName=@CustLName,";
                    lsQueryText += "CustMobNo=@CustMobNo,";
                    lsQueryText += "CustEmail=@CustEmail,";
                    lsQueryText += "CustSts=@CustSts,";
                    lsQueryText += "CustType=@CustType,";
                    lsQueryText += "CustStAddr=@CustStAddr,";
                    lsQueryText += "CustArAddr=@CustArAddr,";
                    lsQueryText += "CustCity=@CustCity,";
                    lsQueryText += "CustState=@CustState,";
                    lsQueryText += "CustPinCode=@CustPinCode,";
                    lsQueryText += "CustCountry=@CustCountry,";
                    lsQueryText += "CustGSTNo=@CustGSTNo,";
                    lsQueryText += "CustLastVisit=@CustLastVisit,";
                    lsQueryText += "CustRemarks=@CustRemarks,";
                    lsQueryText += "Created=@Created,";
                    lsQueryText += "CreatedBy=@CreatedBy,";
                    lsQueryText += "Modified=@Modified,";
                    lsQueryText += "ModifiedBy=@ModifiedBy ";
                    lsQueryText += "where CustNo=@CustNo ";


                    MessageBox.Show("Updated Successfully");
                }
                using (SqlCommand cmd = new SqlCommand(lsQueryText, lObjConn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@CustNo", SqlDbType.VarChar).Value = mnCustomerNo;
                        cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = msCustFirstName;
                        cmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = msCustLastName;
                        cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = msCustMobile;
                        cmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = msCustEmail;
                        cmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = msCustStatus;
                        cmd.Parameters.AddWithValue("@CustType", SqlDbType.VarChar).Value = msCustType;
                        cmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.Time).Value = msCustStreet;
                        cmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.Time).Value = msCustArea;
                        cmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = msCustCity;
                        cmd.Parameters.AddWithValue("@CustState", SqlDbType.Time).Value = msCustState;
                        cmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.Time).Value = msCustPincode;
                        cmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = msCustCountry;
                        cmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = msCustGSTNo;
                        cmd.Parameters.AddWithValue("@CustLastVisit", SqlDbType.VarChar).Value = mdCustLastVisit;
                        cmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = msCustRemarks;
                        cmd.Parameters.AddWithValue("@Created", SqlDbType.VarChar).Value = mdCustCreated;
                        cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = msCustCreatedBy;
                        cmd.Parameters.AddWithValue("@Modified", SqlDbType.VarChar).Value = mdCustModified;
                        cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = msCustModifiedBy;

                        lObjConn.Open();
                        cmd.ExecuteNonQuery();
                        lObjConn.Close();
                       
                        //return true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //return false;
                    }


                }
            }
        }

    }
}
