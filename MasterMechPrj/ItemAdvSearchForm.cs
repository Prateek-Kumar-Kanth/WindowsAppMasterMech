using MainForm;
using MasterMechData;
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
    public partial class ItemAdvSearchForm : Form
    {
        ItemForm mObjItemForm;
        public ItemAdvSearchForm(ItemForm iObjItem)
        {
            InitializeComponent();
            mObjItemForm = iObjItem;
        }

        private void ItemAdvSearchForm_Load(object sender, EventArgs e)
        {

        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            string lsItemDesc = TextBoxItemDesc.Text;
            string lsItemType = ComboBoxType.Text;
            string lsItemCatg = ComboBoxCat.Text;

            Items lObj = new Items();
            List<Items> ListItemData = lObj.AdvanceSearch(lsItemDesc, lsItemType, lsItemCatg);

            if (ListItemData.Count > 0)
            {
                
                this.Hide();
                mObjItemForm.ItemData(ListItemData);

            }
            else
            {
                MessageBox.Show("No Match Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
