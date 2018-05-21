using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecommendationSystem.Models;

namespace RecommendationSystem.Controllers
{
    public class SurveyController : ControllerBase
    {
        [HttpGet]
        [Route("api/survey")]
        public Survey DefaultSurvey() => Survey.Default;      
    }
}