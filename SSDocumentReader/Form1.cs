using SSDocumentReader.MainControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSDocumentReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var directoryView = new DirectoryViewControl();
            this.xtraTabPage1.Controls.Add(directoryView);
            directoryView.Dock = DockStyle.Fill;
        }
    }
}
