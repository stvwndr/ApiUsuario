using Infrastructure.Interfaces;
using System;

namespace Infrastructure.Notifications
{
    public class Notification : INotification
    {
        public string Message { get; set; }

        public Notification GetError(Exception exception)
        {
            return new Notification()
            {
                Message = exception.Message,
            };
        }
    }
}
