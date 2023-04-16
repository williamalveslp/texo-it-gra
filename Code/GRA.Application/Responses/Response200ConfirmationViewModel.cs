using System.Net;

namespace GRA.Application.Responses
{
    /// <summary>
    /// Response 2xx.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Response200ConfirmationViewModel<T> : ResponseBaseViewModel
    {
        public T Data { get; set; }

        public Response200ConfirmationViewModel(T data) : base(HttpStatusCode.OK)
        {
            this.Success = true;
            this.Data = data;
        }
    }
}
