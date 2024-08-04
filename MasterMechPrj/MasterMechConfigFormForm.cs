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
    public partial class MasterMechConfigFormForm : Form
    {
        MasterMechUtil.OPMode mMode;
        public MasterMechConfigFormForm()
        {
            InitializeComponent();
        }
        public MasterMechConfigFormForm(MasterMechUtil.OPMode iMode)
        {
            InitializeComponent();
            mMode = iMode;
        }

        private void MasterMechConfigFormForm_Load(object sender, EventArgs e)
        {
            if(mMode == MasterMechUtil.OPMode.Open)
            {
                TextBoxCompName.Text = MasterMechUtil.msCompName;
                TextBoxStreet.Text = MasterMechUtil.msStreetAdd;
                TextBoxArea.Text = MasterMechUtil.msArea;
                TextBoxCity.Text = MasterMechUtil.msCity;
                TextBoxState.Text = MasterMechUtil.msState;
                TextBoxPincode.Text = MasterMechUtil.msPincode;
                TextBoxCountry.Text = MasterMechUtil.msCountry;
                TextBoxGST.Text = MasterMechUtil.msGSTNo;
                TextBoxContact.Text = MasterMechUtil.msContact;
                TextBoxPAN.Text = MasterMechUtil.msPAN;
                TextBoxTAN.Text = MasterMechUtil.msTAN;
                DTDOEstab.Text = MasterMechUtil.msDOEstab;
                TextBoxServer.Text = MasterMechUtil.msServerName;
                TextBoxDataBase.Text = MasterMechUtil.msDatabase;
                TextBoxUserID.Text = MasterMechUtil.msUserID;
                TextBoxPassword.Text = MasterMechUtil.msPassword;
                TextBoxConfirmPass.Text = MasterMechUtil.msConfirmPass;
            }
        }
    }
}
