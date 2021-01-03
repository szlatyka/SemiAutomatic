using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SemiAuto.Configurations;

namespace SemiAuto.SettingsViews
{
    public partial class ucGeneral : ucSettingsBase<Configuration>
    {
        public ucGeneral()
        {
            InitializeComponent();
        }

        public override void OnInit()
        {
            this.comboBox1.SelectedItem = this.DataSource.Generator;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.DataSource.Generator = (string)this.comboBox1.SelectedItem;
        }
    }
}
