using Facebook;
using Newtonsoft.Json;
using System;
using System.Net;

namespace RecommendationSystem
{
    public class AccesTokenInfo
    {
        public string Access_Token;
        public string Token_Type;
    }

    class Program
    {
        const string AppId = "193302211282683";
        const string AppSecret = "2fb8063b7eded7b8a0f63b68c108dc7f";

        static void Main(string[] args)
        {
            var client = new WebClient();

            string oauthUrl = string.Format("https://graph.facebook.com/oauth/access_token?type=client_cred&client_id={0}&client_secret={1}", AppId, AppSecret);
            var tempo = client.DownloadString(oauthUrl).ToString();
            string accessToken = JsonConvert.DeserializeObject<AccesTokenInfo>(tempo).Access_Token;

            var fbClient = new FacebookClient(accessToken);
           
            try
            {
                var fbData = fbClient.Get("/wikipedia/feed?fields=name").ToString();
                Console.Write(fbData);
            }
            catch (FacebookOAuthException ex)
            {
                Console.Write(ex.Message);
            }
            Console.ReadLine();
        }
    }
}