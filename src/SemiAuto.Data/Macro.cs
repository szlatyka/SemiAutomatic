using CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiAuto.Data
{
    [Serializable]
    public class Macro
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<Activity> Steps { get; set; }
        public Activity Replacement { get; set; }
    }
}
