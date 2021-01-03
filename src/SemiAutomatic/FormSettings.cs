using SemiAuto.Configurations;
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
    public partial class FormSettings : Form
    {
        public Configuration EditedConfiguration { get; private set; }

        public FormSettings()
        {
            InitializeComponent();

            this.EditedConfiguration = Configuration.Load(Configuration.DEFAULT_FILE);

            this.ucGeneral1.DataSource = this.EditedConfiguration;
            this.ucAlias1.DataSource = this.EditedConfiguration;
            this.ucMacros1.DataSource = this.EditedConfiguration;

        }
    }
}
