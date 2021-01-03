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
    public partial class ucMacroView : ucSettingsBase<Macro>
    {
        public ucMacroView()
        {
            InitializeComponent();
        }

        public override void OnInit()
        {
            this.tbxName.DataBindings.Clear();
            this.tbxName.DataBindings.Add(new Binding(nameof(this.tbxSubstitute.Text), this.DataSource, nameof(this.DataSource.Name), true, DataSourceUpdateMode.OnValidation));

            this.tbxSubstitute.DataBindings.Clear();
            this.tbxSubstitute.DataBindings.Add(new Binding(nameof(this.tbxSubstitute.Text), this.DataSource, nameof(this.DataSource.Replacement), true, DataSourceUpdateMode.OnValidation));

            this.dgvSteps.DataSource = this.DataSource.Steps;
        }
    }
}
