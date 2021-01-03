using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiAuto.Data
{
    [Serializable]
    public class Activity
    {
        public enum Types
        {
            Click,
            ValueChange,
            SelectionChange,
            Toggle,
            Comment,
            Macro,
        }

        [Browsable(false)]
        public Types Type { get; set; }
        [Browsable(false)]
        public string ControlType { get; set; }
        [Browsable(false)]
        public string ControlDisplayText { get; set; }
        [Browsable(false)]
        public string WPath { get; set; }
        [Browsable(false)]
        public object Value { get; set; }

        public string Display
        {
            get
            {
                string optionalPath = String.IsNullOrEmpty(ControlDisplayText) ? $"(WPATH: {WPath})" : "";
                switch (this.Type)
                {
                    case Types.Click:           return $"Click {ControlType.ToLower()} \"{ControlDisplayText}\" {optionalPath}";
                    case Types.SelectionChange: return $"Select option {Value} of {ControlType.ToLower()} \"{ControlDisplayText}\" {optionalPath}";
                    case Types.Toggle:          return $"Toggle {ControlType.ToLower()} \"{ControlDisplayText}\" {optionalPath}";
                    case Types.ValueChange:     return $"Change value of {ControlType.ToLower()} \"{ControlDisplayText}\" to \"{Value}\" {optionalPath}";
                    case Types.Comment:         return this.Value.ToString();
                }
                return null;
            }
        }
    }
}
