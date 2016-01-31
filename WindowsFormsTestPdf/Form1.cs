using DevExpress.LookAndFeel;
using DevExpress.Pdf;
using DevExpress.XtraPdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsTestPdf
{
    public partial class Form1 : Form
    {
        PdfViewer pdfViewer = new PdfViewer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Controls.Add(pdfViewer);
            pdfViewer.Dock = DockStyle.Fill;

            pdfViewer.DocumentCreator = "PDF Viewer Demo";
            pdfViewer.DocumentProducer = "Developer Express Inc., " + AssemblyInfo.Version;
            UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            pdfViewer.CreateRibbon();

            pdfViewer.DocumentChanged += new PdfDocumentChangedEventHandler(OnPdfViewerDocumentChanged);
            pdfViewer.CurrentPageChanged += pdfViewer_CurrentPageChanged;
            using (Stream pdfStream = GetDocumentStream())
                pdfViewer.LoadDocument(pdfStream);
            //new LimitationsForm(pdfViewer);
        }

        void pdfViewer_CurrentPageChanged(object sender, PdfCurrentPageChangedEventArgs e)
        {
            
        }

        static Stream GetDocumentStream()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("Demo.pdf");
            if (stream == null)
                return assembly.GetManifestResourceStream("Data.Demo.pdf");
            return stream;
        }

        void OnPdfViewerDocumentChanged(object sender, PdfDocumentChangedEventArgs e)
        {
            string fileName = Path.GetFileName(e.DocumentFilePath);
            var mainFormText = "pdf1";
            if (String.IsNullOrEmpty(fileName))
                Text = mainFormText;
            else
                Text = fileName + " - " + mainFormText;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var pdfStream = File.OpenRead(@"E:\backup\我的文档\IMG_20150524_0003.pdf");
            pdfViewer.LoadDocument(pdfStream);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           var text=(string)  barEditItem1.EditValue;
            PdfTextSearchParameters searchParam=new PdfTextSearchParameters();
            searchParam.CaseSensitive=false;
           var results= pdfViewer.FindText(text, searchParam);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pdfViewer.CurrentPageNumber =Convert.ToInt32( barEditItem3.EditValue);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var pdfViewer1 = new PdfViewer();
            var pdfStream = File.OpenRead(@"C:\Users\Public\Documents\DevExpress Demos 14.1\Components\WinForms\CS\PdfViewerDemo\Data\Demo.pdf");
            pdfViewer1.LoadDocument(pdfStream);

            var text = (string)barEditItem1.EditValue;
            PdfTextSearchParameters searchParam = new PdfTextSearchParameters();
            searchParam.CaseSensitive = false;
            searchParam.Direction = PdfTextSearchDirection.Forward;
            PdfTextSearchResults results = pdfViewer1.FindText(text, searchParam);
            List<int> findPages = new List<int>();

            while (results.Words !=null )
            {
                findPages.Add(results.PageNumber);
                results = pdfViewer1.FindText(text, searchParam);
            }
        }
    }
}
