using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<string> Login(string clientAccessToken)
        {
            var account = await _service.GetAccountAsync(clientAccessToken);
            return account.Name;
        }
    }
}
