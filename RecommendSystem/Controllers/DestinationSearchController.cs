using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecommendationSystem.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem.Controllers
{
    public class DestinationSearchController : ControllerBase
    {
        const string requestUri = "";

        private readonly IFacebookService _service;
        private readonly DestinationProvider _provider;


        public DestinationSearchController(IFacebookService service, DestinationProvider provider)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        [Route("api/destination")]
        [HttpPost]
        public async Task<ApartmentsResult> GetDestination(string json)
        {
            string clientAccessToken = RetrieveAccessToken();
            var account = await _service.GetAccountAsync(clientAccessToken);

            Survey surveyResult = null;
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();

                surveyResult = JsonConvert.DeserializeObject<Survey>(body);
            }

            if (surveyResult == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var integrationModel = FormIntegrationModel(account, surveyResult);

            var result = await _provider.GetOrAdd(clientAccessToken, integrationModel);

            return result.ToApartmentsResult();
        }

        [Route("api/destination")]
        [HttpOptions]
        public string Options()
        {
            return "api/destination";
        }

        [HttpGet]
        [Route("api/prices")]
        public async Task<dynamic> GetPrices()
        {
            var resultForUser = await _provider.GetOrAdd(RetrieveAccessToken(), null);

            var lowestPrice = resultForUser.Apartments[0].Price;
            var highestPrice = resultForUser.Apartments[0].Price;

            foreach (var appartment in resultForUser.Apartments)
            {
                var currPrice = appartment.Price;

                if (currPrice < lowestPrice)
                    lowestPrice = currPrice;
                if (currPrice > highestPrice)
                    highestPrice = currPrice;
            }

            return new 
            {
                LowestPrice = lowestPrice,
                HighestPrice = highestPrice
            };
        }

        [HttpGet]
        [Route("api/apartments")]
        public async Task<ApartmentsResult> FilterApartments([FromBody] float highestPrice)
        {
            var resultForUser = await _provider.GetOrAdd(RetrieveAccessToken(), null);

            var filteredResult = new List<Appartment>();

            foreach (var appartment in resultForUser.Apartments)
            {
                if (appartment.Price <= highestPrice)
                {
                    filteredResult.Add(appartment);
                }
            }

            if (filteredResult.Count == 0)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            resultForUser.Apartments = filteredResult.ToArray();

             return resultForUser.ToApartmentsResult();
        }

        string RetrieveAccessToken()
        {
            if (!Request.Headers.ContainsKey("ClientAccessToken"))
                throw new HttpResponseException(HttpStatusCode.Forbidden, "ClientAccessToken ");

            var value = Request.Headers["ClientAccessToken"];

            return value[0];
         }

        IntegrationModel FormIntegrationModel(Account account, Survey surveyResult)
        {
            var integrationModel = CreateIntegrationModel(account);

            integrationModel.InCity = FindAnswerByTitle(surveyResult, Survey.Region) == "City";
            integrationModel.AmountOfPeopleLiving = Int32.Parse(FindAnswerByTitle(surveyResult, Survey.People));
            integrationModel.City = FindAnswerByTitle(surveyResult, Survey.City);
            integrationModel.Work = FindAnswerByTitle(surveyResult, Survey.Work);
            integrationModel.Study = FindAnswerByTitle(surveyResult, Survey.Study);
            integrationModel.PetsToWalkPresence = FindAnswerByTitle(surveyResult, Survey.Pets).Equals("Yes", StringComparison.InvariantCultureIgnoreCase);

            return integrationModel;
        }

        IntegrationModel CreateIntegrationModel(Account account)
        {
            var integrationModel = new IntegrationModel();

            var checkIns = new List<Coordinate>();

            foreach (var location in account.Tagged_Places)
                if (location.Created_time > DateTime.Today.AddYears(-1))
                    checkIns.Add(new Coordinate(location.Place.Location.Latitude, location.Place.Location.Longitude));

            integrationModel.Coordinates = checkIns;

            return integrationModel;
        }

        string FindAnswerByTitle(Survey surveyResult, string name)
        {
            Question question = null;

            foreach (var item in surveyResult.Questions)
            {
                if (item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    question = item;
            }

            return question == null ? string.Empty: question.Answer;
        }


    }

    public class DestinationProvider
    {
        private readonly HttpClient _client;
        private readonly ConcurrentDictionary<string, ResponseModel>
            _cache = new ConcurrentDictionary<string, ResponseModel>();

        private readonly string _uri;

        public DestinationProvider(HttpClient client, string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentNullException(nameof(uri));

            _client = client ?? throw new ArgumentNullException(nameof(client));
            _uri = uri;
        }

        public async Task<ResponseModel> GetOrAdd(string token, IntegrationModel model)
        {
            return MockData.ResponseModel;

            if (_cache.TryGetValue(token, out var value))
                return value;

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_uri, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var destinations = JsonConvert.DeserializeObject<ResponseModel>(responseContent);
            _cache.AddOrUpdate(token, destinations, (k, v) => destinations);
            return destinations;
        }
    }
}
