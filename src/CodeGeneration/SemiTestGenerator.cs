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
                        if(activities[i + j].Type != macro.Steps[j].Type ||
                            activities[i + j].ControlType != macro.Steps[j].ControlType ||
                            activities[i + j].WPath != macro.Steps[j].WPath)
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


            foreach(Activity activity in macroEnabledActivities)
            {
                switch(activity.Type)
                {
                    case Activity.Types.Click:
                        builder.AppendLine($"new Controls.{activity.ControlType}(\"{this.GetWPath(activity)}\").{activity.Type}();");
                        break;
                    case Activity.Types.Toggle:
                        if(activity.ControlType == "CheckBox")
                        {
                            builder.AppendLine($"new Controls.{activity.ControlType}(\"{this.GetWPath(activity)}\").Check();");
                        }
                        break;
                    case Activity.Types.SelectionChange:
                        if (activity.ControlType == "RadioButton")
                        {
                            builder.AppendLine($"new Controls.{activity.ControlType}(\"{this.GetWPath(activity)}\").Select();");
                        }
                        else if(activity.ControlType != "ComboBox")
                        {
                            builder.AppendLine($"new Controls.{activity.ControlType}(\"{this.GetWPath(activity)}\").SelectItem(\"{activity.Value}\");");
                        }
                        break;
                    case Activity.Types.ValueChange:
                        if (activity.ControlType == "ComboBox")
                        {
                            builder.AppendLine($"new Controls.{activity.ControlType}(\"{this.GetWPath(activity)}\").SelectItem(\"{activity.Value}\");");
                        }
                        else
                        {
                            builder.AppendLine($"new Controls.{activity.ControlType}(\"{this.GetWPath(activity)}\").Value = \"{activity.Value}\";");
                        }
                        break;
                    case Activity.Types.Macro:
                        builder.AppendLine(activity.Value.ToString());
                        break;
                    case Activity.Types.Comment:
                        builder.AppendLine($"//{activity.Value}");
                        break;
                }
            }

            return builder.ToString();
        }

        private string GetWPath(Activity activity)
        {
            string id = activity.WPath;
            Alias alias = this.Aliases.FirstOrDefault(a => a.OriginalID == id);
            if (alias != null)
            {
                id = alias.Substitution;
            }
            return id;
        }
    }
}
