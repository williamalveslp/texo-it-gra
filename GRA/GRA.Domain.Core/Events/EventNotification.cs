using MediatR;

namespace GRA.Domain.Core.Events
{
    /// <summary>
    /// Abstract class as event using MediatR.
    /// </summary>
    public abstract class EventNotification : INotification
    {
        /// <summary>
        /// Timestamp of the notification.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Type of class being sending.
        /// </summary>
        public string MessageType { get; protected set; }

        public Guid AggregateId { get; protected set; }

        protected EventNotification()
        {
            this.MessageType = GetType().Name;
            this.Timestamp = DateTime.Now;
        }
    }
}
