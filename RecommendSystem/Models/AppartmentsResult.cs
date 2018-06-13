using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class ApartmentsResult
    {
        public float Center_lat { get; set; }
        public float Center_lot { get; set; }
        public float Radius { get; set;  }
        public List<AppartmentInfo> Apartments { get; set; }

    }

    public class AppartmentInfo
    {
        public float Lat { get; set; }
        public float Long { get; set; }
        public string Address { get; set; }
        public float Area { get; set; }
        public float Price { get; set; }
        public float DistanceToCenter { get; set; }
    }
}
