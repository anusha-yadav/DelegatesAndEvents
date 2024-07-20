using DelegatesAndEvents.Events;
using DelegatesAndEvents.Publisher;

namespace DelegatesAndEvents
{
    public class Program
    {
        static void Main(string[] args)
        {
            FacebookPublisher publisher = new FacebookPublisher();

            EventHandler<NotificationEventArgs> firstSubscriber = (sender, e) =>
            {
                Console.WriteLine($"firstSubscriber received notification: {e.Message} at {e.Timestamp}");
            };

            EventHandler<NotificationEventArgs> secondSubscriber = (sender, e) =>
            {
                Console.WriteLine($"secondSubscriber received notification: {e.Message} at {e.Timestamp}");
            };

            try
            {
                // Subscribe handlers
                publisher.Subscribe(firstSubscriber);
                publisher.Subscribe(secondSubscriber);

                Thread thread1 = new Thread(() => publisher.Notify("New post published by Thread 1!"));
                Thread thread2 = new Thread(() => publisher.Notify("New post published by Thread 2!"));

                // Start threads
                thread1.Start();
                thread2.Start();

                thread1.Join();
                thread2.Join();

                publisher.Unsubscribe(firstSubscriber);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

