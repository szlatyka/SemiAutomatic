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
    [Browsable(false)]
    public class ucSettingsBase<T> : UserControl
    {
        public T DataSource
        {
            get
            {
                return this.m_DataSource;
            }

            set
            {
                this.m_DataSource = value;
                if(this.m_DataSource != null)
                {
                    this.OnInit();
                }
            }
        }
        private T m_DataSource ;

        public virtual void OnInit()
        {

        }
    }
}
