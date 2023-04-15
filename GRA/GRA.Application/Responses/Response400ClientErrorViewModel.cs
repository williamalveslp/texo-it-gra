using System.Net;

namespace GRA.Application.Responses
{
    /// <summary>
    /// Response 4xx.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Response400ClientErrorViewModel<T> : ResponseBaseViewModel, IResponseErrorsViewModel<T>
    {
        private T _errors;

        public T Errors
        {
            get { return _errors; }
            private set { this._errors = value; }
        }

        public Response400ClientErrorViewModel(T errors, HttpStatusCode statusCode) : base(statusCode)
        {
            this.Success = false;
            this.Errors = errors;
        }

        public Response400ClientErrorViewModel(T errors, IEnumerable<HttpStatusCode> statusCodes) : base(statusCodes)
        {
            this.Success = false;
            this.Errors = errors;
        }
    }
}
