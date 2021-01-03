using CodeGeneration;
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
    public partial class FormAddMacro : Form
    {
        public Macro EditedItem { get { return this.ucMacroView1.DataSource; } }

        public FormAddMacro()
        {
            InitializeComponent();
        }

        public FormAddMacro(List<Activity> activities)
        {
            InitializeComponent();

            this.ucMacroView1.DataSource = new Macro()
            {
                Steps = activities,
                Name = "macro",
            };
        }
    }
}
