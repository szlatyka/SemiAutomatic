using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiAuto.CodeGeneration
{
    public class ReadableGenerator : IGenerator
    {
        public string Generate(List<Activity> activities)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < activities.Count; i++)
            {
                builder.AppendLine($"{i + 1}. {activities[i].Display}");
            }

            return builder.ToString();
        }
    }
}
