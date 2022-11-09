using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLab.Classes.Database.Columns
{
    public class StrCol : Column
    {
        public StrCol(string name) : base(name) { Type = "STRING"; }

        public override bool Validate(string value) => true;

    }
}
