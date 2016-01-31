using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSDocumentReader.SingleControls
{
    public class DocViewerControlBase : UserControl
    {
        public virtual void ShowDoc(string path, int? pos = null) { }
    }
}
