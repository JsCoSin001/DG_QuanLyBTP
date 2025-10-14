namespace QLDuLieuTonKho_BTP
{
    partial class Uc_BcTonKho
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTonKho = new System.Windows.Forms.Button();
            this.pdfRenderer1 = new PdfiumViewer.PdfRenderer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbXuatExcelReport = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.maBin = new System.Windows.Forms.TextBox();
            this.klConLai = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.klBanTran = new System.Windows.Forms.NumericUpDown();
            this.klHienTai = new System.Windows.Forms.NumericUpDown();
            this.btnLuu = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTimIDBen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.klConLai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.klBanTran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.klHienTai)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(440, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh Mục Báo cáo";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnTonKho);
            this.flowLayoutPanel1.Controls.Add(this.pdfRenderer1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 62);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(434, 89);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnTonKho
            // 
            this.btnTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTonKho.Location = new System.Drawing.Point(3, 3);
            this.btnTonKho.Name = "btnTonKho";
            this.btnTonKho.Size = new System.Drawing.Size(161, 45);
            this.btnTonKho.TabIndex = 0;
            this.btnTonKho.Text = "Báo cáo Tồn";
            this.btnTonKho.UseVisualStyleBackColor = true;
            this.btnTonKho.Click += new System.EventHandler(this.btnTonKho_Click);
            // 
            // pdfRenderer1
            // 
            this.pdfRenderer1.Location = new System.Drawing.Point(170, 3);
            this.pdfRenderer1.Name = "pdfRenderer1";
            this.pdfRenderer1.Page = 0;
            this.pdfRenderer1.Rotation = PdfiumViewer.PdfRotation.Rotate0;
            this.pdfRenderer1.Size = new System.Drawing.Size(75, 23);
            this.pdfRenderer1.TabIndex = 1;
            this.pdfRenderer1.Text = "pdfRenderer1";
            this.pdfRenderer1.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitHeight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbXuatExcelReport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 30);
            this.panel1.TabIndex = 0;
            // 
            // cbXuatExcelReport
            // 
            this.cbXuatExcelReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbXuatExcelReport.AutoSize = true;
            this.cbXuatExcelReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbXuatExcelReport.Location = new System.Drawing.Point(309, 3);
            this.cbXuatExcelReport.Name = "cbXuatExcelReport";
            this.cbXuatExcelReport.Size = new System.Drawing.Size(104, 24);
            this.cbXuatExcelReport.TabIndex = 0;
            this.cbXuatExcelReport.Text = "Xuất Excel";
            this.cbXuatExcelReport.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.maBin);
            this.groupBox2.Controls.Add(this.klConLai);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.klBanTran);
            this.groupBox2.Controls.Add(this.klHienTai);
            this.groupBox2.Controls.Add(this.btnLuu);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnTimIDBen);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 437);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cập nhật bán trần";
            // 
            // maBin
            // 
            this.maBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maBin.Location = new System.Drawing.Point(95, 40);
            this.maBin.Name = "maBin";
            this.maBin.Size = new System.Drawing.Size(250, 27);
            this.maBin.TabIndex = 10;
            // 
            // klConLai
            // 
            this.klConLai.DecimalPlaces = 1;
            this.klConLai.Enabled = false;
            this.klConLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.klConLai.Location = new System.Drawing.Point(95, 139);
            this.klConLai.Maximum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            0});
            this.klConLai.Name = "klConLai";
            this.klConLai.Size = new System.Drawing.Size(108, 27);
            this.klConLai.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "KL còn lại";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(221, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "KL bán trần";
            // 
            // klBanTran
            // 
            this.klBanTran.DecimalPlaces = 1;
            this.klBanTran.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.klBanTran.Location = new System.Drawing.Point(318, 94);
            this.klBanTran.Maximum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            0});
            this.klBanTran.Name = "klBanTran";
            this.klBanTran.Size = new System.Drawing.Size(108, 27);
            this.klBanTran.TabIndex = 6;
            this.klBanTran.ValueChanged += new System.EventHandler(this.klBanTran_ValueChanged);
            this.klBanTran.KeyDown += new System.Windows.Forms.KeyEventHandler(this.klBanTran_KeyDown);
            // 
            // klHienTai
            // 
            this.klHienTai.DecimalPlaces = 1;
            this.klHienTai.Enabled = false;
            this.klHienTai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.klHienTai.Location = new System.Drawing.Point(95, 94);
            this.klHienTai.Maximum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            0});
            this.klHienTai.Name = "klHienTai";
            this.klHienTai.Size = new System.Drawing.Size(108, 27);
            this.klHienTai.TabIndex = 6;
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(225, 133);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 36);
            this.btnLuu.TabIndex = 5;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "KL hiện tại";
            // 
            // btnTimIDBen
            // 
            this.btnTimIDBen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimIDBen.Location = new System.Drawing.Point(351, 36);
            this.btnTimIDBen.Name = "btnTimIDBen";
            this.btnTimIDBen.Size = new System.Drawing.Size(75, 36);
            this.btnTimIDBen.TabIndex = 2;
            this.btnTimIDBen.Text = "Tìm";
            this.btnTimIDBen.UseVisualStyleBackColor = true;
            this.btnTimIDBen.Click += new System.EventHandler(this.btnTimIDBen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã bin";
            // 
            // Uc_BcTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Uc_BcTonKho";
            this.Size = new System.Drawing.Size(440, 591);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.klConLai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.klBanTran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.klHienTai)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnTonKho;
        private System.Windows.Forms.CheckBox cbXuatExcelReport;
        private PdfiumViewer.PdfRenderer pdfRenderer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTimIDBen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown klHienTai;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown klConLai;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown klBanTran;
        private System.Windows.Forms.TextBox maBin;
    }
}
