using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiAuto.CodeGeneration
{
    public interface IGenerator
    {
        string Generate(List<Activity> activities);
    }
}
