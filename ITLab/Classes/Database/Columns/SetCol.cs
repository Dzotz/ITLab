using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLab.Classes.Database.Columns
{
    public class SetCol : Column
    {

        public HashSet<string> Values { get; set; } = new HashSet<string>();
        public SetCol(string name, string values) : base(name) {
            Type = "SET";
            Values = values.Split(';').ToHashSet();
        }
        public override bool Validate(string value)
        {
            return Values.Contains(value);
        }
    }
}
