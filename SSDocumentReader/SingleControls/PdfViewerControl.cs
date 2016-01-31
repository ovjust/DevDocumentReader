using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPdfViewer;
using System.IO;

namespace SSDocumentReader.SingleControls
{
    public partial class PdfViewerControl : DocViewerControlBase
    {
        public PdfViewerControl()
        {
            InitializeComponent();
        }

        public override void ShowDoc(string path, int? page = null)
        {
            var pdfStream = File.OpenRead(path);
            pdfViewer.LoadDocument(pdfStream);
            if (page != null)
                pdfViewer.CurrentPageNumber = page.Value;
        }

        private void PdfViewerControl_Load(object sender, EventArgs e)
        {
            pdfViewer.DocumentChanged += new PdfDocumentChangedEventHandler(OnPdfViewerDocumentChanged);
            pdfViewer.CurrentPageChanged += pdfViewer_CurrentPageChanged;
        }

        void pdfViewer_CurrentPageChanged(object sender, PdfCurrentPageChangedEventArgs e)
        {
            barPage1.Caption = pdfViewer.CurrentPageNumber.ToString("N0");
        }

        private void OnPdfViewerDocumentChanged(object sender, PdfDocumentChangedEventArgs e)
        {
            barPageAll.Caption = pdfViewer.PageCount.ToString("N0");
        }
    }
}
