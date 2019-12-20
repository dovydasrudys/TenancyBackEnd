using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public class Failure
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsFixed { get; set; }
        public DateTime IssueDate { get; set; }

        public int? ReporterId { get; set; }
        public User Reporter { get; set; }

        public int? ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
