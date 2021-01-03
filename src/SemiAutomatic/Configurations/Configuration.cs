using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemiAuto.Configurations
{
    [Serializable]
    public class Configuration
    {
        public const string DEFAULT_FILE = "config.xml";

        public string Generator { get; set; } = "C# code (Semilab test atumation)";

        public List<Alias> Aliases { get; set; } = new List<Alias>();

        public List<Macro> Macros { get; set; } = new List<Macro>();

        public void Save(string file)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Configuration));

            using (StreamWriter sw = new StreamWriter(file))
            {
                ser.Serialize(sw, this);
            }
        }

        public static Configuration Load(string file)
        {
            Configuration result = null;

            XmlSerializer ser = new XmlSerializer(typeof(Configuration));

            using (StreamReader sr = new StreamReader(file))
            {
                result = ser.Deserialize(sr) as Configuration;
            }

            return result;
        }
    }
}
