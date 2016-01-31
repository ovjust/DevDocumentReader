using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraRichEdit;

namespace SSDocumentReader.SingleControls
{
    public partial class RichViewerControl : DocViewerControlBase
    {
        public RichViewerControl()
        {
            InitializeComponent();
        }

        private void RichViewerControl_Load(object sender, EventArgs e)
        {
            richEditControl1.Paint += richEditControl1_Paint;
        }

        void richEditControl1_Paint(object sender, PaintEventArgs e)
        {
            //barProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            UpdateViewProcess();

            //PageBasedRichEditView currentView = richEditControl1.ActiveView as PageBasedRichEditView;
            //barPageAll.Caption = currentView.PageCount.ToString("N0");
            //barPage1.Caption = (currentView.CurrentPageIndex + 1).ToString("N0");
        }

    
        //void richEditControl1_UpdateUI(object sender, EventArgs e)
        //{
        //   //barPage1.Caption=  richEditControl1.VerticalScrollPosition.ToString("P0");
        //   //var length = richEditControl1.Document.Length;
        //   //var pages = richEditControl1.ActiveView.GetVisiblePagesRange();
        //   //var pos = richEditControl1.ActiveView.GetCursorBounds();
        //   //var pos2 = richEditControl1.ShowCaretInReadOnly;
        //   //var pos3 = richEditControl1.Document.NumberingLists;

        //    UpdateViewProcess();
        //}

        private void UpdateViewProcess()
        {
            try
            {
                //var caret=  richEditControl1.Document.CaretPosition.ToInt();
                var pages = richEditControl1.ActiveView.GetVisiblePagesRange();
                var pos1 = pages.End.ToInt() * 1.0 / richEditControl1.Document.Length;
                barProcess.Caption = pos1.ToString("P2");
            }
            catch
            {
                barProcess.Caption = null;
            }
        }

        public override void ShowDoc(string path, int? pos = null)
        {
            richEditControl1.LoadDocument(path);
            if (pos != null)
            {
                richEditControl1.Document.CaretPosition = richEditControl1.Document.CreatePosition(pos.Value);
                richEditControl1.ScrollToCaret();
            }
        }
    }
}
