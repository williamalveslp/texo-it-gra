using System.Net;

namespace GRA.Application.Responses
{
    /// <summary>
    /// Response 3xx.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Response300RedirectionViewModel<T> : ResponseBaseViewModel, IResponseErrorsViewModel<T>
    {
        private T _errors;

        public T Errors
        {
            get { return _errors; }
            private set { this._errors = value; }
        }

        public Response300RedirectionViewModel(T errors, IEnumerable<HttpStatusCode> statusCodes) : base(statusCodes)
        {
            this.Success = false;
            this.Errors = errors;
        }
    }
}
