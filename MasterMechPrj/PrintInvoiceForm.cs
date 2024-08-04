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
    public partial class PrintInvoiceForm : Form
    {
        public PrintInvoiceForm()
        {
            InitializeComponent();
        }

        private void PrintInvoiceForm_Load(object sender, EventArgs e)
        {
            WebPrint.Navigate("D:\\Invoice.html");
            
    }

        private void button1_Click(object sender, EventArgs e)
        {
            WebPrint.Print();
        }
    }
    }
