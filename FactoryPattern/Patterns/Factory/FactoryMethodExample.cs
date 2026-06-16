namespace FactoryPattern.Patterns.Factory;

// ────────────────────────────────────────────────────────────────
// Factory Method
//
// WHAT: A base class defines the general workflow, but lets
//       subclasses decide which specific product to create.
//
// HOW:  The base class has a "factory method" (CreateNotification)
//       that subclasses override. The base class calls this method
//       when it needs a product, but doesn't know (or care) which
//       concrete type it gets.
//
// BENEFIT: You can add new notification types by creating a new
//          subclass — no need to modify existing code!
// ────────────────────────────────────────────────────────────────

// The base "Creator" class.
// It is "abstract" because it cannot be used directly —
// you must create a subclass (like EmailCampaign) that fills
// in the missing piece (CreateNotification).
public abstract class NotificationCampaign
{
    // Deliver() is the shared workflow.
    // It calls CreateNotification() to get the right product,
    // then uses it. The subclasses decide what CreateNotification()
    // actually returns.
    public void Deliver(string recipient, string message)
    {
        var notification = CreateNotification();  // <-- calls the factory method
        Console.WriteLine($"  Preparing {notification.Channel} campaign.");
        notification.Send(recipient, message);
    }

    // This is the "factory method". Subclasses must override it
    // to return the specific INotification they need.
    // "protected" means only subclasses can call it.
    // "abstract" means subclasses MUST provide their own version.
    protected abstract INotification CreateNotification();
}

// Each subclass overrides CreateNotification() to return the
// specific notification type it represents.
public class EmailCampaign : NotificationCampaign
{
    protected override INotification CreateNotification() => new EmailNotification();
}

public class SmsCampaign : NotificationCampaign
{
    protected override INotification CreateNotification() => new SmsNotification();
}

public class PushCampaign : NotificationCampaign
{
    protected override INotification CreateNotification() => new PushNotification();
}
