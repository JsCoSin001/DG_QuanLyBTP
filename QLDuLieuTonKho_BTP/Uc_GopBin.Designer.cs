namespace QLDuLieuTonKho_BTP
{
    partial class Uc_GopBin
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLamLai = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.may = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.maHT = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.STTCD = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.sttBin = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.soBin = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLot = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbDsLot = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maHT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sttBin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soBin)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(537, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "BÁO CÁO GỘP BIN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 10, 3, 10);
            this.groupBox1.Size = new System.Drawing.Size(537, 300);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bảng Nhập Liệu";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLuu);
            this.panel1.Controls.Add(this.btnLamLai);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 203);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 79);
            this.panel1.TabIndex = 6;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(411, 15);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(106, 50);
            this.btnLuu.TabIndex = 4;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnLamLai
            // 
            this.btnLamLai.Location = new System.Drawing.Point(294, 15);
            this.btnLamLai.Name = "btnLamLai";
            this.btnLamLai.Size = new System.Drawing.Size(100, 50);
            this.btnLamLai.TabIndex = 5;
            this.btnLamLai.Text = "Làm lại";
            this.btnLamLai.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.cbLot);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(529, 176);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "LOT Mới";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.may);
            this.flowLayoutPanel2.Controls.Add(this.label22);
            this.flowLayoutPanel2.Controls.Add(this.maHT);
            this.flowLayoutPanel2.Controls.Add(this.label20);
            this.flowLayoutPanel2.Controls.Add(this.STTCD);
            this.flowLayoutPanel2.Controls.Add(this.label14);
            this.flowLayoutPanel2.Controls.Add(this.sttBin);
            this.flowLayoutPanel2.Controls.Add(this.label21);
            this.flowLayoutPanel2.Controls.Add(this.soBin);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 38);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(520, 40);
            this.flowLayoutPanel2.TabIndex = 22;
            // 
            // may
            // 
            this.may.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.may.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.may.FormattingEnabled = true;
            this.may.Items.AddRange(new object[] {
            "B1",
            "B2",
            "B3",
            "B4",
            "B5",
            "B6",
            "B7",
            "B8",
            "B9",
            "B10",
            "B11",
            "B12",
            "B13",
            "B14",
            "B15",
            "B16",
            "R6",
            "R10",
            "R12"});
            this.may.Location = new System.Drawing.Point(3, 3);
            this.may.Name = "may";
            this.may.Size = new System.Drawing.Size(70, 28);
            this.may.TabIndex = 17;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(79, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(20, 25);
            this.label22.TabIndex = 14;
            this.label22.Text = "-";
            // 
            // maHT
            // 
            this.maHT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maHT.Location = new System.Drawing.Point(105, 3);
            this.maHT.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.maHT.Name = "maHT";
            this.maHT.Size = new System.Drawing.Size(99, 27);
            this.maHT.TabIndex = 19;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(210, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 25);
            this.label20.TabIndex = 13;
            this.label20.Text = "/";
            // 
            // STTCD
            // 
            this.STTCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.STTCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STTCD.FormattingEnabled = true;
            this.STTCD.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.STTCD.Location = new System.Drawing.Point(235, 3);
            this.STTCD.Name = "STTCD";
            this.STTCD.Size = new System.Drawing.Size(49, 28);
            this.STTCD.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(290, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 25);
            this.label14.TabIndex = 16;
            this.label14.Text = "-";
            // 
            // sttBin
            // 
            this.sttBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttBin.Location = new System.Drawing.Point(316, 3);
            this.sttBin.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.sttBin.Name = "sttBin";
            this.sttBin.Size = new System.Drawing.Size(85, 27);
            this.sttBin.TabIndex = 20;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(407, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(20, 25);
            this.label21.TabIndex = 15;
            this.label21.Text = "-";
            // 
            // soBin
            // 
            this.soBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soBin.Location = new System.Drawing.Point(433, 3);
            this.soBin.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.soBin.Name = "soBin";
            this.soBin.Size = new System.Drawing.Size(81, 27);
            this.soBin.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 15, 3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "LOT được gộp";
            // 
            // cbLot
            // 
            this.cbLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLot.FormattingEnabled = true;
            this.cbLot.Location = new System.Drawing.Point(3, 131);
            this.cbLot.Margin = new System.Windows.Forms.Padding(3, 10, 3, 7);
            this.cbLot.Name = "cbLot";
            this.cbLot.Size = new System.Drawing.Size(514, 28);
            this.cbLot.TabIndex = 3;
            this.cbLot.TextUpdate += new System.EventHandler(this.cbLot_TextUpdate);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbDsLot);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 502);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách LOT gộp";
            // 
            // lbDsLot
            // 
            this.lbDsLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDsLot.FormattingEnabled = true;
            this.lbDsLot.ItemHeight = 16;
            this.lbDsLot.Location = new System.Drawing.Point(3, 18);
            this.lbDsLot.Name = "lbDsLot";
            this.lbDsLot.Size = new System.Drawing.Size(531, 481);
            this.lbDsLot.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Crimson;
            this.label4.Location = new System.Drawing.Point(0, 797);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(537, 39);
            this.label4.TabIndex = 3;
            this.label4.Text = "Khối lượng của các Lot trong bảng trên sẽ được gán bằng 0 khi được lưu.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Uc_GopBin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Uc_GopBin";
            this.Size = new System.Drawing.Size(537, 836);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maHT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sttBin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soBin)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLot;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLamLai;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbDsLot;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ComboBox may;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown maHT;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox STTCD;
        private System.Windows.Forms.NumericUpDown soBin;
        private System.Windows.Forms.NumericUpDown sttBin;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Timer timer1;
    }
}
