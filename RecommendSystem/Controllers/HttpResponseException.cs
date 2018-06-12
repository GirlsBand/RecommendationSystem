using System;
using System.Net;
using System.Runtime.Serialization;

namespace RecommendationSystem
{
    [Serializable]
    internal class HttpResponseException : Exception
    {
        public HttpStatusCode statusCode;

       public HttpResponseException(HttpStatusCode statusCode) : base()
        {
            this.statusCode = statusCode;
        }
    }
}