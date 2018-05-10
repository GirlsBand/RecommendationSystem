using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class IntegrationModel
    {
        public string Country;
        public string City;
        public Coordinates Coordinates;
        public Dictionary<PreferenceType, bool> Preferences;
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
        public Coordinate Work;
        public Coordinate Study;
        public List<Coordinate> CheckIns;

        public Coordinates(Coordinate work, Coordinate study, List<Coordinate> checkIns)
        {
            Work = work;
            Study = study;
            CheckIns = checkIns;
        }

        public void UpdateWork(Coordinate work)
        {
            Work = work;
        }

        public void UpdateStudy(Coordinate study)
        {
            Study = study;
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
