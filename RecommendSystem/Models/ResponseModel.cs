using System;
using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class ResponseModel
    {
        public float Center_lat { get; set; }
        public float Center_long { get; set; }
        public DistanceProfits Profits { get; set; }
        public Appartment[] Apartments { get; set; }
    }

    public static class ResponseModelExtension
    {
        public static ApartmentsResult ToApartmentsResult(this ResponseModel model)
        {
            const int numberOfApartments = 50;

            var appartmentInfoResult = new List<AppartmentInfo>();

            foreach (var appartment in model.Apartments)
            {
                appartmentInfoResult.Add(new AppartmentInfo
                {
                    Lat = appartment.Lat,
                    Long = appartment.Long,
                    Address = appartment.Address,
                    Area = appartment.Area,
                    Price = appartment.Price,
                    DistanceToCenter = appartment.Distance_to_center
                });
            }

            appartmentInfoResult.Sort(delegate (AppartmentInfo x, AppartmentInfo y)
            {
                return Convert.ToInt32(x.Price < y.Price);
            });

            if (appartmentInfoResult.Count > numberOfApartments)
                appartmentInfoResult.RemoveRange(numberOfApartments, appartmentInfoResult.Count - numberOfApartments);

            appartmentInfoResult.Sort(delegate (AppartmentInfo x, AppartmentInfo y)
            {
                return Convert.ToInt32(x.DistanceToCenter < y.DistanceToCenter);
            });

            return new ApartmentsResult
            {
                Center_lat = model.Center_lat,
                Center_lot = model.Center_long,
                Radius = appartmentInfoResult[appartmentInfoResult.Count - 1].DistanceToCenter,
                Apartments = appartmentInfoResult
            };

        }
    }
    public class DistanceProfits
    {
        public float Work_distance { get; set; }
        public float Work_time { get; set; }
        public float School_distance { get; set; }
        public float Shcool_time { get; set; }
    }

    public class Appartment
    {
        public float Lat { get; set; }
        public float Long { get; set; }
        public string Address { get; set; }
        public string Image_url { get; set; }
        public float Area { get; set; }
        public float Price { get; set; }
        public bool Leasing_available { get; set; }
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
