using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public class Failure
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool IsFixed { get; set; }

        public string ReporterId { get; set; }
        public User Reporter { get; set; }

        public string ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
