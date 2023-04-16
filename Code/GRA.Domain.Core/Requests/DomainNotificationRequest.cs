using System.Net;
using GRA.Domain.Core.Events;

namespace GRA.Domain.Core.Requests
{
    /// <summary>
    /// Notification request.
    /// </summary>
    public class DomainNotificationRequest : EventNotification
    {
        /// <summary>
        /// Identifier of notification.
        /// </summary>
        public Guid DomainNotificationId { get; private set; }

        /// <summary>
        /// Value of notification. Can be a error message, as example.
        /// </summary>
        public string Value { get; private set; } = null!;

        /// <summary>
        /// Error code.
        /// </summary>
        public string? ErrorCode { get; private set; }

        /// <summary>
        /// API version.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Property Name.
        /// </summary>
        public string PropertyName { get; private set; } = null!;

        /// <summary>
        /// Http Status Code from request.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public DomainNotificationRequest(string value)
        {
            InitialComponents(value, HttpStatusCode.BadRequest);
        }

        public DomainNotificationRequest(string value, HttpStatusCode statusCode)
        {
            InitialComponents(value, statusCode);
        }

        public DomainNotificationRequest(string value, string errorCode, HttpStatusCode statusCode, string propertyName)
        {
            InitialComponents(value, statusCode);
            this.ErrorCode = errorCode;
            this.PropertyName = propertyName;
        }

        private void InitialComponents(string value, HttpStatusCode statusCode)
        {
            this.DomainNotificationId = Guid.NewGuid();
            this.Version = 1;
            this.Value = value;
            this.StatusCode = statusCode;
        }
    }
}
