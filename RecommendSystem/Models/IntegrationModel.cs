using System;
using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class IntegrationModel
    {
        public string Country = "USA";
        public string City;
        public Coordinates Coordinates;
        public bool InCity;

        public IntegrationModel CreateIntegrationModel(Account account)
        {
            var integrationModel = new IntegrationModel
            {
                City = account.Location.Name
            };

            var checkIns = new List<Coordinate>();

            foreach (var location in account.Tagged_Places)
                if (!(location.Created_time < DateTime.Today.AddYears(-1)))
                    checkIns.Add(new Coordinate(location.Place.Location.Latitude, location.Place.Location.Longitude));

            integrationModel.Coordinates = new Coordinates(checkIns);

            return integrationModel;
        }
    }

    public class Coordinate {

        public float Latitude;
        public float Longitude;

        public Coordinate(float latitude, float longitude) {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public class Coordinates
    {
        public Coordinate Work = null;
        public Coordinate Study = null;
        public List<Coordinate> CheckIns;

        public Coordinates(Coordinate work, Coordinate study, List<Coordinate> checkIns)
        {
            Work = work;
            Study = study;
            CheckIns = checkIns;
        }

        public Coordinates(List<Coordinate> checkIns)
        {
            CheckIns = checkIns;
        }

        public void AddCheckIn(Coordinate checkIn)
        {
            CheckIns.Add(checkIn);
        }

        public void RemoveCheckIn(Coordinate checkIn)
        {
            CheckIns.Remove(checkIn);
        }

    }

    public enum PreferenceType
    {
        if_city,
        if_near_subway
    }
}
