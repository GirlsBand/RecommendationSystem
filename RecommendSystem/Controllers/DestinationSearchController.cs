using Microsoft.AspNetCore.Mvc;

namespace RecommendationSystem.Controllers
{
    public class DestinationSearchController : ControllerBase
    {
        [Route("api/destination")]
        [HttpGet]
        public string GetDetination()
        {
            return "It's default destination [0,0]";
        }
    }
}
