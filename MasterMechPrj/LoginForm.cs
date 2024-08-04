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
namespace MasterMechPrj
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            string lsUserID = TextBoxUserID.Text;
            string lsPassword = TextBoxPassword.Text;
            string lsFY = ComboBoxFY.Text;

            User lObjUser = new User();

            lObjUser.ReadLoginData(lsUserID, lsPassword);

            if (!string.IsNullOrEmpty(MasterMechUtil.msUserType))
            {
                MasterMechUtil.sFY = ComboBoxFY.Text;
                
                MainForm.MainForm lObj = new MainForm.MainForm(MasterMechUtil.msUserType);
                Hide();
                lObj.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid User Name Or PassWord", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
