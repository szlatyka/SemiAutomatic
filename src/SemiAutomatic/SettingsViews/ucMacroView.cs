using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SemiAuto.Data;

namespace SemiAuto.SettingsViews
{
    public partial class ucMacroView : ucSettingsBase<Macro>
    {
        public ucMacroView()
        {
            InitializeComponent();
        }

        public override void OnInit()
        {
            this.tbxName.Text = this.DataSource.Name;
            this.tbxSubstitute.Text = this.DataSource.Replacement.Value?.ToString();
            this.dgvSteps.DataSource = this.DataSource.Steps;
        }

        private void tbxName_TextChanged(object sender, EventArgs e)
        {
            this.DataSource.Name = this.tbxName.Text;
        }

        private void tbxSubstitute_TextChanged(object sender, EventArgs e)
        {
            this.DataSource.Replacement.Value = this.tbxSubstitute.Text;
        }
    }
}
