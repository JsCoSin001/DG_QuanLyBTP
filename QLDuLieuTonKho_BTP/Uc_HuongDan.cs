using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDuLieuTonKho_BTP
{
    public partial class Uc_HuongDan : UserControl
    {
        private readonly string[] _pdfNames; // danh sách TÊN file (không phải full path)
        private string DataDir => Path.Combine(Application.StartupPath, "data");

        public Uc_HuongDan(string[] pdfNames)
        {
            InitializeComponent();

            _pdfNames = pdfNames ?? Array.Empty<string>();

            // Cấu hình flowLayoutPanel1 (đã có từ Designer)
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = false;   // xếp ngang 1 hàng
            flowLayoutPanel1.AutoScroll = true;    // có scroll (ngang)
            flowLayoutPanel1.Padding = new Padding(0);
            flowLayoutPanel1.Margin = new Padding(0);

            // Sự kiện
            this.Load += Uc_HuongDan_Load;
            this.Resize += (_, __) => ResizeChildrenToFullWidth();
        }

        private void Uc_HuongDan_Load(object sender, EventArgs e)
        {
            PopulateViewers();
        }

        private void PopulateViewers()
        {
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            // Ghép tên -> full path, lọc file tồn tại
            var files = _pdfNames
                .Select(name => Path.Combine(DataDir, name))
                .Where(File.Exists)
                .ToArray();

            if (files.Length == 0)
            {
                flowLayoutPanel1.Controls.Add(new Label
                {
                    Text = "Không tìm thấy file PDF hợp lệ trong thư mục data.",
                    AutoSize = true,
                    Padding = new Padding(8)
                });
                flowLayoutPanel1.ResumeLayout();
                return;
            }

            foreach (var path in files)
            {
                var viewer = CreatePdfViewer(path);

                // mỗi viewer chiếm 100% bề rộng container
                viewer.Margin = new Padding(0);
                viewer.Width = flowLayoutPanel1.ClientSize.Width;
                viewer.Height = flowLayoutPanel1.ClientSize.Height;

                flowLayoutPanel1.Controls.Add(viewer);
            }

            flowLayoutPanel1.ResumeLayout();
        }

        private Control CreatePdfViewer(string pdfPath)
        {
            var doc = PdfiumViewer.PdfDocument.Load(pdfPath);

            var viewer = new PdfiumViewer.PdfViewer
            {
                Document = doc,
                ZoomMode = PdfViewerZoomMode.FitWidth,
                Dock = DockStyle.None,
                ShowToolbar = false
            };

            // Giải phóng document khi viewer bị dispose
            viewer.Disposed += (_, __) =>
            {
                try { doc.Dispose(); } catch { /* ignore */ }
            };

            // Tuỳ chọn: chặn menu chuột phải nếu muốn đơn giản giao diện
            viewer.ContextMenuStrip = new ContextMenuStrip(); // rỗng

            return viewer;
        }

        private void ResizeChildrenToFullWidth()
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                c.Width = flowLayoutPanel1.ClientSize.Width;
                c.Height = flowLayoutPanel1.ClientSize.Height;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Không cần xử lý
        }
    }
}
