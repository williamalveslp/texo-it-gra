namespace GRA.Application.Responses
{
    public interface IResponseErrorsViewModel<T>
    {
        public T Errors { get; }
    }
}
