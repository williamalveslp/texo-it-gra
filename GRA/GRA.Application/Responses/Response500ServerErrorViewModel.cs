using System.Net;

namespace GRA.Application.Responses
{
    /// <summary>
    /// Response 5xx.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Response500ServerErrorViewModel<T> : ResponseBaseViewModel, IResponseErrorsViewModel<T>
    {
        private T _errors;

        public T Errors
        {
            get { return _errors; }
            private set { this._errors = value; }
        }

        public Response500ServerErrorViewModel(T errors, HttpStatusCode statusCode) : base(statusCode)
        {
            this.Errors = errors;
        }

        public Response500ServerErrorViewModel(T errors, IEnumerable<HttpStatusCode> statusCodes) :base(statusCodes)
        {
            this.Success = false;
            this.Errors = errors;
        }
    }
}
