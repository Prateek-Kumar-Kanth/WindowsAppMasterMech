namespace MasterMechPrj
{
    partial class CustAdvanceSearchForm
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
            this.TextBoxCity = new System.Windows.Forms.TextBox();
            this.TextBoxLName = new System.Windows.Forms.TextBox();
            this.TextBoxFName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(388, 178);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 28);
            this.ButtonCancel.TabIndex = 15;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(286, 178);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 28);
            this.ButtonOk.TabIndex = 14;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextBoxCity
            // 
            this.TextBoxCity.Location = new System.Drawing.Point(243, 127);
            this.TextBoxCity.Name = "TextBoxCity";
            this.TextBoxCity.Size = new System.Drawing.Size(220, 22);
            this.TextBoxCity.TabIndex = 13;
            // 
            // TextBoxLName
            // 
            this.TextBoxLName.Location = new System.Drawing.Point(243, 86);
            this.TextBoxLName.Name = "TextBoxLName";
            this.TextBoxLName.Size = new System.Drawing.Size(220, 22);
            this.TextBoxLName.TabIndex = 12;
            // 
            // TextBoxFName
            // 
            this.TextBoxFName.Location = new System.Drawing.Point(243, 42);
            this.TextBoxFName.Name = "TextBoxFName";
            this.TextBoxFName.Size = new System.Drawing.Size(220, 22);
            this.TextBoxFName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "City";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Last Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "First Name";
            // 
            // CustAdvanceSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 228);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.TextBoxCity);
            this.Controls.Add(this.TextBoxLName);
            this.Controls.Add(this.TextBoxFName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CustAdvanceSearchForm";
            this.Text = "Customer Advance SearchForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.TextBox TextBoxCity;
        private System.Windows.Forms.TextBox TextBoxLName;
        private System.Windows.Forms.TextBox TextBoxFName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}