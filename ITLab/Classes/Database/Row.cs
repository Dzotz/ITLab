using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLab.Classes.Database
{
    public class Row
    {
        public List<string> Values { get; set; } = new List<string>();

        public string this[int i]
        {
            get => Values[i];
            set => Values[i] = value;
        }
    }

}
