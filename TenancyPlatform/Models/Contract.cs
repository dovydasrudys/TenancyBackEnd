using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public double Price { get; set; }   // amount to pay per month
        public DateTime Start { get; set; }
        public int Duration { get; set; }   // duration in months

        public int? TenantId { get; set; }
        public User Tenant { get; set; }

        public int? LandlordId { get; set; }
        public User Landlord { get; set; }

        public int? RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }
    }
}
