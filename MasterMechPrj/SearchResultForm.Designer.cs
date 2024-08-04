namespace MasterMechPrj
{
    partial class SearchResultForm
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
            this.DataGridResult = new System.Windows.Forms.DataGridView();
            this.ButtonSelect = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridResult)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridResult
            // 
            this.DataGridResult.AllowUserToAddRows = false;
            this.DataGridResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridResult.Location = new System.Drawing.Point(12, 12);
            this.DataGridResult.Name = "DataGridResult";
            this.DataGridResult.RowHeadersWidth = 51;
            this.DataGridResult.RowTemplate.Height = 24;
            this.DataGridResult.Size = new System.Drawing.Size(1138, 321);
            this.DataGridResult.TabIndex = 0;
            this.DataGridResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridResult_CellClick);
            // 
            // ButtonSelect
            // 
            this.ButtonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSelect.Location = new System.Drawing.Point(925, 414);
            this.ButtonSelect.Name = "ButtonSelect";
            this.ButtonSelect.Size = new System.Drawing.Size(75, 23);
            this.ButtonSelect.TabIndex = 1;
            this.ButtonSelect.Text = "Select";
            this.ButtonSelect.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(1071, 414);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // SearchResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 462);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSelect);
            this.Controls.Add(this.DataGridResult);
            this.Name = "SearchResultForm";
            this.Text = "SearchResultForm";
            this.Load += new System.EventHandler(this.SearchResultForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ButtonSelect;
        private System.Windows.Forms.Button ButtonCancel;
        public System.Windows.Forms.DataGridView DataGridResult;
    }
}