using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLab.Classes.Database
{
    public abstract class Column
    {
        public string Name { get; set; }
        public string Type { get; set; } = "";

        public Column(string name)
        {
            Name = name;
        }

        public abstract bool Validate(string value);
    }

}
