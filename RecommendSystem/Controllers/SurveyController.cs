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

        [HttpPost]
        [Route("api/survey/answers")]
        public void Answers([FromBody]string json) {
            var surveyResuslt = JsonConvert.DeserializeObject<Survey>(json);

            var inCityQuestion = surveyResuslt.FindQuestion(Survey.InCityPreferenceQuestion);
            var inCity = false;

            if (inCityQuestion.Answers[0] == "City")
                inCity = true;
        }
    }
}
