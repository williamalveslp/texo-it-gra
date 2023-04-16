using System.Net;

namespace GRA.Application.Responses
{
    public sealed class Response100InformativeViewModel : ResponseBaseViewModel
    {
        public Response100InformativeViewModel(HttpStatusCode statusCode) : base(statusCode)
        {
            this.Success = true;
        }

        public Response100InformativeViewModel(IEnumerable<HttpStatusCode> statusCodes) : base(statusCodes)
        {
            this.Success = true;
        }
    }
}
