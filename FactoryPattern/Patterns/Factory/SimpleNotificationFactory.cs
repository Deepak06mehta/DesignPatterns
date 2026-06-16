namespace FactoryPattern.Patterns.Factory;

// ────────────────────────────────────────────────────────────────
// Simple Factory
//
// WHAT: A single method that creates different objects based on
//       a parameter you pass in.
//
// WHY:  Instead of writing "new EmailNotification()" everywhere,
//       you call one method and tell it what you need. All the
//       "new" logic lives in ONE place.
//
// DOWNSIDE: If you want to add a new type (e.g. SlackNotification),
//           you must edit this file and add a new case to the
//           switch statement. This violates the "Open/Closed
//           Principle" (classes should be open for extension but
//           closed for modification).
// ────────────────────────────────────────────────────────────────
public static class SimpleNotificationFactory
{
    // The Create method takes a NotificationChannel value and
    // returns the matching INotification object.
    // The "switch expression" checks which channel was requested
    // and creates the right kind of notification.
    public static INotification Create(NotificationChannel channel) =>
        channel switch
        {
            NotificationChannel.Email => new EmailNotification(),
            NotificationChannel.Sms => new SmsNotification(),
            NotificationChannel.Push => new PushNotification(),
            _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, "Unsupported notification channel.")
        };
}

// This enum lists the available notification channels.
// Each value matches one case in the switch statement above.
public enum NotificationChannel
{
    Email,
    Sms,
    Push
}
