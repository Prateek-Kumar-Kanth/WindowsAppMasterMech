using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using MasterMechPrj;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MasterMechData
{
    public class User
    {
        [Display(Name = "User ID")]
        public string msUserID { get; set; }

        [Display(Name = "User Password")]
        public string msPassword { get; set; }

        [Display(Name = "Name")]
        public string msUserName { get; set; }

        [Display(Name = "Mobile ")]
        public string msMobileNo { get; set; }

        [Display(Name = "Email")]
        public string msEmail { get; set; }

        [Display(Name = "User Type")]
        public string msUserType { get; set; }

        [Display(Name = "Remarks")]
        public string msRemarks { get; set; }

        [Display(Name = "Last Login")]
        public DateTime? mdLastLoginTime { get; set; }

        [Display(Name = "Password Change Time")]
        public DateTime? mdLastPasswordChangeTime { get; set; }

        [Display(Name = "Created By")]
        public string msCreatedBy { get; set; }

        [Display(Name = "Created")]
        public DateTime mdCreated { get; set; }

        [Display(Name = "Modified")]
        public DateTime? mdModified { get; set; }

        [Display(Name = "Modified By")]
        public string msModifiedBy { get; set; }

        public char mcDeleted { get; set; }

        public DateTime mdDeletedOn { get; set; }

        public string mdDeletedBy { get; set; }

        public string msConnStr { get; set; }

        public string FYr {  get; set; }

        //public string msPassword;
        //public string msUserName;
        //public string msMobileNo;
        //public string msEmail;
        //public string msUserType;
        //public string msRemarks;
        //public DateTime mdLastLoginTime;
        //public DateTime mdLastPasswordChangeTime;
        //public string msCreatedBy;
        //public DateTime mdCreated;
        //public DateTime mdModified;
        //public string msModifiedBy;
        //public char mcDeleted;
        //public DateTime mdDeletedOn;
        //public string mdDeletedBy;
        //public string msConnStr;

     
        public User()
        {
            msConnStr = "Data Source=DESKTOP-QA6D9NP\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
        }

        //Use Only in Web MasterMech
        public bool ValidLogin(string isConStr)
        {
            bool lbValidUser = false;
            using (SqlConnection lObjCon = new SqlConnection(isConStr))
            {

                string lsQuery = "SELECT UserType FROM UserDtl WHERE UserID = @UserID AND Pwd=@Pwd AND Deleted='N'";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(lsQuery))
                    {
                        cmd.Connection = lObjCon;
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = msUserID;
                        cmd.Parameters.AddWithValue("@Pwd", SqlDbType.VarChar).Value = msPassword;

                        lObjCon.Open();
                        using (SqlDataReader lObjSDR = cmd.ExecuteReader())
                        {
                            if (lObjSDR.HasRows)
                            {
                                while (lObjSDR.Read())
                                {
                                    msUserType = lObjSDR["UserType"].ToString();
                                    lbValidUser = true;
                                }
                            }
                        }
                        lObjCon.Close();
                    }
                }

                catch (SqlException ex)
                {
                    //MasterMechUtil.ShowError(ex.Message);
                }
            }

            return lbValidUser;
        }
        public void UpdateLastLogin(string isUserID)
        {
            try
            {

                SqlConnection lObjCon = new SqlConnection(msConnStr);
                lObjCon.Open();
                string lsQuery = $"UPDATE UserDtl SET LastLoginTime = @LastLoginTime WHERE UserID = @UserID";
                SqlCommand lObjCmd = new SqlCommand(lsQuery, lObjCon);

                lObjCmd.Parameters.AddWithValue("@LastLoginTime", SqlDbType.Time).Value = DateTime.Now;
                lObjCmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = isUserID;

                SqlDataReader lObjReader = lObjCmd.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        public void ReadLoginData(string isUserID, string isPassword)
        {

            try
            {

                SqlConnection lObjCon = new SqlConnection(msConnStr);
                lObjCon.Open();
                string lsQuery = $"Select * from UserDtl Where UserID = '{isUserID}' and Pwd = '{isPassword}' and Deleted = 'N'";
                SqlCommand lObjCmd = new SqlCommand(lsQuery, lObjCon);
                SqlDataReader lObjReader = lObjCmd.ExecuteReader();

                if (lObjReader.Read())
                {
                    string lsUserType = lObjReader["UserType"].ToString();
                    //MasterMechUtil.mdLastLogin = (DateTime)lObjReader["LastLoginTime"];
                    MasterMechUtil.msUserName = lObjReader["UserName"].ToString();

                    MasterMechUtil.msUserType = lObjReader["UserType"].ToString(); ;

                    UpdateLastLogin(isUserID); // Update Last Login Time After Login

                }
                else
                    MasterMechUtil.msUserType = "";
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public List<User> ListData(string isUserID)
        {
            List<User> ListRecordsMatched = new List<User>();
           
            string query = $"SELECT * FROM UserDtl Where UserId like  '{isUserID}%' and Deleted = 'N'";

            using (SqlConnection connection = new SqlConnection(msConnStr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User lObjUser = new User();

                            lObjUser.msUserID = reader["UserID"].ToString();
                            lObjUser.msPassword = reader["Pwd"].ToString();

                            lObjUser.msUserName = reader["UserName"].ToString();
                            lObjUser.msMobileNo = reader["MobNo"].ToString();
                            lObjUser.msEmail = reader["EmailID"].ToString();
                            lObjUser.msUserType = reader["UserType"].ToString();
                            lObjUser.msRemarks = reader["Remarks"].ToString();

                            
                            lObjUser.mdLastLoginTime = reader["LastLoginTime"] != DBNull.Value ? (DateTime)reader["LastLoginTime"] : (DateTime?)null;

                            lObjUser.mdLastPasswordChangeTime = reader["LastPwdChangeTime"] != DBNull.Value ? (DateTime?)reader["LastPwdChangeTime"] : (DateTime?)null;


                            lObjUser.msCreatedBy = reader["CreatedBy"].ToString();
                            lObjUser.mdCreated = (DateTime)reader["Created"];

                            lObjUser.mdModified = reader["Modified"] != DBNull.Value ? (DateTime?)reader["Modified"] : (DateTime?)null;
                            lObjUser.msModifiedBy = reader["ModifiedBy"].ToString();

                            ListRecordsMatched.Add(lObjUser);
                        }
                    }
                }
            }
            return ListRecordsMatched;

            
        }
        public void DeleteData(string isUserID)
        {
            try
            {

                //string lsConString = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
                SqlConnection lObjCon = new SqlConnection(msConnStr);
                lObjCon.Open();

                // No data is exactly deleted it is available for auditing purpose so you cannot delete it only change the status
                string lsQuery = "Update UserDtl Set ";
                lsQuery += "Deleted = @Deleted, ";
                lsQuery += "DeletedOn = @DeletedOn, ";
                lsQuery += "DeletedBy = @DeletedBy ";
                lsQuery += "Where UserId = @UserID and Deleted = 'N'";

                SqlCommand lObjCmd = new SqlCommand(lsQuery, lObjCon);

                lObjCmd.Parameters.AddWithValue("@Deleted", SqlDbType.VarChar).Value = 'Y';
                lObjCmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.Time).Value = DateTime.Now;
                lObjCmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.VarChar).Value = MasterMechUtil.msUserName;
                lObjCmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = isUserID;


                SqlDataReader lObjReader = lObjCmd.ExecuteReader();

                if(lObjReader.RecordsAffected>0)
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
        public bool SearchUniqueID(string iUserID)
        {
            try
            {

                //string lsConString = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
                SqlConnection lObjCon = new SqlConnection(msConnStr);
                lObjCon.Open();
                string lsQuery = $"Select *from UserDtl Where UserID = '{iUserID}'";
                SqlCommand lObjCmd = new SqlCommand(lsQuery, lObjCon);
                SqlDataReader lObjReader = lObjCmd.ExecuteReader();

                if(lObjReader.HasRows)
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
        public bool SaveSQL(MasterMechUtil.OPMode inMode)
        {
            string lsQueryText = "";
            //msConnStr = "Data Source=SOI\\SQLEXPRESS;Initial Catalog=MasterMech;Integrated Security=True";
            bool lbOpType;
            using (SqlConnection lObjConn = new SqlConnection(msConnStr))
            {
                if (inMode == MasterMechUtil.OPMode.New)
                {
                    lsQueryText = "INSERT INTO UserDtl(UserID, Pwd, UserName, MobNo, EmailID, UserType, LastLoginTime, LastPwdChangeTime, Remarks, Created, CreatedBy, Modified, ModifiedBy, Deleted, DeletedOn, DeletedBy) Values";
                    lsQueryText += "(@UserID, @Pwd, @UserName, @MobNo, @EmailID, @UserType, @LastLoginTime, @LastPwdChangeTime, @Remarks, @Created, @CreatedBy, @Modified, @ModifiedBy, 'N', NULL, 'N')";
                    lbOpType =  true;
                }
                else
                {

                    lsQueryText = "UPDATE UserDtl SET ";
              
                    lsQueryText += "Pwd = @Pwd,";
                    lsQueryText += "UserName = @UserName,";
                    lsQueryText += "MobNo = @MobNo,";
                    lsQueryText += "EmailID = @EmailID,";
                    lsQueryText += "UserType = @UserType,";
                    lsQueryText += "LastLoginTime = @LastLoginTime,";
                    lsQueryText += "LastPwdChangeTime = @LastPwdChangeTime,";
                    lsQueryText += "Remarks = @Remarks,";
                    lsQueryText += "Created = @Created,";
                    lsQueryText += "CreatedBy = @CreatedBy,";
                    lsQueryText += "Modified = @Modified,";
                    lsQueryText += "ModifiedBy = @ModifiedBy ";
                    lsQueryText += "WHERE UserID = @UserID";
                    lbOpType = false;

                }
                using (SqlCommand cmd = new SqlCommand(lsQueryText, lObjConn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = msUserID;
                        cmd.Parameters.AddWithValue("@Pwd", SqlDbType.VarChar).Value = msPassword;
                        cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = msUserName;
                        cmd.Parameters.AddWithValue("@MobNo", SqlDbType.VarChar).Value = msMobileNo;
                        cmd.Parameters.AddWithValue("@EmailID", SqlDbType.VarChar).Value = msEmail;
                        cmd.Parameters.AddWithValue("@UserType", SqlDbType.VarChar).Value = msUserType;
                        cmd.Parameters.AddWithValue("@Remarks", SqlDbType.VarChar).Value = msRemarks;
                        cmd.Parameters.AddWithValue("@LastLoginTime", SqlDbType.Time).Value = mdLastLoginTime;
                        cmd.Parameters.AddWithValue("@LastPwdChangeTime", SqlDbType.Time).Value = mdLastPasswordChangeTime;
                        cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = msCreatedBy;
                        cmd.Parameters.AddWithValue("@Created", SqlDbType.Time).Value = mdCreated;

                        cmd.Parameters.AddWithValue("@Modified", SqlDbType.Time).Value = DateTime.Now;
                        //When Modify, UserName is save as Modified BY
                        cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = MasterMechUtil.msUserName;
                        
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
            return lbOpType;
        }
    }
}
