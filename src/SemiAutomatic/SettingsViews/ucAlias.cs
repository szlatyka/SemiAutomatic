using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using SemiAuto.Configurations;
using System.Collections.Specialized;

namespace SemiAuto.SettingsViews
{
    public partial class ucAlias : ucSettingsBase<Configuration>
    {
        private ListSync<Alias> m_Aliases;

        public ucAlias()
        {
            InitializeComponent();

        }

        public override void OnInit()
        {
            this.m_Aliases = new ListSync<Alias>(this.DataSource.Aliases);
            this.dgvGrid.DataSource = this.m_Aliases;
        }
    }
}
