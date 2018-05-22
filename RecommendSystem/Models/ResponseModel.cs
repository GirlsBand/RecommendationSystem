using System;
using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class ResponseModel
    {
        public float Center_lat { get; set; }
        public float Center_lot { get; set; }
        public DistanceProfits Profits { get; set; }
        public Apartment[] Apartment { get; set; }

    }

    public class DistanceProfits
    {
        public float Work_distance { get; set; }
        public float Work_time { get; set; }
        public float School_distance { get; set; }
        public float Shcool_time { get; set; }
    }

    public class Apartment
    {
        public float Lat { get; set; }
        public float Long { get; set; }
        public string Address { get; set; }
        public string Image_url { get; set; }
        public float Area { get; set; }
        public float Price { get; set; }
        public bool Leasing_avaliable { get; set; }
        public float Distance_to_center { get; set; }
        public ApartmentProfits Profits { get; set; }
    }

    public class ApartmentProfits
    {
        public bool Park_nearby { get; set; }
        public bool Cafe_nearby { get; set; }
        public bool Cinema_nearby { get; set; }
        public bool Higway_nearby { get; set; }
    }
}
