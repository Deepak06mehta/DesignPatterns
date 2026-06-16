namespace FactoryPattern.Patterns.Factory;

// ────────────────────────────────────────────────────────────────
// FactoryDemo — Runs all factory pattern examples
//
// This class calls each factory variant one at a time so you
// can see the output in the console and understand how each
// pattern works differently.
// ────────────────────────────────────────────────────────────────
public static class FactoryDemo
{
    // Entry point — calls each demonstration method.
    public static void Run()
    {
        Console.WriteLine("=== Factory Pattern Demo ===\n");
        DemonstrateSimpleFactory();
        DemonstrateFactoryMethod();
        DemonstrateRegistrationFactory();
    }

    // 1. Simple Factory: asks the factory to create an Email
    //    notification by passing the enum value.
    static void DemonstrateSimpleFactory()
    {
        Console.WriteLine("1) Simple Factory — one static method creates objects");
        var notification = SimpleNotificationFactory.Create(NotificationChannel.Email);
        notification.Send("user@example.com", "Created via static switch.");
    }

    // 2. Factory Method: creates a PushCampaign (a subclass) and
    //    calls Deliver(). The PushCampaign decides internally to
    //    create a PushNotification.
    static void DemonstrateFactoryMethod()
    {
        Console.WriteLine("\n2) Factory Method — subclasses supply the product");
        new PushCampaign().Deliver("device-42", "Created by subclass override.");
    }

    // 3. Registration Factory: registers two notification types
    //    ("email" and "slack"), then creates a Slack notification.
    //    Notice that SlackNotification is not available in the
    //    Simple Factory — the registry doesn't need a switch statement.
    static void DemonstrateRegistrationFactory()
    {
        Console.WriteLine("\n3) Registration Factory — add products at runtime");
        var factory = new NotificationRegistryFactory();
        factory.Register("email", () => new EmailNotification());
        factory.Register("slack", () => new SlackNotification());
        factory.Create("slack").Send("#patterns", "No switch statement needed.");
    }
}
