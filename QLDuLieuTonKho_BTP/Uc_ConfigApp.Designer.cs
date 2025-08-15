namespace QLDuLieuTonKho_BTP
{
    partial class Uc_ConfigApp
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbPathDB = new System.Windows.Forms.TextBox();
            this.btnFindPathDB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbQuyenUser = new System.Windows.Forms.TextBox();
            this.btnPhanQuyen = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(719, 422);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cấu hình";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.tbPathDB);
            this.flowLayoutPanel1.Controls.Add(this.btnFindPathDB);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.tbQuyenUser);
            this.flowLayoutPanel1.Controls.Add(this.btnPhanQuyen);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(699, 122);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(132, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "DB Path";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tbPathDB
            // 
            this.tbPathDB.Location = new System.Drawing.Point(141, 7);
            this.tbPathDB.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.tbPathDB.Name = "tbPathDB";
            this.tbPathDB.Size = new System.Drawing.Size(426, 26);
            this.tbPathDB.TabIndex = 1;
            // 
            // btnFindPathDB
            // 
            this.btnFindPathDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindPathDB.Location = new System.Drawing.Point(573, 3);
            this.btnFindPathDB.Name = "btnFindPathDB";
            this.btnFindPathDB.Size = new System.Drawing.Size(105, 36);
            this.btnFindPathDB.TabIndex = 2;
            this.btnFindPathDB.Text = "Chọn";
            this.btnFindPathDB.UseVisualStyleBackColor = true;
            this.btnFindPathDB.Click += new System.EventHandler(this.btnFindPathDB_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(132, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "Setting Config";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tbQuyenUser
            // 
            this.tbQuyenUser.Location = new System.Drawing.Point(141, 49);
            this.tbQuyenUser.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.tbQuyenUser.Name = "tbQuyenUser";
            this.tbQuyenUser.Size = new System.Drawing.Size(426, 26);
            this.tbQuyenUser.TabIndex = 3;
            this.tbQuyenUser.UseSystemPasswordChar = true;
            // 
            // btnPhanQuyen
            // 
            this.btnPhanQuyen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhanQuyen.Location = new System.Drawing.Point(573, 45);
            this.btnPhanQuyen.Name = "btnPhanQuyen";
            this.btnPhanQuyen.Size = new System.Drawing.Size(105, 36);
            this.btnPhanQuyen.TabIndex = 4;
            this.btnPhanQuyen.Text = "Lưu";
            this.btnPhanQuyen.UseVisualStyleBackColor = true;
            this.btnPhanQuyen.Click += new System.EventHandler(this.btnPhanQuyen_Click);
            // 
            // Uc_ConfigApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Name = "Uc_ConfigApp";
            this.Size = new System.Drawing.Size(719, 422);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPathDB;
        private System.Windows.Forms.Button btnFindPathDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbQuyenUser;
        private System.Windows.Forms.Button btnPhanQuyen;
    }
}
