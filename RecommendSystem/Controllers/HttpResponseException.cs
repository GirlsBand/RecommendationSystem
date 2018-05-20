using System;
using System.Net;
using System.Runtime.Serialization;

namespace RecommendationSystem
{
    [Serializable]
    internal class HttpResponseException : Exception
    {
        private HttpStatusCode statusCode;

        public HttpResponseException()
        {
        }

        public HttpResponseException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public HttpResponseException(string message) : base(message)
        { }
    }
}