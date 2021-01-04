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
        //Example on how it should work with common stuff:
        //new Controls.Button("").Click();
        //new Controls.CheckBox("").Check();
        //new Controls.ComboBox("").SelectItem("");
        //new Controls.ListBox("").SelectItem("");
        //new Controls.ListView("").SelectItem("");
        //new Controls.TextBox("").Value = "";
        //new Controls.TreeView("").SelectItem("");
        //new Controls.RadioButton("").Select();

        public List<Alias> Aliases { get; set; } = new List<Alias>();
        public List<Macro> Macros { get; set; } = new List<Macro>();

        public string Generate(List<Activity> activities)
        {
            StringBuilder builder = new StringBuilder();

            List<Activity> macroEnabledActivities = new List<Activity>();

            for (int i = 0; i < activities.Count; i++)
            {
                Macro matchedMaco = null;

                foreach(Macro macro in this.Macros)
                {
                    bool match = true;

                    for(int j = 0; j < macro.Steps.Count; j++)
                    {
                        if(activities[i].Type != macro.Steps[j].Type ||
                            activities[i].ControlType != macro.Steps[j].ControlType ||
                            activities[i].WPath != macro.Steps[j].WPath)
                        {
                            match = false; break;
                        }
                    }

                    if(match)
                    {
                        matchedMaco = macro;
                        break;
                    }
                }

                if(matchedMaco != null)
                {
                    macroEnabledActivities.Add(matchedMaco.Replacement);
                    i += matchedMaco.Steps.Count - 1;
                }
                else
                {
                    macroEnabledActivities.Add(activities[i]);
                }
            }


            foreach(Activity activity in activities)
            {
                switch(activity.Type)
                {
                    case Activity.Types.Click:
                    case Activity.Types.Toggle:
                        string id = activity.WPath;
                        Alias alias = this.Aliases.FirstOrDefault(a => a.OriginalID == id);
                        if (alias != null)
                        {
                            id = alias.Substitution;
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
