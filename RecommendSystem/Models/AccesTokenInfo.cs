using Newtonsoft.Json;

namespace RecommendationSystem.Models
{
    public class AccesTokenInfo
    {
        [JsonProperty("Access_Token")]
        public string AccessToken { get; set; }

        [JsonProperty("Token_Type")]
        public string TokenType { get; set; }
    }
}
