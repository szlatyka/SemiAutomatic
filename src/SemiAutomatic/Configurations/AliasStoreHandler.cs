using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Machines.Common.AutomationId_Dictionaries;
using SemiAuto.Data;

namespace SemiAuto.Configurations
{
    public class AliasStoreHandler
    {
        public static void AutoFillFromStore(List<Alias> aliases, Type store)
        {
            //Get a type, find all static dictionaries, and fill all unused entries - easy-peasy lemon-sqeazy
            FieldInfo[] fields = store.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach(FieldInfo field in fields)
            {
                if(field.FieldType == typeof(Dictionary<string, string>))
                {
                    Dictionary<string, string> section = field.GetValue(null) as Dictionary<string, string>;
                    foreach(KeyValuePair<string, string> kvp in section)
                    {
                        if (aliases.FirstOrDefault(a => a.OriginalID == kvp.Value) == null)
                        {
                            aliases.Add(new Alias()
                            {
                                OriginalID = kvp.Value,
                                Substitution = $"{store.Name}.{field.Name}[\"{kvp.Key}\"]"
                            });
                        }
                    }
                }
            }
        }
    }
}
