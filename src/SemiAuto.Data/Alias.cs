using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiAuto.Data
{
    [Serializable]
    public class Alias
    {
        public string OriginalID { get; set; }
        public string Substitution { get; set; }
    }
}
