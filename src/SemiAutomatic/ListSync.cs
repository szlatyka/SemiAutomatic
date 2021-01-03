using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemiAuto
{
    public class ListSync<T> : BindingSource
    {
        private ObservableCollection<T> m_Observable;

        public ListSync(List<T> original)
        {
            this.m_Observable = new ObservableCollection<T>(original);
            this.DataSource = this.m_Observable;
            //this.lbxMacros.DataSource = this.m_Binder;

            this.m_Observable.CollectionChanged += (sender, e) =>
            {
                //Holy shit winforms sucks more than I remember...
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    original.AddRange(e.NewItems.Cast<T>());
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    e.OldItems.Cast<T>().ToList().ForEach(el => original.Remove(el));
                }
                this.ResetBindings(false);
            };
        }
    }
}
