namespace QLDuLieuTonKho_BTP
{
    partial class Uc_BoSungKL
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitleForm = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.tbnLuu = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.nmID = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLot = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTenSP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbTenBin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nmTongKL = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nmKLBin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nmKLDong = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmTongKL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmKLBin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmKLDong)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitleForm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 50);
            this.panel1.TabIndex = 0;
            // 
            // lblTitleForm
            // 
            this.lblTitleForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleForm.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleForm.Location = new System.Drawing.Point(0, 0);
            this.lblTitleForm.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitleForm.Name = "lblTitleForm";
            this.lblTitleForm.Size = new System.Drawing.Size(620, 50);
            this.lblTitleForm.TabIndex = 0;
            this.lblTitleForm.Text = "BẢNG BỔ SUNG KHỐI LƯỢNG";
            this.lblTitleForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnXoa);
            this.groupBox1.Controls.Add(this.tbnLuu);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 50);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(620, 646);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bảng Nhập Liệu";
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.Crimson;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.SystemColors.Control;
            this.btnXoa.Location = new System.Drawing.Point(340, 580);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(113, 50);
            this.btnXoa.TabIndex = 8;
            this.btnXoa.Text = "Làm lại";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // tbnLuu
            // 
            this.tbnLuu.BackColor = System.Drawing.Color.SeaGreen;
            this.tbnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnLuu.ForeColor = System.Drawing.SystemColors.Control;
            this.tbnLuu.Location = new System.Drawing.Point(481, 580);
            this.tbnLuu.Name = "tbnLuu";
            this.tbnLuu.Size = new System.Drawing.Size(111, 50);
            this.tbnLuu.TabIndex = 9;
            this.tbnLuu.Text = "Lưu";
            this.tbnLuu.UseVisualStyleBackColor = false;
            this.tbnLuu.Click += new System.EventHandler(this.tbnLuu_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.nmID);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cbLot);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.tbTenSP);
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Controls.Add(this.tbTenBin);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.nmTongKL);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.nmKLBin);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.nmKLDong);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 27);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(607, 541);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 10, 5, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 25);
            this.label6.TabIndex = 10;
            this.label6.Text = "ID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nmID
            // 
            this.nmID.Enabled = false;
            this.nmID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmID.Location = new System.Drawing.Point(0, 45);
            this.nmID.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.nmID.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.nmID.Name = "nmID";
            this.nmID.Size = new System.Drawing.Size(591, 27);
            this.nmID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 10, 5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "LOT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbLot
            // 
            this.cbLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLot.FormattingEnabled = true;
            this.cbLot.Location = new System.Drawing.Point(0, 117);
            this.cbLot.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.cbLot.Name = "cbLot";
            this.cbLot.Size = new System.Drawing.Size(591, 28);
            this.cbLot.TabIndex = 2;
            this.cbLot.TextUpdate += new System.EventHandler(this.cbLot_TextUpdate);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 160);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 10, 5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tên Sản Phẩm";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTenSP
            // 
            this.tbTenSP.Enabled = false;
            this.tbTenSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTenSP.Location = new System.Drawing.Point(0, 195);
            this.tbTenSP.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.tbTenSP.Name = "tbTenSP";
            this.tbTenSP.Size = new System.Drawing.Size(591, 27);
            this.tbTenSP.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 237);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 10, 5, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "Tên Bin";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTenBin
            // 
            this.tbTenBin.Enabled = false;
            this.tbTenBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTenBin.Location = new System.Drawing.Point(0, 272);
            this.tbTenBin.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.tbTenBin.Name = "tbTenBin";
            this.tbTenBin.Size = new System.Drawing.Size(591, 27);
            this.tbTenBin.TabIndex = 4;
            this.tbTenBin.TextChanged += new System.EventHandler(this.tbTenBin_TextChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 314);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 10, 5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tổng khối lượng";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nmTongKL
            // 
            this.nmTongKL.DecimalPlaces = 1;
            this.nmTongKL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmTongKL.Location = new System.Drawing.Point(0, 349);
            this.nmTongKL.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.nmTongKL.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            0});
            this.nmTongKL.Name = "nmTongKL";
            this.nmTongKL.Size = new System.Drawing.Size(591, 27);
            this.nmTongKL.TabIndex = 5;
            this.nmTongKL.ValueChanged += new System.EventHandler(this.nmTongKL_ValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 391);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 10, 5, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Khối lượng Bin";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nmKLBin
            // 
            this.nmKLBin.DecimalPlaces = 1;
            this.nmKLBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmKLBin.Location = new System.Drawing.Point(0, 426);
            this.nmKLBin.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.nmKLBin.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            0});
            this.nmKLBin.Name = "nmKLBin";
            this.nmKLBin.Size = new System.Drawing.Size(591, 27);
            this.nmKLBin.TabIndex = 6;
            this.nmKLBin.ValueChanged += new System.EventHandler(this.nmKLBin_ValueChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 468);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 10, 5, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "Khối lượng Đồng";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nmKLDong
            // 
            this.nmKLDong.DecimalPlaces = 1;
            this.nmKLDong.Enabled = false;
            this.nmKLDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmKLDong.Location = new System.Drawing.Point(0, 503);
            this.nmKLDong.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.nmKLDong.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            0});
            this.nmKLDong.Name = "nmKLDong";
            this.nmKLDong.Size = new System.Drawing.Size(591, 27);
            this.nmKLDong.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblHuongDan
            // 
            this.lblHuongDan.AutoSize = true;
            this.lblHuongDan.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblHuongDan.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHuongDan.Location = new System.Drawing.Point(503, 696);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(117, 18);
            this.lblHuongDan.TabIndex = 2;
            this.lblHuongDan.Text = "Xem Hướng Dẫn";
            this.lblHuongDan.Click += new System.EventHandler(this.lblHuongDan_Click);
            // 
            // Uc_BoSungKL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblHuongDan);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Uc_BoSungKL";
            this.Size = new System.Drawing.Size(620, 755);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmTongKL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmKLBin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmKLDong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitleForm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nmID;
        private System.Windows.Forms.ComboBox cbLot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmTongKL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTenSP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmKLBin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nmKLDong;
        private System.Windows.Forms.Button tbnLuu;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbTenBin;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Label lblHuongDan;
    }
}
