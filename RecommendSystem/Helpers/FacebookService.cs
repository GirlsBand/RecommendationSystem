using System.Threading.Tasks;

namespace RecommendationSystem
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Locale { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }

    public interface IFacebookService
    {
        Task<Account> GetAccountAsync(string accessToken);
        Task PostOnWallAsync(string accessToken, string message);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<Account> GetAccountAsync(string accessToken)
        {
            var result = await _facebookClient.GetAsync<dynamic>(
                accessToken, "me", "fields=id,name,email,first_name,last_name,age_range,birthday,gender,locale");

            if (result == null)
            {
                return new Account();
            }

            var account = new Account
            {
                Id = result.id,
                Email = result.email,
                Name = result.name,
                UserName = result.username,
                FirstName = result.first_name,
                LastName = result.last_name,
                Locale = result.locale
            };

            return account;
        }

        public async Task PostOnWallAsync(string accessToken, string message)
            => await _facebookClient.PostAsync(accessToken, "me/feed", new { message });
    }
}