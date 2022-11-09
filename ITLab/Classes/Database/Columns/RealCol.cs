using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLab.Classes.Database.Columns
{
    public class RealCol: Column
    {
        public RealCol(string name) : base(name) { Type = "REAL"; }

        public override bool Validate(string value) => double.TryParse(value, out _);

    }
}
