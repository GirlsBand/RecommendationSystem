using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RecommendationSystem.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecommendationSystem.Controllers
{
    public class DestinationSearchController : ControllerBase
    {
        const string requestUri = "";

        readonly IFacebookService _service;
        Survey surveyResult;
       
        public DestinationSearchController(IFacebookService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Route("api/destination")]
        [HttpPost]
        public async Task<string> GetDetination([FromBody]string json)
        {
            if (!Request.Headers.ContainsKey("ClientAccessToken"))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            Request.Headers.TryGetValue("ClientAccessToken", out StringValues clientAccessTokens);

            var clientAccessToken = clientAccessTokens[0].ToString();
            var account = await _service.GetAccountAsync(clientAccessToken);

            surveyResult = JsonConvert.DeserializeObject<Survey>(json);

            var integrationModel = FormIntegrationModel(account);

            var client = new HttpClient();
            var response = await client.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(integrationModel)));

            return "It's default destination [0,0]";
        }

        private IntegrationModel FormIntegrationModel(Account account)
        {
            var integrationModel = CreateIntegrationModel(account);

            integrationModel.InCity = FindAnswerByTitle(Survey.WhatDoYouPreferQuestion) == "City";
            integrationModel.AmountOfPeopleLiving = Int32.Parse(FindAnswerByTitle(Survey.HowManyPeopleAreInYourFamilyQuestion));
            integrationModel.City = FindAnswerByTitle(Survey.ClarifyCityToLiveQuestion);
            integrationModel.Work = FindAnswerByTitle(Survey.SpecifyPlaceOfWorkQuestion);
            integrationModel.Study = FindAnswerByTitle(Survey.SpecifyPlaceOfStudyQuestion);
            integrationModel.PetsToWalkPresence = FindAnswerByTitle(Survey.DoYouHavePetsToWalkQuestion) == "Yes";

            return integrationModel;
        }

        IntegrationModel CreateIntegrationModel(Account account)
        {
            var integrationModel = new IntegrationModel();

            var checkIns = new List<Coordinate>();

            foreach (var location in account.Tagged_Places)
                if (!(location.Created_time < DateTime.Today.AddYears(-1)))
                    checkIns.Add(new Coordinate(location.Place.Location.Latitude, location.Place.Location.Longitude));

            integrationModel.Coordinates = checkIns;

            return integrationModel;
        }

        string FindAnswerByTitle(string title)
        {
            Question question = null;
            foreach (var item in surveyResult.Questions)
            {
                if (title == item.Title)
                    question = item;
            }

            if (question == null)
                throw new ArgumentException();

            return question.Answers[0];
        }
    }
}
