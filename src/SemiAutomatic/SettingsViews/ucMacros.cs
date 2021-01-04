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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SemiAuto.Data;

namespace SemiAuto.SettingsViews
{
    public partial class ucMacros : ucSettingsBase<Configuration>
    {
        private ListSync<Macro> m_Macros;

        public ucMacros()
        {
            InitializeComponent();

        }

        public override void OnInit()
        {
            this.m_Macros = new ListSync<Macro>(this.DataSource.Macros);
            this.lbxMacros.DataSource = this.m_Macros;
            this.lbxMacros.DisplayMember = nameof(Macro.Name);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Macro m = new Macro()
            {
                Name = "<new>"
            };

            this.m_Macros.Add(m);

            this.lbxMacros.SelectedItem = m;
        }

        private void lbxMacros_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ucMacroView1.DataSource = this.lbxMacros.SelectedItem as Macro;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(this.lbxMacros.SelectedItem != null)
            {
                int next = this.lbxMacros.SelectedIndex == 0 ? 0 : this.lbxMacros.SelectedIndex - 1;

                this.m_Macros.Remove(this.lbxMacros.SelectedItem as Macro);
                this.lbxMacros.SelectedIndex = next;
            }
        }
    }
}
