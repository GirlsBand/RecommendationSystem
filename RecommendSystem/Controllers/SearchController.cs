using Microsoft.AspNetCore.Mvc;
using RecommendationSystem.Models;

namespace RecommendationSystem.Controllers
{
    public class SearchController: ControllerBase
    {
        [HttpGet]
        [Route("api/form")]
        public Form DefaultForm()
        {
            return Form.Default;
        }
    }
}
