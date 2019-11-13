using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public class Advert
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double LoanPrice { get; set; }   // per month

        public int? OwnerId { get; set; }
        public User Owner { get; set; }

        public int? RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }
    }
}
