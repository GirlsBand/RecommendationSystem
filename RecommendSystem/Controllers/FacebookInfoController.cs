using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecommendationSystem.Controllers
{
    public class FacebookInfoController : ControllerBase
    {
        [Route("api/login/facebook")]
        [HttpGet]
        [Authorize]
        public string GetUserInfo()
        {
            return "UserInfo";
        }
    }
}
