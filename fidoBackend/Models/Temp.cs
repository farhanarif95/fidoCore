using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Models
{
    public class Temp
    {
        public string projectId { get; set; }
        public List<Users> myUsers { get; set; }
    }

    public class Temp1
    {
        public string projectId { get; set; }
        public Tasks task { get; set; }
    }
}
