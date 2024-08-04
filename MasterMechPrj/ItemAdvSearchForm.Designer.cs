namespace MasterMechPrj
{
    partial class ItemAdvSearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.TextBoxItemDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboBoxType = new System.Windows.Forms.ComboBox();
            this.ComboBoxCat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(386, 194);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 28);
            this.ButtonCancel.TabIndex = 23;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(282, 194);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 28);
            this.ButtonOk.TabIndex = 22;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // TextBoxItemDesc
            // 
            this.TextBoxItemDesc.Location = new System.Drawing.Point(244, 41);
            this.TextBoxItemDesc.Name = "TextBoxItemDesc";
            this.TextBoxItemDesc.Size = new System.Drawing.Size(217, 22);
            this.TextBoxItemDesc.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Item Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Item Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Item Description";
            // 
            // ComboBoxType
            // 
            this.ComboBoxType.FormattingEnabled = true;
            this.ComboBoxType.Items.AddRange(new object[] {
            "Parts",
            "Labour"});
            this.ComboBoxType.Location = new System.Drawing.Point(244, 82);
            this.ComboBoxType.Name = "ComboBoxType";
            this.ComboBoxType.Size = new System.Drawing.Size(217, 24);
            this.ComboBoxType.TabIndex = 24;
            // 
            // ComboBoxCat
            // 
            this.ComboBoxCat.FormattingEnabled = true;
            this.ComboBoxCat.Items.AddRange(new object[] {
            "ENGIN",
            "BODY",
            "DRIVE",
            "ELECTRIC",
            "FRAME",
            "SUSPENSION",
            "BRAKE"});
            this.ComboBoxCat.Location = new System.Drawing.Point(244, 124);
            this.ComboBoxCat.Name = "ComboBoxCat";
            this.ComboBoxCat.Size = new System.Drawing.Size(217, 24);
            this.ComboBoxCat.TabIndex = 25;
            // 
            // ItemAdvSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 249);
            this.Controls.Add(this.ComboBoxCat);
            this.Controls.Add(this.ComboBoxType);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.TextBoxItemDesc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ItemAdvSearchForm";
            this.Text = "ItemAdvSearchForm";
            this.Load += new System.EventHandler(this.ItemAdvSearchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.TextBox TextBoxItemDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboBoxType;
        private System.Windows.Forms.ComboBox ComboBoxCat;
    }
}