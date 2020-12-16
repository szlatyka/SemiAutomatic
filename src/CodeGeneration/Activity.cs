using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    public class Activity
    {
        public enum Types
        {
            Click,
            ValueChange,
            SelectionChange,
            Toggle,
            Comment
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
                switch(this.Type)
                {
                    case Types.Click:           return $"Click {ControlType.ToLower()} \"{ControlDisplayText}\" (WPATH: {WPath})";
                    case Types.SelectionChange: return $"Select option {Value} of {ControlType.ToLower()} \"{ControlDisplayText}\" (WPATH: {WPath})";
                    case Types.Toggle:          return $"Toggle {ControlType.ToLower()} \"{ControlDisplayText}\" (WPATH: {WPath})";
                    case Types.ValueChange:     return $"Change value of {ControlType.ToLower()} \"{ControlDisplayText}\" to \"{Value}\" (WPATH: {WPath})";
                    case Types.Comment:         return this.Value.ToString();
                }
                return null;
            }
        }
    }
}
