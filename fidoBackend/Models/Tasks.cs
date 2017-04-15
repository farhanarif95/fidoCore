using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Models
{
    public class Tasks
    {
        public string id { get; set; }
        public string projectId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string assignedTo { get; set; }
        [JsonIgnore]
        public string assignedToName { get; set; }
        public string status { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
