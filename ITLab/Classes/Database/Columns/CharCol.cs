using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLab.Classes.Database.Columns
{
    public class CharCol : Column
    {
        public CharCol(string name) : base(name) { Type = "CHAR"; }

        public override bool Validate(string value) => char.TryParse(value, out _);

    }
}
