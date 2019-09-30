using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public enum PaymentStatus { NotPaid, Paid }

    public class Payment
    {
        public string Id { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        
        public string ContractId { get; set; }
        public Contract Contract { get; set; }

        public string PayerId { get; set; }
        public User Payer { get; set; }

        public string BeneficiaryId { get; set; }
        public User Beneficiary { get; set; }

        public List<Service> Services { get; set; }
    }
}
