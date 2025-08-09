namespace QLDuLieuTonKho_BTP
{
    partial class Uc_ShowData
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbShowData = new System.Windows.Forms.GroupBox();
            this.grDataViewer = new System.Windows.Forms.DataGridView();
            this.grbShowData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grDataViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // grbShowData
            // 
            this.grbShowData.Controls.Add(this.grDataViewer);
            this.grbShowData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbShowData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbShowData.Location = new System.Drawing.Point(0, 0);
            this.grbShowData.Name = "grbShowData";
            this.grbShowData.Padding = new System.Windows.Forms.Padding(10);
            this.grbShowData.Size = new System.Drawing.Size(732, 632);
            this.grbShowData.TabIndex = 0;
            this.grbShowData.TabStop = false;
            this.grbShowData.Text = "Bảng Dữ Liệu";
            // 
            // grDataViewer
            // 
            this.grDataViewer.AllowUserToAddRows = false;
            this.grDataViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grDataViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grDataViewer.Location = new System.Drawing.Point(10, 25);
            this.grDataViewer.Name = "grDataViewer";
            this.grDataViewer.Size = new System.Drawing.Size(712, 597);
            this.grDataViewer.TabIndex = 0;
            // 
            // Uc_ShowData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbShowData);
            this.Name = "Uc_ShowData";
            this.Size = new System.Drawing.Size(732, 632);
            this.grbShowData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grDataViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbShowData;
        private System.Windows.Forms.DataGridView grDataViewer;
    }
}
