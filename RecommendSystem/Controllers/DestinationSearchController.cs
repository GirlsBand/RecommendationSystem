using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RecommendationSystem.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        public async Task<ApartmentsResult> GetDestination([FromBody]string json)
        {
            string clientAccessToken = RetrieveAccessToken();
            var account = await _service.GetAccountAsync(clientAccessToken);

            var surveyResult = JsonConvert.DeserializeObject<Survey>(json);

            var integrationModel = FormIntegrationModel(account, surveyResult);

            var result = await _provider.GetOrAdd(clientAccessToken, integrationModel);

            return result.ToApartmentsResult();
        }

        [HttpGet]
        [Route("api/prices")]
        public dynamic GetPrices()
        {
            var resultForUser = GetResultModelByAccessToken();

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
        public ApartmentsResult FilterApartments([FromBody] float highestPrice)
        {
            var resultForUser = GetResultModelByAccessToken();

            var filteredResult = new List<Appartment>();

            foreach (var appartment in resultForUser.Apartments)
            {
                if (appartment.Price <= highestPrice)
                {
                    filteredResult.Add(appartment);
                }
            }

            resultForUser.Apartments = filteredResult.ToArray();

            return resultForUser.ToApartmentsResult();
        }

        ResponseModel GetResultModelByAccessToken()
        {
            var accessToken = RetrieveAccessToken();

            //TODO check cache disctionary implementation

            //if (!ResponseDictionary.TryGetValue(accessToken, out ResponseModel resultForUser))
            //   throw new HttpResponseException(HttpStatusCode.NotFound);

            var resultForUser = new ResponseModel();

            if (resultForUser.Apartments == null || resultForUser.Apartments.Length == 0)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return resultForUser;
        }

        string RetrieveAccessToken()
        {
            if (!Request.Headers.ContainsKey("ClientAccessToken"))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            Request.Headers.TryGetValue("ClientAccessToken", out StringValues clientAccessTokens);

            return clientAccessTokens[0].ToString();
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

            if (question == null)
                throw new ArgumentException();

            return question.Answer;
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
