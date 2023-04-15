using GRA.Application.Responses;
using GRA.Domain.Core.Handlers;
using GRA.Domain.Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GRA.API.Controllers.Base
{
    /// <summary>
    /// Controller base.
    /// </summary>
    public abstract class ApiBaseController : Controller
    {
        /// <summary>
        /// Notifications.
        /// </summary>
        private readonly DomainNotificationHandler _notifications;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="notifications"></param>
        protected ApiBaseController(INotificationHandler<DomainNotificationRequest> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected new IActionResult Response(object? result)
        {
            if (IsValidOperation())
                return Ok(new Response200ConfirmationViewModel<object?>(result));

            var errorsNotifications = _notifications.GetNotifications()?.OrderByDescending(f => f.StatusCode)
                                                                       ?.ThenBy(f => f.ErrorCode);

            if (errorsNotifications != null)
            {
                var responsesNotificationViewModel = new List<ResponsesNotificationViewModel>(capacity: errorsNotifications.Count());

                foreach (var item in errorsNotifications)
                {
                    responsesNotificationViewModel.Add(new ResponsesNotificationViewModel
                    {
                        ErrorMessage = item.Value,
                        ErrorCode = item.ErrorCode,
                        StatusCode = item.StatusCode,
                        PropertyName = item.PropertyName
                    });
                }

                IEnumerable<HttpStatusCode> GetStatusCode(IEnumerable<ResponsesNotificationViewModel> responsesNotificationViewModel)
                    => (responsesNotificationViewModel != null) ? responsesNotificationViewModel.Select(f => f.StatusCode) : new List<HttpStatusCode>(capacity: 0);

                var statusCodes500ServerError = responsesNotificationViewModel.Where(f => f.StatusCode >= HttpStatusCode.InternalServerError && f.StatusCode <= HttpStatusCode.HttpVersionNotSupported);

                if (statusCodes500ServerError.Any())
                    return BadRequest(new Response500ServerErrorViewModel<object>(responsesNotificationViewModel, GetStatusCode(statusCodes500ServerError)));

                var statusCode400ClientError = responsesNotificationViewModel.Where(f => f.StatusCode >= HttpStatusCode.BadRequest && f.StatusCode <= HttpStatusCode.ExpectationFailed);

                if (statusCode400ClientError.Any())
                    return BadRequest(new Response400ClientErrorViewModel<object>(responsesNotificationViewModel, GetStatusCode(statusCode400ClientError)));

                var statusCode300Redirection = responsesNotificationViewModel.Where(f => f.StatusCode >= HttpStatusCode.MultipleChoices && f.StatusCode <= HttpStatusCode.Unused);

                if (statusCode300Redirection.Any())
                    return BadRequest(new Response300RedirectionViewModel<object>(responsesNotificationViewModel, GetStatusCode(statusCode300Redirection)));
            }

            return Ok(new Response100InformativeViewModel(HttpStatusCode.Continue));
        }

        protected IActionResult ResponseModelStateInvalid()
        {
            return Response(null);
        }

        private bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }
    }
}
