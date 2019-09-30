using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public class Service
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        
        public string PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
