using System;
using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class IntegrationModel
    {
        public string Country { get; set; } = "USA";
        public string City { get; set; }
        public string Work { get; set; }
        public string Study { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        public bool InCity { get; set; }
        public bool PetsToWalkPresence { get; set; }
        public int AmountOfPeopleLiving { get; set; }

        public void AddCheckIn(Coordinate checkIn)
        {
            Coordinates.Add(checkIn);
        }

        public void RemoveCheckIn(Coordinate checkIn)
        {
            Coordinates.Remove(checkIn);
        }
    }

    public class Coordinate {

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Coordinate(float latitude, float longitude) {
            Latitude = latitude;
            Longitude = longitude;
        }

    }
}
