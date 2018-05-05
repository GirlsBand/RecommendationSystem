using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RecommendationSystem.Controllers
{
    [Route("api/facebook-signin")]
    public class FacebookInfoController : ControllerBase
    {
        const string AppId = "193302211282683";
        const string AppSecret = "2fb8063b7eded7b8a0f63b68c108dc7f";

        [HttpGet]
        public string GetUserName([FromBody]string clientAccessToken)
        {
            //string url = $"https://graph.facebook.com/v2.12/oauth/access_token?client_id={AppId}&client_secret={AppSecret}&grant_type=client_credentials";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


            //string data;
            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //using (Stream stream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    data = reader.ReadToEnd();
            //};

            //var acccesTokenInfo = JsonConvert.DeserializeObject<AccessTokenInfo>(data);

            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var getAccountTask = facebookService.GetAccountAsync(clientAccessToken);

            Task.WaitAll(getAccountTask);
            var account = getAccountTask.Result;
            return account.Name;
        }

    }
}
