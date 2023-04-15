using System.Net;

namespace GRA.Application.Responses
{
    public abstract class ResponseBaseViewModel
    {
        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        protected ResponseBaseViewModel(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        protected ResponseBaseViewModel(IEnumerable<HttpStatusCode> statusCodes)
        {
            this.StatusCode = GetMaxStatusCode(statusCodes);
        }

        protected HttpStatusCode GetMaxStatusCode(IEnumerable<HttpStatusCode> statusCodes)
        {
            return (statusCodes != null) ? statusCodes.Max(x => x) : HttpStatusCode.BadRequest;
        }
    }
}
