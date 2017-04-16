using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Models
{
    public class Accounts
    {
        public string id { get; set; }
        public string particular { get; set; }
        public string debit { get; set; }
        public string credit { get; set; }
        public double amount { get; set; }
        public string organisation { get; set; }
        public DateTimeOffset date { get; set; }
    }
}
