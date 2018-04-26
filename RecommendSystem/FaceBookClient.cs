using System;
using System.Net.Http;
using Newtonsoft.Json;
using RecommendationSystem.Models;

namespace RecommendationSystem
{
    public class FaceBookClientWrapper
    {
        const string AppId = "193302211282683";
        const string AppSecret = "2fb8063b7eded7b8a0f63b68c108dc7f";
        readonly HttpClient _client;
        //private FacebookClient _fbClient;

        public FaceBookClientWrapper(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public void SetupToken()
        {
            string oauthUrl = $"https://graph.facebook.com/oauth/access_token?type=client_cred&client_id={AppId}&client_secret={AppSecret}";
            var tempo = _client.GetAsync(oauthUrl).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var accessToken = JsonConvert.DeserializeObject<AccesTokenInfo>(tempo).AccessToken;
            //_fbClient = new FacebookClient(accessToken);
        }

        public string GetTestInfo()
        {
            

            /*try
            {
                var fbData = _fbClient.Get("/wikipedia/feed?fields=name").ToString();
                Console.Write(fbData);
            }
            catch (FacebookOAuthException ex)
            {
                Console.Write(ex.Message);
            }*/

            return string.Empty;
        }



    }
}
