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
    public partial class Uc_ShowData : UserControl
    {
        private PdfDocument _currentDoc; // tài liệu hiện tại

        public Uc_ShowData()
        {
            InitializeComponent();
            ShowHideController();
        }

        public void LoadPdf(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Đường dẫn rỗng.", nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Không tìm thấy file PDF.", filePath);

            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadPdf(filePath)));
                return;
            }

            // dọn tài liệu cũ trước khi nạp tài liệu mới
            DisposePdf();

            _currentDoc = PdfDocument.Load(filePath);
            pdfViewer1.Document = _currentDoc;

            // tuỳ chọn
            // pdfViewer1.ZoomMode = PdfViewerZoomMode.FitWidth;
            pdfViewer1.ZoomMode = PdfViewerZoomMode.FitWidth;
        }

        public void ClearPdf()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ClearPdf));
                return;
            }

            pdfViewer1.Document = null;
            DisposePdf();
        }

        /// <summary>
        /// Dọn dẹp tài nguyên PDF (an toàn gọi nhiều lần).
        /// </summary>
        public void DisposePdf()
        {
            try
            {
                if (_currentDoc != null)
                {
                    _currentDoc.Dispose();
                    _currentDoc = null;
                }
            }
            catch
            {
                // bỏ qua lỗi dispose
            }
        }


        public void ShowHideController(bool flg = false)
        {
            grbShowData.Visible=flg;
            grbShowData.Dock = flg ? DockStyle.Fill : DockStyle.Right;

            pdfViewer1.Visible= !flg;
            pdfViewer1.Dock = !flg ? DockStyle.Fill :DockStyle.Left;

        }


        public void SetData(DataTable dt)
        {
            grDataViewer.DataSource = dt;

            ShowHideController(true);
        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
