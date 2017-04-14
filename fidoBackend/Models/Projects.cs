using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Models
{
    public class Projects
    {
        public string id { get; set; }
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerAddress { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string teamLeads { get;set; }
    }
}
