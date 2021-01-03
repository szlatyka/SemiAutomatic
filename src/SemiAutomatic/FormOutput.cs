using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemiAuto
{
    public partial class FormOutput : Form
    {
        public string Content { get; set; }

        public FormOutput()
        {
            InitializeComponent();

            this.tbxOutput.Text = this.Content;
        }

        public FormOutput(string content) : this()
        {
            this.Content = content;
            this.tbxOutput.Text = this.Content;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.Content);
        }
    }
}
