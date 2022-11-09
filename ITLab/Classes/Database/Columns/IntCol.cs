using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLab.Classes.Database.Columns
{
    public class IntCol : Column
    {
        public IntCol(string name) : base(name) { Type = "INT"; }

        public override bool Validate(string value) => int.TryParse(value, out _);

    }
}
