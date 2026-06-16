namespace FactoryPattern.Patterns.Factory;

// ────────────────────────────────────────────────────────────────
// INotification — The "Product" Interface
//
// This is the common contract that ALL notification types must follow.
// Every notification has a Channel name and a Send method.
// The factory patterns in this project all create objects that
// implement this interface. The caller never needs to know the
// concrete type (Email, SMS, etc.) — it just works with INotification.
// ────────────────────────────────────────────────────────────────
public interface INotification
{
    // Example: "Email", "SMS", "Push", "Slack"
    string Channel { get; }

    // Sends the message to the given recipient.
    void Send(string recipient, string message);
}

// ────────────────────────────────────────────────────────────────
// Concrete Products — The actual implementations
//
// Each of these classes "implements" the INotification contract.
// They contain the real logic for sending via their specific channel.
// ────────────────────────────────────────────────────────────────

// Sends notifications via Email.
public class EmailNotification : INotification
{
    public string Channel => "Email";
    public void Send(string recipient, string message) => Console.WriteLine($"  [Email] To: {recipient} | Body: {message}");
}

// Sends notifications via SMS (text message).
public class SmsNotification : INotification
{
    public string Channel => "SMS";
    public void Send(string recipient, string message) => Console.WriteLine($"  [SMS] To: {recipient} | Message: {message}");
}

// Sends push notifications to a device.
public class PushNotification : INotification
{
    public string Channel => "Push";
    public void Send(string recipient, string message) => Console.WriteLine($"  [Push] Device: {recipient} | Alert: {message}");
}

// Sends notifications to a Slack channel.
// NOTE: This product is NOT available in SimpleNotificationFactory.
// It is only used with the registration-based factory (which lets
// you add new types without editing existing code).
public class SlackNotification : INotification
{
    public string Channel => "Slack";
    public void Send(string recipient, string message) => Console.WriteLine($"  [Slack] Channel: {recipient} | Post: {message}");
}
