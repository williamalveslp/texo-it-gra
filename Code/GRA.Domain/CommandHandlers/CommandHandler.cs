using GRA.Domain.Commands;
using GRA.Domain.Core.Requests;
using MediatR;
using System.Net;

namespace GRA.Domain.CommandHandlers
{
    /// <summary>
    /// Abstract class related to Handler to Notification Pattern.
    /// </summary>
    public abstract class CommandHandler
    {
        /// <summary>
        /// Encapsule Request/Response and publishing the interaction patterns.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator"></param>
        protected CommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessage">Errors message.</param>
        /// <param name="httpStatusCode">HTTP Status Code related to notification.</param>
        protected void NotifyValidationErrors(string? errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            _mediator.Publish(new DomainNotificationRequest(errorMessage, httpStatusCode));
        }

        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessage">Errors message.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="httpStatusCode">HTTP Status Code related to notification.</param>
        protected void NotifyValidationErrors(string? errorMessage, string propertyName, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            _mediator.Publish(new DomainNotificationRequest(errorMessage, null, httpStatusCode, propertyName));
        }


        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessage">Errors message.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="httpStatusCode">HTTP Status Code related to notification.</param>
        protected void NotifyValidationErrors(string? errorMessage, string propertyName, string errorCode, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            _mediator.Publish(new DomainNotificationRequest(errorMessage, errorCode, httpStatusCode, propertyName));
        }

        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessages">List of the errors message.</param>
        /// <param name="httpStatusCode">HTTP Status Code related to notification.</param>
        protected void NotifyValidationErrors(IEnumerable<string?>? errorMessages, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            if (errorMessages == null)
                return;

            foreach (var item in errorMessages)
            {
                NotifyValidationErrors(item, httpStatusCode);
            }
        }

        /// <summary>
        /// It calls handlers to storage the notification.
        /// </summary>
        /// <param name="errorMessages">List of the errors message.</param>
        /// <param name="httpStatusCode">HTTP Status Code related to notification.</param>
        protected void NotifyValidationErrors(IEnumerable<ValidationResultResponse>? errorMessages, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            if (errorMessages == null)
                return;

            foreach (var item in errorMessages)
            {
                var domainNotificationRequest = new DomainNotificationRequest(item.ErrorMessage, item.ErrorCode, httpStatusCode, item.PropertyName);
                _mediator.Publish(domainNotificationRequest);
            }
        }
    }
}
