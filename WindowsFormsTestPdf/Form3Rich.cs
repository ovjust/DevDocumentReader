using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.API.Native.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsTestPdf
{
    public partial class Form3Rich : Form
    {
        public Form3Rich()
        {
            InitializeComponent();
        }

        private void Form3Rich_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            richEditControl1.LoadDocument();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var text=(string)barEditItem1.EditValue;
           var results= richEditControl1.Document.FindAll(text, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var words=Convert.ToInt32( barEditItem2.EditValue);
            //var pos1 = new NativeDocumentPosition();
            //NativeDocumentRange range = new NativeDocumentRange();
            //range.Start = pos1;
            //  var sele = richEditControl1.Document.Selection;
          var range=  richEditControl1.Document.CreateRange(words,10);


          //richEditControl1.Document.Selection = range;

          //   var text=(string)barEditItem1.EditValue;
          //   richEditControl1.Document.StartSearch(text,SearchOptions.None,SearchDirection.Forward,range);


             //richEditControl1.Document.CaretPosition.BeginUpdateDocument();
             richEditControl1.Document.CaretPosition = richEditControl1.Document.CreatePosition(words);
             //richEditControl1.Document.CaretPosition.EndUpdateDocument(richEditControl1.Document);

            richEditControl1.ScrollToCaret();

             //ParagraphProperties pp = richEditControl1.Document.BeginUpdateParagraphs(richEditControl1.Document.Range);//richEditControl1.Document.Range
             //pp.Alignment = ParagraphAlignment.Center;
             //richEditControl1.Document.EndUpdateParagraphs(pp);


            //richEditControl1.Document.GetCustomMarkByVisualInfo(

            //richEditControl1.VerticalScrollPosition
            //richEditControl1.Document.Selection.Start = words;
            //richEditControl1.Document.Selection.Length = 5;
        }
    }
}
