using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Models
{
    public class Users
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string organisation { get; set; }
        public string roles { get; set; }
    }
}
