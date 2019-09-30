using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public class Contract
    {
        public string Id { get; set; }
        public double Price { get; set; }   // amount to pay per month
        public DateTime Start { get; set; }
        public int Duration { get; set; }   // duration in months

        public string TenantId { get; set; }
        public User Tenant { get; set; }

        public string LandlordId { get; set; }
        public User Landlord { get; set; }
    }
}
