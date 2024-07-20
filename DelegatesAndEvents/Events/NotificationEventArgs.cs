namespace DelegatesAndEvents.Events
{
    public class NotificationEventArgs : EventArgs
    {
        public string Message { get; }
        public DateTime Timestamp { get; }

        public NotificationEventArgs(string message)
        {
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}

