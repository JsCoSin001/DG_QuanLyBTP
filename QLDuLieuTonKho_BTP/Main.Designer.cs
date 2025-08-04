namespace QLDuLieuTonKho_BTP
{
    partial class Main
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
            this.mySign = new System.Windows.Forms.Label();
            this.pnMainControl = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTimDB = new System.Windows.Forms.Button();
            this.UrlDb = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCapNhatMaSP = new System.Windows.Forms.Button();
            this.dsChucNang = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBen = new System.Windows.Forms.Button();
            this.btnBocMach = new System.Windows.Forms.Button();
            this.btnQuanMica = new System.Windows.Forms.Button();
            this.btnBocVo = new System.Windows.Forms.Button();
            this.pnLogo = new System.Windows.Forms.Panel();
            this.pnRight = new System.Windows.Forms.Panel();
            this.pnLeft = new System.Windows.Forms.Panel();
            this.pnMainControl.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.dsChucNang.SuspendLayout();
            this.SuspendLayout();
            // 
            // mySign
            // 
            this.mySign.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mySign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mySign.Location = new System.Drawing.Point(10, 740);
            this.mySign.Name = "mySign";
            this.mySign.Size = new System.Drawing.Size(202, 25);
            this.mySign.TabIndex = 0;
            this.mySign.Text = "label1";
            this.mySign.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // pnMainControl
            // 
            this.pnMainControl.Controls.Add(this.panel3);
            this.pnMainControl.Controls.Add(this.dsChucNang);
            this.pnMainControl.Controls.Add(this.pnLogo);
            this.pnMainControl.Controls.Add(this.mySign);
            this.pnMainControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnMainControl.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnMainControl.Location = new System.Drawing.Point(0, 0);
            this.pnMainControl.Name = "pnMainControl";
            this.pnMainControl.Padding = new System.Windows.Forms.Padding(10);
            this.pnMainControl.Size = new System.Drawing.Size(222, 775);
            this.pnMainControl.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 375);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(202, 361);
            this.panel3.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTimDB);
            this.groupBox1.Controls.Add(this.UrlDb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(202, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bảng Cấu Hình";
            // 
            // btnTimDB
            // 
            this.btnTimDB.Location = new System.Drawing.Point(95, 71);
            this.btnTimDB.Name = "btnTimDB";
            this.btnTimDB.Size = new System.Drawing.Size(90, 35);
            this.btnTimDB.TabIndex = 1;
            this.btnTimDB.Text = "Tìm DB";
            this.btnTimDB.UseVisualStyleBackColor = true;
            this.btnTimDB.Click += new System.EventHandler(this.btnTimDB_Click);
            // 
            // UrlDb
            // 
            this.UrlDb.Dock = System.Windows.Forms.DockStyle.Top;
            this.UrlDb.Location = new System.Drawing.Point(3, 26);
            this.UrlDb.Name = "UrlDb";
            this.UrlDb.Size = new System.Drawing.Size(196, 33);
            this.UrlDb.TabIndex = 0;
            this.UrlDb.Text = "label2";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnCapNhatMaSP);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(202, 133);
            this.panel4.TabIndex = 2;
            // 
            // btnCapNhatMaSP
            // 
            this.btnCapNhatMaSP.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCapNhatMaSP.Location = new System.Drawing.Point(0, 0);
            this.btnCapNhatMaSP.Name = "btnCapNhatMaSP";
            this.btnCapNhatMaSP.Size = new System.Drawing.Size(202, 53);
            this.btnCapNhatMaSP.TabIndex = 0;
            this.btnCapNhatMaSP.Text = "Cập Nhật Mã Hàng";
            this.btnCapNhatMaSP.UseVisualStyleBackColor = true;
            this.btnCapNhatMaSP.Click += new System.EventHandler(this.btnCapNhatMaSP_Click);
            // 
            // dsChucNang
            // 
            this.dsChucNang.Controls.Add(this.btnBen);
            this.dsChucNang.Controls.Add(this.btnBocMach);
            this.dsChucNang.Controls.Add(this.btnQuanMica);
            this.dsChucNang.Controls.Add(this.btnBocVo);
            this.dsChucNang.Dock = System.Windows.Forms.DockStyle.Top;
            this.dsChucNang.Location = new System.Drawing.Point(10, 113);
            this.dsChucNang.Name = "dsChucNang";
            this.dsChucNang.Size = new System.Drawing.Size(202, 262);
            this.dsChucNang.TabIndex = 3;
            // 
            // btnBen
            // 
            this.btnBen.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBen.Location = new System.Drawing.Point(0, 0);
            this.btnBen.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnBen.Name = "btnBen";
            this.btnBen.Size = new System.Drawing.Size(199, 49);
            this.btnBen.TabIndex = 0;
            this.btnBen.Text = "Công Đoạn Bện";
            this.btnBen.UseVisualStyleBackColor = true;
            this.btnBen.Click += new System.EventHandler(this.btnBen_Click);
            // 
            // btnBocMach
            // 
            this.btnBocMach.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBocMach.Location = new System.Drawing.Point(0, 59);
            this.btnBocMach.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnBocMach.Name = "btnBocMach";
            this.btnBocMach.Size = new System.Drawing.Size(199, 49);
            this.btnBocMach.TabIndex = 1;
            this.btnBocMach.Text = "Công Đoạn Bọc Mạch";
            this.btnBocMach.UseVisualStyleBackColor = true;
            this.btnBocMach.Click += new System.EventHandler(this.btnBocMach_Click);
            // 
            // btnQuanMica
            // 
            this.btnQuanMica.AutoSize = true;
            this.btnQuanMica.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanMica.Location = new System.Drawing.Point(0, 118);
            this.btnQuanMica.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnQuanMica.Name = "btnQuanMica";
            this.btnQuanMica.Size = new System.Drawing.Size(199, 49);
            this.btnQuanMica.TabIndex = 2;
            this.btnQuanMica.Text = "Công Đoạn Quấn Mica";
            this.btnQuanMica.UseVisualStyleBackColor = true;
            this.btnQuanMica.Click += new System.EventHandler(this.btnQuanMica_Click);
            // 
            // btnBocVo
            // 
            this.btnBocVo.AutoSize = true;
            this.btnBocVo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBocVo.Location = new System.Drawing.Point(0, 177);
            this.btnBocVo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnBocVo.Name = "btnBocVo";
            this.btnBocVo.Size = new System.Drawing.Size(199, 49);
            this.btnBocVo.TabIndex = 3;
            this.btnBocVo.Text = "Công Đoạn Bọc Vỏ";
            this.btnBocVo.UseVisualStyleBackColor = true;
            this.btnBocVo.Click += new System.EventHandler(this.btnBocVo_Click);
            // 
            // pnLogo
            // 
            this.pnLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnLogo.Location = new System.Drawing.Point(10, 10);
            this.pnLogo.Name = "pnLogo";
            this.pnLogo.Size = new System.Drawing.Size(202, 103);
            this.pnLogo.TabIndex = 1;
            // 
            // pnRight
            // 
            this.pnRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRight.Location = new System.Drawing.Point(414, 0);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(729, 775);
            this.pnRight.TabIndex = 2;
            // 
            // pnLeft
            // 
            this.pnLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnLeft.Location = new System.Drawing.Point(222, 0);
            this.pnLeft.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnLeft.Size = new System.Drawing.Size(192, 775);
            this.pnLeft.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 775);
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.pnLeft);
            this.Controls.Add(this.pnMainControl);
            this.Name = "Main";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.pnMainControl.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.dsChucNang.ResumeLayout(false);
            this.dsChucNang.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label mySign;
        private System.Windows.Forms.Panel pnMainControl;
        private System.Windows.Forms.FlowLayoutPanel dsChucNang;
        private System.Windows.Forms.Button btnBen;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnLogo;
        private System.Windows.Forms.Button btnBocMach;
        private System.Windows.Forms.Button btnQuanMica;
        private System.Windows.Forms.Button btnBocVo;
        private System.Windows.Forms.Button btnCapNhatMaSP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTimDB;
        private System.Windows.Forms.Label UrlDb;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnLeft;
    }
}