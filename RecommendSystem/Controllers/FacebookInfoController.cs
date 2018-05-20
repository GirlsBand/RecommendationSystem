using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RecommendationSystem.Controllers
{
    [Route("api/facebook-signin")]
    public class FacebookInfoController : ControllerBase
    {
        private IFacebookService _service;

        public FacebookInfoController(IFacebookService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        public string Login(string clientAccessToken)
        {
            var getAccountTask = _service.GetAccountAsync(clientAccessToken);
            Task.WaitAll(getAccountTask);
            var account = getAccountTask.Result;

            return account.Name;
        }
    }
}
