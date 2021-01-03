using SemiAuto.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiAuto.CodeGeneration
{
    public class SemiTestGenerator : IGenerator
    {
        public Dictionary<string, string> Aliases { get; set; } = new Dictionary<string, string>();

        public string Generate(List<Activity> activities)
        {
            StringBuilder builder = new StringBuilder();

            foreach(Activity activity in activities)
            {
                switch(activity.Type)
                {
                    case Activity.Types.Click:
                    case Activity.Types.Toggle:
                        string id = activity.WPath;
                        if (this.Aliases.ContainsKey(activity.WPath))
                        {
                            id = this.Aliases[activity.WPath];
                        }
                        builder.AppendLine($"new {activity.ControlType}(\"{id}\").{activity.Type}()");
                        break;
                    case Activity.Types.Comment:
                        builder.AppendLine($"//{activity.Value}");
                        break;
                }
            }

            return null;
        }
    }
}
