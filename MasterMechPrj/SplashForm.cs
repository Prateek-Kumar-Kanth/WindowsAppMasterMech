using MasterMechPrj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
            CloseTimer = new Timer();
            CloseTimer.Interval = 1000; // Set the interval to 3 seconds (3000 milliseconds)
            CloseTimer.Tick += CloseTimer_Tick;
        }
        public static bool ReadConfiguration()
        {

            if (File.Exists("MasterMechConfiguration.txt"))
            {
                FileStream lObjFS = new FileStream("MasterMechConfiguration.txt", FileMode.Open, FileAccess.Read);
                StreamReader lObjSR = new StreamReader(lObjFS);

                MasterMechUtil.msCompName = lObjSR.ReadLine();
                MasterMechUtil.msStreetAdd = lObjSR.ReadLine();
                MasterMechUtil.msArea = lObjSR.ReadLine();
                MasterMechUtil.msCity = lObjSR.ReadLine();
                MasterMechUtil.msState = lObjSR.ReadLine();
                MasterMechUtil.msPincode = lObjSR.ReadLine();
                MasterMechUtil.msCountry = lObjSR.ReadLine();
                MasterMechUtil.msGSTNo = lObjSR.ReadLine();
                MasterMechUtil.msContact = lObjSR.ReadLine();
                MasterMechUtil.msPAN = lObjSR.ReadLine();
                MasterMechUtil.msTAN = lObjSR.ReadLine();
                MasterMechUtil.msDOEstab = lObjSR.ReadLine();
                MasterMechUtil.msServerName = lObjSR.ReadLine();
                MasterMechUtil.msDatabase = lObjSR.ReadLine();
                MasterMechUtil.msUserID = lObjSR.ReadLine();
                MasterMechUtil.msPassword = lObjSR.ReadLine();
                MasterMechUtil.msConfirmPass = lObjSR.ReadLine();

                Console.ReadLine();
                lObjSR.Close();
                lObjFS.Close();

                return true;
            }
            else
            {
                return false;
            }


        }
        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            CloseTimer.Stop();
            this.Hide();

            if (ReadConfiguration())
            {
                //if file is present that means software is installed
                LoginForm lObj = new LoginForm();
                this.Hide();
                lObj.Show();
            }
            else
            {
                //if file is present that means software is not installed in any company and you must install first
                MasterMechConfigFormForm lObj = new MasterMechConfigFormForm();
                lObj.Show();

            }
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            CloseTimer.Start();
        }
    }
}
