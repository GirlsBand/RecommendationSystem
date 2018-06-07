using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net;

namespace RecommendationSystem.Controllers
{
    [Route("api/facebook-signin")]
    public class FacebookInfoController : ControllerBase
    {
        private readonly IFacebookService _service;

        public FacebookInfoController(IFacebookService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        [HttpPost]
        public async Task<HttpResponseMessage> Login(string clientAccessToken)
        {
            var account = await _service.GetAccountAsync(clientAccessToken);
            
            var response = new HttpResponseMessage
            {
                Content = new StringContent(account.Name, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }
    }
}
