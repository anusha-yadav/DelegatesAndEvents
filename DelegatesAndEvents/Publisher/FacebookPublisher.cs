using DelegatesAndEvents.Events;

namespace DelegatesAndEvents.Publisher
{
    public class FacebookPublisher
    {
        private readonly List<EventHandler<NotificationEventArgs>> _subscribers;
        private readonly object _lock = new object();

        public event EventHandler<NotificationEventArgs> Notification;

        public FacebookPublisher()
        {
            _subscribers = new List<EventHandler<NotificationEventArgs>>();
        }

        public void Subscribe(EventHandler<NotificationEventArgs> handler)
        {
            lock (_lock)
            {
                if (!_subscribers.Contains(handler))
                {
                    _subscribers.Add(handler);
                    Notification += handler;
                }
                else
                {
                    throw new InvalidOperationException("Handler is already subscribed.");
                }
            }
        }

        public void Unsubscribe(EventHandler<NotificationEventArgs> handler)
        {
            lock (_lock)
            {
                if (_subscribers.Contains(handler))
                {
                    _subscribers.Remove(handler);
                    Notification -= handler;
                }
                else
                {
                    throw new InvalidOperationException("Handler is not subscribed.");
                }
            }
        }

        public void Notify(string message)
        {
            NotificationEventArgs args = new NotificationEventArgs(message);

            EventHandler<NotificationEventArgs> handler;
            lock (_lock)
            {
                handler = Notification;
            }

            if (handler != null)
            {
                foreach (EventHandler<NotificationEventArgs> subscriber in handler.GetInvocationList())
                {
                    try
                    {
                        subscriber(this, args);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error notifying subscriber: {ex.Message}");
                    }
                }
            }
        }
    }
}

