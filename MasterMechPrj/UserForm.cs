using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;// To Use User.cs Class
using MasterMechData;
using System.Data.SqlClient;
using MasterMechPrj;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MainForm
{
    public partial class UserForm : Form
    {
        MasterMechUtil.OPMode mnMode;
        public UserForm()
        {
            InitializeComponent();
        }
        public UserForm(MasterMechUtil.OPMode iOPMode)
        {
            InitializeComponent();
            mnMode = iOPMode;
        }
        public bool Input()
        {
            string lsUserId = TextBoxUserID.Text;
            string lsPassword = TextBoxPassword.Text;
            string lsPassConfirm = TextBoxPasswordConfirm.Text;
            string lsUserName = TextBoxUserName.Text;
            string lsMob = TextBoxMobile.Text;
            string lsEmail = TextBoxEmail.Text;
            string lsRemarks = TextBoxRemarks.Text;
            bool lbEmptyFound = false;
            if (lsUserId == "")
            {
                LabelUserIDValid.ForeColor = Color.Red;
                LabelUserIDValid.Text = "Empty Field";
                lbEmptyFound = true;
            }
            if (lsPassword == "")
            {
                LabelPassValid.ForeColor = Color.Red;
                LabelPassValid.Text = "Empty Field"; ;
                lbEmptyFound = true;
            }
            if (lsPassConfirm == "")
            {
                LabelCnfPassValid.ForeColor = Color.Red;
                LabelCnfPassValid.Text = "Empty Field"; ;
                lbEmptyFound = true;
            }
            if (lsUserName == "")
            {
                LabelUserNameValid.ForeColor = Color.Red;
                LabelUserNameValid.Text = "Empty Field";
                lbEmptyFound = true;
            }
            if (lsMob == "")
            {
                LabelMobileValid.ForeColor = Color.Red;
                LabelMobileValid.Text = "Empty Field";
                lbEmptyFound = true;
            }
            if (lsEmail == "")
            {
                LabelEmailValid.ForeColor = Color.Red;
                LabelEmailValid.Text = "Empty Field";
                lbEmptyFound = true;
            }
            if(lsRemarks =="")
            {
                LabelRemarkValid.ForeColor = Color.Red;
                LabelRemarkValid.Text = "Empty Field";
                lbEmptyFound = true;
            }
            
            if(lbEmptyFound)
                return false;
            else
                return true;
        }
        public void UserData(List<User> iObjUserResultList)
        {
            SearchResultForm lObjResult = new SearchResultForm();
            lObjResult.DataGridResult.Columns.Add("UserIDCol", "User ID");
            lObjResult.DataGridResult.Columns.Add("PasswordCol", "Password");
            lObjResult.DataGridResult.Columns.Add("UserNameCol", "User Name");
            lObjResult.DataGridResult.Columns.Add("MobileCol", "Mobile Number");
            lObjResult.DataGridResult.Columns.Add("EmailCol", "Email");
            lObjResult.DataGridResult.Columns.Add("UserTypeCol", "User Type");
            lObjResult.DataGridResult.Columns.Add("LastLoginCol", "Last Login Time");
            lObjResult.DataGridResult.Columns.Add("LastPwdChangeCol", "Last Password Change Time");
            lObjResult.DataGridResult.Columns.Add("RemarksCol", "Remarks");
            lObjResult.DataGridResult.Columns.Add("CreatedCol", "Created");
            lObjResult.DataGridResult.Columns.Add("CreatedByCol", "Created By");
            lObjResult.DataGridResult.Columns.Add("ModifiedCol", "Modified");
            lObjResult.DataGridResult.Columns.Add("ModifiedByCol", "ModiFied By");


            foreach (User lObj in iObjUserResultList)
            {

                lObjResult.DataGridResult.Rows.Add(
                    lObj.msUserID,
                    lObj.msPassword,
                    lObj.msUserName,
                    lObj.msMobileNo,
                    lObj.msEmail,
                    lObj.msUserType,
                    lObj.mdLastLoginTime,
                    lObj.mdLastPasswordChangeTime,
                    lObj.msRemarks,
                    lObj.mdCreated,
                    lObj.msCreatedBy,
                    lObj.mdModified,
                    lObj.msModifiedBy);
            }
            DialogResult Result = lObjResult.ShowDialog();

            if (Result == DialogResult.OK)
            {
                //Column Name is not a Column Name, it's a Column's Variable Name

                TextBoxUserID.Text = lObjResult.DataGridResult.CurrentRow.Cells["UserIDCol"].Value.ToString();
                TextBoxPassword.Text = lObjResult.DataGridResult.CurrentRow.Cells["PasswordCol"].Value.ToString();
                TextBoxUserName.Text = lObjResult.DataGridResult.CurrentRow.Cells["UserNameCol"].Value.ToString();
                TextBoxMobile.Text = lObjResult.DataGridResult.CurrentRow.Cells["MobileCol"].Value.ToString();
                TextBoxEmail.Text = lObjResult.DataGridResult.CurrentRow.Cells["EmailCol"].Value.ToString();
                ComboBoxUserType.Text = lObjResult.DataGridResult.CurrentRow.Cells["UserTypeCol"].Value.ToString();

                TextBoxLastLogin.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastLoginCol"].Value.ToString();
                TextBoxLastPassword.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastPwdChangeCol"].Value.ToString();
                TextBoxRemarks.Text = lObjResult.DataGridResult.CurrentRow.Cells["RemarksCol"].Value.ToString();
                TextBoxCreated.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedCol"].Value.ToString();
                TextBoxCreatedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedByCol"].Value.ToString();

                TextBoxModified.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedCol"].Value.ToString();
                TextBoxModifiedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedByCol"].Value.ToString();

            }
        }
        public void UserInput()
        {

            User lObjUser = new User();
            switch (mnMode)
            {
                case MasterMechUtil.OPMode.Delete:
                    string lsUserID = TextBoxUserID.Text;

                    DialogResult Result = MessageBox.Show("Do you want to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (Result == DialogResult.Yes)
                        lObjUser.DeleteData(lsUserID);
                    break;

                case MasterMechUtil.OPMode.New:
                case MasterMechUtil.OPMode.Open:
                    lObjUser.msUserID = TextBoxUserID.Text;

                    lObjUser.msPassword = TextBoxPassword.Text;
                    //lObjUser.msPassword = MasterMechUtil.Encrypt(lObjUser.msPassword);

                    lObjUser.msUserName = TextBoxUserName.Text;
                    lObjUser.msMobileNo = TextBoxMobile.Text;
                    lObjUser.msEmail = TextBoxEmail.Text;
                    lObjUser.msUserType = ComboBoxUserType.Text;
                    lObjUser.msRemarks = TextBoxRemarks.Text;

                    //lObjUser.mdLastLoginTime = DateTime.Parse(TextBoxLastLogin.Text);
                    lObjUser.mdLastLoginTime = DateTime.Now;

                    //lObjUser.mdLastPasswordChangeTime = DateTime.Parse(TextBoxLastPassword.Text);
                    lObjUser.mdLastPasswordChangeTime = DateTime.Now;


                    lObjUser.msCreatedBy = TextBoxCreatedBy.Text; // UserId is from LoginPage
                                                                  //lObjUser.mdCreated = DateTime.Parse(TextBoxCreated.Text);
                    lObjUser.mdCreated = DateTime.Now;
                    //lObjUser.mdModified = DateTime.Parse(TextBoxModified.Text);
                    lObjUser.mdModified = DateTime.Now;

                    lObjUser.msModifiedBy = "";

                    if (Input())
                    {
                        if (lObjUser.SaveSQL(mnMode))
                        {
                            MessageBox.Show("Insert Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Updated Successfully");
                        }
                    }
                    break;

            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {

            UserInput();

        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            User lObjUser = new User();
            string lsUserId = TextBoxUserID.Text;

            if (mnMode == MasterMechUtil.OPMode.Open)
            {
                
                List<User> lObjUserResultList = lObjUser.ListData(lsUserId);

                UserData(lObjUserResultList);

            }
            else if (mnMode == MasterMechUtil.OPMode.New)
            {
                
                bool lbIDFound = lObjUser.SearchUniqueID(lsUserId);

                if (lbIDFound)
                    MessageBox.Show("Duplicate User ID");
                else
                    MessageBox.Show("Valid User ID. You can continue");
            }

            //User lObjUser = new User();

            //if (mnMode == MasterMechUtil.OPMode.Open)
            //{
            //    string lsUserID = TextBoxUserID.Text;
            //    List<User> ListUserData = lObjUser.ListData(lsUserID);

            //    SearchResultForm lObjResult = new SearchResultForm(ListUserData, MasterMechUtil.FormType.User);
            //    DialogResult Result = lObjResult.ShowDialog();

            //    if (Result == DialogResult.OK)
            //    {
            //        //Column Name is not a Column Name, it's a Column's Variable Name

            //        TextBoxUserID.Text = lObjResult.DataGridResult.CurrentRow.Cells["UserIDCol"].Value.ToString();
            //        TextBoxPassword.Text = lObjResult.DataGridResult.CurrentRow.Cells["PasswordCol"].Value.ToString();
            //        TextBoxUserName.Text = lObjResult.DataGridResult.CurrentRow.Cells["UserNameCol"].Value.ToString();
            //        TextBoxMobile.Text = lObjResult.DataGridResult.CurrentRow.Cells["MobileCol"].Value.ToString();
            //        TextBoxEmail.Text = lObjResult.DataGridResult.CurrentRow.Cells["EmailCol"].Value.ToString();
            //        ComboBoxUserType.Text = lObjResult.DataGridResult.CurrentRow.Cells["UserTypeCol"].Value.ToString();

            //        TextBoxLastLogin.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastLoginCol"].Value.ToString();
            //        TextBoxLastPassword.Text = lObjResult.DataGridResult.CurrentRow.Cells["LastPwdChangeCol"].Value.ToString();
            //        TextBoxRemarks.Text = lObjResult.DataGridResult.CurrentRow.Cells["RemarksCol"].Value.ToString();
            //        TextBoxCreated.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedCol"].Value.ToString();
            //        TextBoxCreatedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["CreatedByCol"].Value.ToString();

            //        TextBoxModified.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedCol"].Value.ToString();
            //        TextBoxModifiedBy.Text = lObjResult.DataGridResult.CurrentRow.Cells["ModifiedByCol"].Value.ToString();

            //    }

            //}
            //else if (mnMode == MasterMechUtil.OPMode.New)
            //{
            //    string lsUserId = TextBoxUserID.Text;
            //    bool lbIDFound = lObjUser.SearchUniqueID(lsUserId);

            //    if (lbIDFound)
            //        MessageBox.Show("Duplicate User ID");
            //    else
            //        MessageBox.Show("Valid User ID. You can continue");
            //}
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
           
            if(mnMode==MasterMechUtil.OPMode.New)
            {
                TextBoxCreatedBy.Text = MasterMechUtil.msUserName;
            }
            if(mnMode == MasterMechUtil.OPMode.Open)
            {
                ButtonSave.Text = "Update";
                TextBoxModifiedBy.Text = MasterMechUtil.msUserName;
                
            }
            else if(mnMode == MasterMechUtil.OPMode.Delete)
            {
                ButtonSave.Text = "Delete";
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Do you want to exit?","Exit Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(Result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void TextBoxEmail_Validated(object sender, EventArgs e)
        {
            string email = TextBoxEmail.Text;
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool lbValidEmail = Regex.IsMatch(email, emailPattern);

            if (!lbValidEmail && email.Length > 0)
            {
                LabelEmailValid.ForeColor = Color.Red;
                LabelEmailValid.Text = "Email is Not in Correct Format";
                TextBoxEmail.Text = "";
            }
            else
            {
                LabelEmailValid.Text = "";
            }
        }

        private void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxEmail.Text.Length > 0)
                LabelEmailValid.Text = "";
        }

        private void TextBoxMobile_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void TextBoxMobile_Validated(object sender, EventArgs e)
        {
            string lsMobile = TextBoxMobile.Text;
            bool lbFoundDigit = double.TryParse(lsMobile, out _);

            if (lsMobile.Length > 0)
            {
                if (lsMobile.Length != 10 || !lbFoundDigit)
                {

                    LabelMobileValid.ForeColor = Color.Red;
                    LabelMobileValid.Text = "Invalid Input";
                    TextBoxMobile.Text = "";
                }
                else
                {
                    LabelMobileValid.Text = string.Empty;
                }

            }
            
            
        }

        private void TextBoxUserID_TextChanged(object sender, EventArgs e)
        {
            LabelUserIDValid.Text = string.Empty;
        }

        private void TextBoxPassword_TextChanged(object sender, EventArgs e)
        {
            LabelPassValid.Text = string.Empty;
        }

        private void TextBoxPasswordConfirm_TextChanged(object sender, EventArgs e)
        {
            LabelCnfPassValid.Text = string.Empty;
        }

        private void TextBoxUserName_TextChanged(object sender, EventArgs e)
        {
            LabelUserNameValid.Text = string.Empty;
        }

        private void TextBoxRemarks_TextChanged(object sender, EventArgs e)
        {
            LabelRemarkValid.Text = string.Empty;
        }

        private void TextBoxPasswordConfirm_Validated(object sender, EventArgs e)
        {
            string lsPassword = TextBoxPassword.Text;
            string lsConfirmPassWord = TextBoxPasswordConfirm.Text;

            //Ordinal for Case Sensitive Comparison
            //Ordinal for Case Sensitive Comparison
            bool lbMatch = string.Equals(lsPassword, lsConfirmPassWord, StringComparison.Ordinal);

            if (!lbMatch)
            {
                LabelCnfPassValid.ForeColor = Color.Red;
                TextBoxPasswordConfirm.Text = string.Empty;
                LabelCnfPassValid.Text = "Password and confirm password do not match.";
                
            }
            
        }
    }
}
