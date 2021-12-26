using Infrastructure.Notifications;
using System;

namespace Infrastructure.Interfaces
{
    public interface INotification
    {
        Notification GetError(Exception exception);
    }
}
