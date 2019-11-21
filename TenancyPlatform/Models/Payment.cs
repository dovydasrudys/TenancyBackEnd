using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public enum PaymentStatus { NotPaid, Paid }

    public class Payment
    {
        public int Id { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        
        public int? ContractId { get; set; }
        public Contract Contract { get; set; }

        public int? PayerId { get; set; }
        public User Payer { get; set; }

        public int? BeneficiaryId { get; set; }
        public User Beneficiary { get; set; }

        public List<Service> Services { get; set; }
    }
}
