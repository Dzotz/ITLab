using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLab.Classes.Database.Columns
{
    public class EmailCol : Column
    {
        public EmailCol(string name) : base(name) { Type = "EMAIL"; }

        public override bool Validate(string value)
        {
            var trimmedEmail = value.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

    }
}
