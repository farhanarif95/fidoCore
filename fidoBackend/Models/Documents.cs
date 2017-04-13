using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Models
{
    public class Documents
    {
        public string id { get; set; }
        public string projectId { get; set; }
        public string url { get; set; }
        public string author { get; set; }
        public string attention { get; set; }
        public string description { get; set; }
    }
}
