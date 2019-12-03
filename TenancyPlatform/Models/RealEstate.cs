using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyPlatform.Models
{
    public class RealEstate
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNr { get; set; }
        public double Area { get; set; }        // in squared meters
        public int Rooms { get; set; }
        public int Floor { get; set; }
        public int BuildYear { get; set; }

        public int? OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
