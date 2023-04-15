using System.Net;

namespace GRA.Application.Responses
{
    public class ResponsesNotificationViewModel
    {
        public string? PropertyName { get; set; }

        public string? ErrorMessage { get; set; }

        public string? ErrorCode { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
