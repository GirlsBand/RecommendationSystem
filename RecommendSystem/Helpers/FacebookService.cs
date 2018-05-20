using System;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public LocationInfo[] Tagged_Places { get; set; }
        public LocationInfo Location { get; set; }
    }

    public class LocationInfo
    {
        public long Id { get; set; }
        public DateTime Created_time { get; set; }
        public string Name { get; set; }
        public Place Place { get; set; }
    }
    public class Place
    {
        public long Id { get; set; }
        public Location Location { get; set; }
    }

    public class Location {
        public string City { get; set; }
        public string Country { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public interface IFacebookService
    {
        Task<Account> GetAccountAsync(string accessToken);
        Task PostOnWallAsync(string accessToken, string message);
    }

    public class FacebookService : IFacebookService
    {
        private readonly ISocialNetHttpClient _facebookClient;

        public FacebookService(ISocialNetHttpClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<Account> GetAccountAsync(string accessToken)
        {
            var result = await _facebookClient.GetAsync<dynamic>(
                accessToken, "me", "fields=id,name,tagged_places.limit(50),location");

            if (result == null)
            {
                return new Account();
            }

            var account = new Account
            {
                Id = result.id,
                Name = result.name,
                Tagged_Places = result.tagged_places.data.ToObject<LocationInfo[]>(),
                Location = result.location.ToObject<LocationInfo>()
            };

            return account;
        }

        public async Task PostOnWallAsync(string accessToken, string message)
            => await _facebookClient.PostAsync(accessToken, "me/feed", new { message });
    }
}